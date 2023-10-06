use crate::{
    commands::{
        selectable_cylinder::SelectableCylinder, selectable_dive_model::SelectableDiveModel,
    },
    controllers::file::{read_dive_planner_state, upsert_dive_planner_state, upsert_dive_results},
    models::{
        central_nervous_system_toxicity::CentralNervousSystemToxicity,
        decompression_steps::DecompressionSteps, dive_profile::DiveProfile, dive_stage::DiveStage,
        results::DiveResults, select_cylinder::SelectCylinder, select_dive_model::SelectDiveModel,
    },
};
use iced::Sandbox;
use serde::{Deserialize, Serialize};

const DIVE_PLANNER_STATE_FILE_NAME: &str = "dive_planner_state.json";
const DIVE_PLAN: &str = "dive_plan.json";

#[derive(Debug, Clone, PartialEq, Serialize, Deserialize)]
pub struct DivePlanner {
    pub select_dive_model: SelectDiveModel,
    pub select_cylinder: SelectCylinder,
    pub dive_stage: DiveStage,
    pub dive_results: DiveResults,
    pub decompression_steps: DecompressionSteps,
    pub cns_toxicity: CentralNervousSystemToxicity,
    pub redo_buffer: Vec<DiveStage>,
}

impl Default for DivePlanner {
    fn default() -> Self {
        Self::new()
    }
}

impl DivePlanner {
    pub fn file_new(&mut self) {
        *self = DivePlanner::default();
    }

    pub fn file_save(&self) {
        upsert_dive_planner_state(DIVE_PLANNER_STATE_FILE_NAME, self);
        upsert_dive_results(DIVE_PLAN, &self.dive_results.results);
    }

    pub fn file_load(&mut self) {
        *self = read_dive_planner_state(DIVE_PLANNER_STATE_FILE_NAME)
    }

    pub fn edit_undo(&mut self) {
        if self.is_undoable() {
            let latest = self.dive_stage;
            self.redo_buffer.push(latest);
            self.dive_results.results.pop();
            self.dive_stage = *self
                .dive_results
                .results
                .last()
                .unwrap_or(&Default::default());
        } else {
            self.file_new();
        }
    }

    pub fn edit_redo(&mut self) {
        if self.is_redoable() {
            let redo = self.redo_buffer.pop().unwrap();
            self.dive_results.results.push(redo);
            self.dive_stage = redo;
        }
    }

    pub fn is_undoable(&self) -> bool {
        !self.dive_results.results.is_empty()
    }

    pub fn is_redoable(&self) -> bool {
        !self.redo_buffer.is_empty()
    }

    pub fn view_toggle_central_nervous_system_toxicity_visibility(&mut self) {
        self.cns_toxicity.toggle_visibility();
    }

    pub fn view_toggle_select_cylinder_visibility(&mut self) {
        self.select_cylinder.toggle_visibility();
    }

    pub fn dive_model_selected(&mut self, selectable_dive_model: SelectableDiveModel) {
        self.select_dive_model
            .select_dive_model(selectable_dive_model, &mut self.dive_stage.dive_model);
    }

    pub fn cylinder_selected(&mut self, selectable_cylinder: SelectableCylinder) {
        self.select_cylinder
            .on_cylinder_selected(selectable_cylinder, &mut self.dive_stage.cylinder);
    }

    pub fn update_cylinder_selected(&mut self, selectable_cylinder: SelectableCylinder) {
        self.select_cylinder
            .update_cylinder_selected(selectable_cylinder, self.dive_stage.cylinder);
    }

    // TODO test
    pub fn update_dive_profile(&mut self) {
        self.assign_selected_cylinder();

        self.assign_dive_stage(DiveProfile::update_dive_profile(self.dive_stage));

        self.add_result();

        self.assign_selected_cylinder();

        self.assign_decompression_steps();

        self.update_visibility();
    }

    pub fn refresh_decompression(&mut self) {
        self.assign_selected_cylinder();

        self.assign_decompression_steps();
    }

    // TODO test
    pub fn decompression_update_dive_profile(&mut self) {
        self.refresh_decompression();

        self.run_decompression_steps();

        self.assign_decompression_steps();

        self.update_decompression_steps_visibility();
    }

    fn run_decompression_steps(&mut self) {
        for dive_step in &self.decompression_steps.dive_steps {
            self.dive_stage.dive_step = *dive_step;

            // TODO Refactor to use assign dive stage
            self.dive_stage = DiveProfile::update_dive_profile(self.dive_stage);

            // TODO Refactor to using dive_planner.update_results()
            self.dive_results.results.push(self.dive_stage);
        }

        self.redo_buffer = Default::default();
    }

    fn update_decompression_steps_visibility(&mut self) {
        self.decompression_steps.update_visibility();
    }

    fn assign_decompression_steps(&mut self) {
        self.decompression_steps
            .assign_decompression_steps(self.dive_stage.calculate_decompression_dive_steps());
    }

    fn assign_selected_cylinder(&mut self) {
        self.select_cylinder
            .assign_cylinder(self.dive_stage.cylinder);
    }

    fn assign_dive_stage(&mut self, dive_stage: DiveStage) {
        self.dive_stage = dive_stage
    }

    fn add_result(&mut self) {
        self.dive_results.results.push(self.dive_stage);
        self.redo_buffer = Default::default();
    }

    fn update_visibility(&mut self) {
        self.select_cylinder.read_only_view();
        self.dive_results.is_visible = true;
        self.decompression_steps.update_visibility();
    }
}

#[cfg(test)]
mod dive_step_view_should {
    use super::*;
    use crate::models::{
        cylinder::Cylinder, dive_model::DiveModel, dive_profile::DiveProfile, dive_step::DiveStep,
        gas_management::GasManagement, gas_mixture::GasMixture,
    };
    use rstest::rstest;
    use std::fs::{self};

    #[test]
    #[ignore]
    fn update_dive_profile() {}

    #[test]
    fn refresh_decompression() {
        // Given
        let cylinder = dive_stage_test_fixture().cylinder;
        let selectable_cylinder = SelectableCylinder::Bottom;
        let expected_dive_planner = DivePlanner {
            select_cylinder: SelectCylinder {
                selected_cylinder: Some(selectable_cylinder),
                cylinders: [cylinder, Default::default(), Default::default()],
                ..Default::default()
            },
            dive_stage: dive_stage_real_test_fixture(),
            decompression_steps: DecompressionSteps {
                is_visible: false,
                dive_steps: vec![
                    DiveStep { depth: 6, time: 1 },
                    DiveStep { depth: 3, time: 3 },
                ],
            },
            ..Default::default()
        };
        let mut dive_planner = DivePlanner {
            select_cylinder: SelectCylinder {
                selected_cylinder: Some(selectable_cylinder),
                cylinders: [Default::default(), Default::default(), Default::default()],
                ..Default::default()
            },
            dive_stage: dive_stage_real_test_fixture(),
            ..Default::default()
        };

        // When
        dive_planner.refresh_decompression();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
        // pub fn refresh_decompression(&mut self) {
        //     self.assign_selected_cylinder();

        //     self.assign_decompression_steps();
        // }
    }

    #[test]
    #[ignore]
    fn decompression_update_dive_profile() {}

    #[rstest]
    #[case(SelectableCylinder::Bottom, 0)]
    #[case(SelectableCylinder::Decompression, 1)]
    #[case(SelectableCylinder::Descend, 2)]
    fn update_the_selected_cylinder(
        #[case] selectable_cylinder: SelectableCylinder,
        #[case] index: usize,
    ) {
        // Given
        let select_cylinder = SelectCylinder {
            cylinders: Default::default(),
            selected_cylinder: Some(selectable_cylinder),
            is_visible: true,
        };
        let cylinder = Cylinder {
            is_read_only: true,
            volume: 12,
            pressure: 200,
            initial_pressurised_cylinder_volume: 2400,
            gas_mixture: GasMixture {
                oxygen: 21,
                helium: 10,
                nitrogen: 69,
                maximum_operating_depth: 56.67,
            },
            gas_management: GasManagement {
                remaining: 1680,
                used: 720,
                surface_air_consumption_rate: 12,
            },
        };
        let mut dive_planner = DivePlanner {
            select_cylinder,
            dive_stage: DiveStage {
                cylinder,
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        dive_planner.update_cylinder_selected(selectable_cylinder);

        // Then
        assert_eq!(cylinder, dive_planner.select_cylinder.cylinders[index]);
    }

    #[rstest]
    #[case(SelectableCylinder::Bottom)]
    #[case(SelectableCylinder::Decompression)]
    #[case(SelectableCylinder::Descend)]
    fn select_a_cylinder(#[case] selectable_cylinder: SelectableCylinder) {
        // Given
        let expected_cylinder = Cylinder {
            is_read_only: true,
            volume: 12,
            pressure: 200,
            initial_pressurised_cylinder_volume: 2400,
            gas_mixture: GasMixture {
                oxygen: 21,
                helium: 10,
                nitrogen: 69,
                maximum_operating_depth: 56.67,
            },
            gas_management: GasManagement {
                remaining: 1680,
                used: 720,
                surface_air_consumption_rate: 12,
            },
        };
        let select_cylinder = SelectCylinder {
            cylinders: [expected_cylinder, expected_cylinder, expected_cylinder],
            selected_cylinder: Some(selectable_cylinder),
            is_visible: true,
        };
        let mut dive_planner = DivePlanner {
            select_cylinder,
            ..Default::default()
        };

        // When
        dive_planner.cylinder_selected(selectable_cylinder);

        // Then
        assert_eq!(expected_cylinder, dive_planner.dive_stage.cylinder);
    }

    #[rstest]
    #[case(vec![dive_stage_test_fixture()], true)]
    #[case(vec![], false)]
    fn is_undoable(#[case] results: Vec<DiveStage>, #[case] expected_is_undoable: bool) {
        // Given
        let dive_planner = DivePlanner {
            dive_results: DiveResults {
                results,
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        let is_undoable = dive_planner.is_undoable();

        // Then
        assert_eq!(expected_is_undoable, is_undoable)
    }

    #[rstest]
    #[case(vec![dive_stage_test_fixture()], true)]
    #[case(vec![], false)]
    fn is_redoable(#[case] redo_buffer: Vec<DiveStage>, #[case] expected_is_redoable: bool) {
        // Given
        let dive_planner = DivePlanner {
            redo_buffer,
            ..Default::default()
        };

        // When
        let is_redoable = dive_planner.is_redoable();

        // Then
        assert_eq!(expected_is_redoable, is_redoable)
    }

    #[test]
    fn file_saves_and_loads_acceptance_test() {
        // Given
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture(),
            dive_results: DiveResults {
                results: vec![dive_stage_test_fixture()],
                ..Default::default()
            },
            ..Default::default()
        };
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture(),
            dive_results: DiveResults {
                results: vec![dive_stage_test_fixture()],
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        dive_planner.file_save();
        dive_planner.file_load();

        // Then
        assert!(fs::metadata(DIVE_PLANNER_STATE_FILE_NAME).is_ok());
        assert!(fs::metadata(DIVE_PLANNER_STATE_FILE_NAME).unwrap().len() != 0);
        assert!(fs::metadata(DIVE_PLAN).is_ok());
        assert!(fs::metadata(DIVE_PLAN).unwrap().len() != 0);
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn reset_dive_planner_to_default_state() {
        // Given
        let expected = DivePlanner::default();
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture(),
            dive_results: DiveResults {
                results: vec![
                    dive_stage_test_fixture(),
                    dive_stage_test_fixture(),
                    dive_stage_test_fixture(),
                ],
                ..Default::default()
            },
            redo_buffer: vec![dive_stage_test_fixture()],
            ..Default::default()
        };

        // When
        dive_planner.file_new();

        // Then
        assert_eq!(expected, dive_planner);
    }

    #[test]
    fn undo_a_dive_stage_with_no_results() {
        // Given
        let mut dive_planner = DivePlanner {
            dive_stage: Default::default(),
            dive_results: DiveResults {
                results: vec![],
                ..Default::default()
            },
            redo_buffer: vec![],
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: Default::default(),
            dive_results: DiveResults {
                results: vec![],
                ..Default::default()
            },
            redo_buffer: vec![],
            ..Default::default()
        };

        // When
        dive_planner.edit_undo();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn undo_a_dive_stage_with_one_result() {
        // Given
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_old(),
            dive_results: DiveResults {
                results: vec![dive_stage_test_fixture_old()],
                ..Default::default()
            },
            redo_buffer: vec![],
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: Default::default(),
            dive_results: DiveResults {
                results: vec![],
                ..Default::default()
            },
            redo_buffer: vec![dive_stage_test_fixture_old()],
            ..Default::default()
        };

        // When
        dive_planner.edit_undo();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn undo_a_dive_stage_with_multiple_results() {
        // Given
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_latest(),
            dive_results: DiveResults {
                results: vec![
                    dive_stage_test_fixture_old(),
                    dive_stage_test_fixture_latest(),
                ],
                ..Default::default()
            },
            redo_buffer: vec![],
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_old(),
            dive_results: DiveResults {
                results: vec![dive_stage_test_fixture_old()],
                ..Default::default()
            },
            redo_buffer: vec![dive_stage_test_fixture_latest()],
            ..Default::default()
        };

        // When
        dive_planner.edit_undo();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn redo_when_buffer_is_empty() {
        // Given
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_latest(),
            dive_results: DiveResults {
                results: vec![
                    dive_stage_test_fixture_old(),
                    dive_stage_test_fixture_latest(),
                ],
                ..Default::default()
            },
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_latest(),
            dive_results: DiveResults {
                results: vec![
                    dive_stage_test_fixture_old(),
                    dive_stage_test_fixture_latest(),
                ],
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        dive_planner.edit_redo();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn redo_dive_stage() {
        // Given
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_old(),
            dive_results: DiveResults {
                results: vec![dive_stage_test_fixture_old()],
                ..Default::default()
            },
            redo_buffer: vec![dive_stage_test_fixture_latest()],
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_latest(),
            dive_results: DiveResults {
                results: vec![
                    dive_stage_test_fixture_old(),
                    dive_stage_test_fixture_latest(),
                ],
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        dive_planner.edit_redo();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn redo_multiple_dive_stages() {
        // Given
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_old(),
            dive_results: DiveResults {
                results: vec![],
                ..Default::default()
            },
            redo_buffer: vec![
                dive_stage_test_fixture_old(),
                dive_stage_test_fixture_latest(),
            ],

            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_latest(),
            dive_results: DiveResults {
                results: vec![dive_stage_test_fixture_latest()],
                ..Default::default()
            },
            redo_buffer: vec![dive_stage_test_fixture_old()],
            ..Default::default()
        };

        // When
        dive_planner.edit_redo();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[rstest]
    #[case(false, true)]
    #[case(true, false)]
    fn toggle_select_cylinder_visibility(
        #[case] is_visible: bool,
        #[case] expected_is_visible: bool,
    ) {
        // Given
        let select_cylinder = SelectCylinder {
            is_visible,
            ..Default::default()
        };
        let mut dive_planner = DivePlanner {
            select_cylinder,
            ..Default::default()
        };

        // When
        dive_planner.view_toggle_select_cylinder_visibility();

        // Then
        assert_eq!(expected_is_visible, dive_planner.select_cylinder.is_visible);
    }

    #[rstest]
    #[case(false, true)]
    #[case(true, false)]
    fn toggle_the_central_nervous_system_toxicity_visibility(
        #[case] is_visible: bool,
        #[case] expected_is_visible: bool,
    ) {
        // Given
        let mut cns_toxicity = CentralNervousSystemToxicity::default();
        cns_toxicity.is_visible = is_visible;
        let mut dive_planner = DivePlanner {
            cns_toxicity,
            ..Default::default()
        };

        // When
        dive_planner.view_toggle_central_nervous_system_toxicity_visibility();

        // Then
        assert_eq!(expected_is_visible, dive_planner.cns_toxicity.is_visible)
    }

    #[rstest]
    #[case(
        DiveModel::create_zhl16_dive_model(),
        SelectableDiveModel::Bulhmann,
        DiveModel::create_usn_rev_6_dive_model()
    )]
    #[case(
        DiveModel::create_usn_rev_6_dive_model(),
        SelectableDiveModel::Usn,
        DiveModel::create_zhl16_dive_model()
    )]
    fn select_dive_model(
        #[case] expected_dive_model: DiveModel,
        #[case] selectable_dive_model: SelectableDiveModel,
        #[case] dive_model: DiveModel,
    ) {
        // Given
        let select_dive_model = SelectDiveModel {
            dive_model_list: Default::default(),
            selected_dive_model: Some(selectable_dive_model),
        };
        let mut dive_planner = DivePlanner {
            dive_stage: DiveStage {
                dive_model,
                ..Default::default()
            },
            select_dive_model,
            ..Default::default()
        };

        // When
        dive_planner.dive_model_selected(select_dive_model.selected_dive_model.unwrap());

        // Then
        assert_eq!(expected_dive_model, dive_planner.dive_stage.dive_model);
    }

    fn dive_stage_real_test_fixture() -> DiveStage {
        let mut dive_model = DiveModel::create_zhl16_dive_model();
        dive_model.dive_profile = dive_profile_real_test_fixture();

        DiveStage {
            dive_model,
            dive_step: DiveStep {
                depth: 50,
                time: 10,
            },
            cylinder: Cylinder {
                is_read_only: true,
                volume: 12,
                pressure: 200,
                initial_pressurised_cylinder_volume: 2400,
                gas_mixture: GasMixture {
                    oxygen: 32,
                    helium: 10,
                    nitrogen: 58,
                    maximum_operating_depth: 0.0,
                },
                gas_management: GasManagement {
                    remaining: 1680,
                    used: 720,
                    surface_air_consumption_rate: 12,
                },
            },
        }
    }

    fn dive_profile_real_test_fixture() -> DiveProfile {
        DiveProfile {
            number_of_compartments: 16,
            maximum_surface_pressures: [
                3.350, 2.630, 2.33, 2.10, 1.95, 1.79, 1.68, 1.60, 1.54, 1.48, 1.44, 1.400, 1.35,
                1.33, 1.300, 1.28,
            ],
            compartment_loads: [
                124.0, 124.0, 115.0, 105.0, 94.0, 88.0, 81.0, 75.0, 71.0, 69.0, 67.0, 67.0, 67.0,
                66.0, 66.0, 66.0,
            ],
            nitrogen_tissue_pressures: [
                3.500, 2.700, 2.200, 1.8, 1.5, 1.3, 1.2, 1.1, 1.0, 0.9, 0.9, 0.9, 0.9, 0.8, 0.8,
                0.8,
            ],
            helium_tissue_pressures: [
                0.594, 0.540, 0.462, 0.377, 0.296, 0.228, 0.172, 0.127, 0.093, 0.071, 0.056, 0.044,
                0.035, 0.028, 0.022, 0.017,
            ],
            total_tissue_pressures: [
                4.140, 3.270, 2.68, 2.21, 1.84, 1.57, 1.36, 1.21, 1.09, 1.02, 0.97, 0.93, 0.90,
                0.88, 0.86, 0.84,
            ],
            tolerated_ambient_pressures: [
                1.390, 1.410, 1.25, 1.09, 0.91, 0.82, 0.72, 0.65, 0.59, 0.57, 0.57, 0.56, 0.57,
                0.57, 0.57, 0.58,
            ],
            a_values: [
                1.3, 1.1, 0.9, 0.8, 0.7, 0.6, 0.5, 0.5, 0.4, 0.4, 0.4, 0.3, 0.3, 0.3, 0.3, 0.2,
            ],
            b_values: [
                0.493, 0.637, 0.708, 0.769, 0.800, 0.84, 0.859, 0.89, 0.910, 0.920, 0.93, 0.94,
                0.95, 0.95, 0.96, 0.96,
            ],
            oxygen_at_pressure: 1.26,
            helium_at_pressure: 0.600,
            nitrogen_at_pressure: 4.14,
            dive_ceiling: 4.1,
        }
    }

    fn dive_stage_test_fixture() -> DiveStage {
        DiveStage {
            dive_model: dive_model_test_fixture_old(),
            dive_step: DiveStep {
                depth: 50,
                time: 10,
            },
            cylinder: Cylinder {
                is_read_only: true,
                volume: 12,
                pressure: 200,
                initial_pressurised_cylinder_volume: 2400,
                gas_mixture: GasMixture {
                    oxygen: 32,
                    helium: 10,
                    nitrogen: 58,
                    maximum_operating_depth: 0.0,
                },
                gas_management: GasManagement {
                    remaining: 1680,
                    used: 720,
                    surface_air_consumption_rate: 12,
                },
            },
        }
    }

    fn dive_stage_test_fixture_old() -> DiveStage {
        DiveStage {
            dive_model: dive_model_test_fixture_old(),
            dive_step: DiveStep {
                depth: 50,
                time: 10,
            },
            cylinder: Cylinder {
                is_read_only: true,
                volume: 12,
                pressure: 200,
                initial_pressurised_cylinder_volume: 2400,
                gas_mixture: GasMixture {
                    oxygen: 32,
                    helium: 10,
                    nitrogen: 58,
                    maximum_operating_depth: 0.0,
                },
                gas_management: GasManagement {
                    remaining: 1680,
                    used: 720,
                    surface_air_consumption_rate: 12,
                },
            },
        }
    }

    fn dive_stage_test_fixture_latest() -> DiveStage {
        DiveStage {
            dive_model: dive_model_test_fixture_latest(),
            dive_step: DiveStep {
                depth: 100,
                time: 15,
            },
            cylinder: Cylinder {
                is_read_only: true,
                volume: 12,
                pressure: 200,
                initial_pressurised_cylinder_volume: 2400,
                gas_mixture: GasMixture {
                    oxygen: 32,
                    helium: 10,
                    nitrogen: 58,
                    maximum_operating_depth: 0.0,
                },
                gas_management: GasManagement {
                    remaining: 980,
                    used: 600,
                    surface_air_consumption_rate: 12,
                },
            },
        }
    }

    fn dive_model_test_fixture_old() -> DiveModel {
        let default_array = [
            2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0,
        ];

        DiveModel {
            number_of_compartments: 2,
            nitrogen_half_times: default_array,
            helium_half_times: default_array,
            a_values_nitrogen: default_array,
            b_values_nitrogen: default_array,
            a_values_helium: default_array,
            b_values_helium: default_array,
            dive_profile: DiveProfile {
                number_of_compartments: 16,
                maximum_surface_pressures: default_array,
                compartment_loads: default_array,
                nitrogen_tissue_pressures: default_array,
                helium_tissue_pressures: default_array,
                total_tissue_pressures: default_array,
                tolerated_ambient_pressures: default_array,
                a_values: default_array,
                b_values: default_array,
                oxygen_at_pressure: 2.34,
                helium_at_pressure: 2.56,
                nitrogen_at_pressure: 2.12,
                dive_ceiling: 0.0,
            },
            is_read_only: Default::default(),
        }
    }

    fn dive_model_test_fixture_latest() -> DiveModel {
        let default_array = [
            4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0,
        ];

        DiveModel {
            number_of_compartments: 2,
            nitrogen_half_times: default_array,
            helium_half_times: default_array,
            a_values_nitrogen: default_array,
            b_values_nitrogen: default_array,
            a_values_helium: default_array,
            b_values_helium: default_array,
            dive_profile: DiveProfile {
                number_of_compartments: 16,
                maximum_surface_pressures: default_array,
                compartment_loads: default_array,
                nitrogen_tissue_pressures: default_array,
                helium_tissue_pressures: default_array,
                total_tissue_pressures: default_array,
                tolerated_ambient_pressures: default_array,
                a_values: default_array,
                b_values: default_array,
                oxygen_at_pressure: 2.34,
                helium_at_pressure: 2.56,
                nitrogen_at_pressure: 2.12,
                dive_ceiling: 0.0,
            },
            is_read_only: Default::default(),
        }
    }
}
