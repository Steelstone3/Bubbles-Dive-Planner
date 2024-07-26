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
    pub is_planning: bool,
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

        self.refresh_decompression();
    }

    pub fn update_cylinder_selected(&mut self, selectable_cylinder: SelectableCylinder) {
        self.select_cylinder
            .update_cylinder_selected(selectable_cylinder, self.dive_stage.cylinder);
    }

    pub fn decompression_update_dive_profile(&mut self) {
        self.refresh_decompression();

        self.run_decompression_steps();

        self.assign_decompression_steps();
    }

    pub fn update_dive_profile(&mut self) {
        self.assign_selected_cylinder();

        self.assign_dive_stage(DiveProfile::update_dive_profile(self.dive_stage));

        self.add_result();

        self.assign_selected_cylinder();

        self.assign_decompression_steps();

        self.update_visibility();
    }

    fn refresh_decompression(&mut self) {
        self.assign_decompression_steps();
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
        self.is_planning = true;

        // TODO AH depricate all the needless readonly and is visible flags
        self.select_cylinder.read_only_view();
        // TODO AH depricate all the needless readonly and is visible flags
        self.dive_results.is_visible = true;
    }
}

#[cfg(test)]
mod dive_planner_should {
    use super::*;
    use crate::models::{
        cylinder::Cylinder, dive_model::DiveModel, dive_profile::DiveProfile, dive_step::DiveStep,
        gas_management::GasManagement, gas_mixture::GasMixture,
    };
    use rstest::rstest;
    use std::fs::{self};

    #[test]
    fn update_dive_profile() {
        // Given
        let mut expected_cylinder = dive_stage_test_fixture().cylinder;
        expected_cylinder.gas_management.remaining = 960;
        let cylinder = dive_stage_test_fixture().cylinder;
        let selectable_cylinder = SelectableCylinder::Bottom;
        let expected_dive_planner = DivePlanner {
            select_cylinder: SelectCylinder {
                selected_cylinder: Some(selectable_cylinder),
                cylinders: [cylinder, Default::default(), Default::default()],
                ..Default::default()
            },
            dive_stage: dive_stage_test_fixture(),
            decompression_steps: DecompressionSteps {
                dive_steps: vec![
                    DiveStep { depth: 9, time: 2 },
                    DiveStep { depth: 6, time: 3 },
                    DiveStep { depth: 3, time: 7 },
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
            dive_stage: dive_stage_test_fixture(),
            ..Default::default()
        };

        // When
        dive_planner.update_dive_profile();

        // Then
        assert!(!dive_planner.dive_results.results.is_empty());
        assert_eq!(expected_cylinder, dive_planner.select_cylinder.cylinders[0]);
        assert_eq!(
            expected_dive_planner.decompression_steps,
            dive_planner.decompression_steps
        );
        assert!(dive_planner.dive_results.is_visible);
        assert!(dive_planner.select_cylinder.cylinders[0].is_read_only);
        assert!(dive_planner.select_cylinder.cylinders[1].is_read_only);
        assert!(dive_planner.select_cylinder.cylinders[2].is_read_only);
    }

    #[test]
    fn decompression_update_dive_profile() {
        // Given
        let cylinder = dive_stage_test_fixture().cylinder;
        let selectable_cylinder = SelectableCylinder::Bottom;
        let expected_dive_profile = DiveProfile {
            number_of_compartments: 16,
            maximum_surface_pressures: [
                3.307063, 2.6041071, 2.3123977, 2.0934062, 1.9490435, 1.7928032, 1.6812322,
                1.5981219, 1.5376393, 1.4803765, 1.4364078, 1.3973763, 1.3510859, 1.3317899,
                1.3025703, 1.2758505,
            ],
            compartment_loads: [
                71.06676, 93.61995, 96.758675, 93.54917, 86.678925, 82.38294, 80.15056, 76.16506,
                70.9391, 65.68041, 66.66759, 67.67246, 69.31499, 62.323696, 63.234497, 64.14328,
            ],
            nitrogen_tissue_pressures: [
                2.143461, 2.1411657, 1.9202945, 1.6601363, 1.4312797, 1.2648269, 1.1799228,
                1.0892807, 0.9949037, 0.89806604, 0.8984879, 0.8988156, 0.89907104, 0.7999815,
                0.79998565, 0.7999888,
            ],
            helium_tissue_pressures: [
                0.2067617,
                0.29679793,
                0.3171508,
                0.29822788,
                0.2581302,
                0.21213704,
                0.16759425,
                0.12792979,
                0.095883876,
                0.07425135,
                0.059130467,
                0.046823382,
                0.037433997,
                0.030039186,
                0.023688126,
                0.018383523,
            ],
            total_tissue_pressures: [
                2.3502226, 2.4379637, 2.2374454, 1.9583642, 1.6894099, 1.476964, 1.347517,
                1.2172105, 1.0907875, 0.9723174, 0.9576184, 0.94563895, 0.936505, 0.83002067,
                0.8236738, 0.8183723,
            ],
            tolerated_ambient_pressures: [
                0.52357197, 0.89332557, 0.9466078, 0.89556754, 0.79117966, 0.7357851, 0.7116197,
                0.6620372, 0.59503824, 0.5327056, 0.5547682, 0.5759695, 0.6076899, 0.5217889,
                0.5406939, 0.5587929,
            ],
            a_values: [
                1.2987001, 1.0466264, 0.9085906, 0.8003015, 0.7057081, 0.5974157, 0.52402705,
                0.47104117, 0.4341974, 0.39314097, 0.36103684, 0.33203465, 0.29431748, 0.28252697,
                0.25991827, 0.23897184,
            ],
            b_values: [
                0.49791798, 0.64206254, 0.7123486, 0.77333254, 0.80428815, 0.83654886, 0.864151,
                0.8872479, 0.90625525, 0.9197639, 0.92991173, 0.938666, 0.9462811, 0.95305,
                0.9590928, 0.96443295,
            ],
            oxygen_at_pressure: 0.41599998,
            helium_at_pressure: 0.13,
            nitrogen_at_pressure: 0.75399995,
            dive_ceiling: -0.5339217,
        };
        let mut expected_dive_planner = DivePlanner {
            select_cylinder: SelectCylinder {
                selected_cylinder: Some(selectable_cylinder),
                cylinders: [cylinder, Default::default(), Default::default()],
                ..Default::default()
            },
            dive_stage: dive_stage_test_fixture(),
            decompression_steps: DecompressionSteps { dive_steps: vec![] },
            ..Default::default()
        };
        expected_dive_planner.dive_stage.dive_model.dive_profile = expected_dive_profile;
        let mut dive_planner = DivePlanner {
            select_cylinder: SelectCylinder {
                selected_cylinder: Some(selectable_cylinder),
                cylinders: [cylinder, Default::default(), Default::default()],
                ..Default::default()
            },
            dive_stage: dive_stage_test_fixture(),
            ..Default::default()
        };

        // When
        dive_planner.decompression_update_dive_profile();

        // Then
        assert_eq!(
            expected_dive_planner.decompression_steps,
            dive_planner.decompression_steps
        );
        assert_eq!(
            expected_dive_planner.dive_stage.dive_model.dive_profile,
            dive_planner.dive_stage.dive_model.dive_profile
        );
        assert_eq!(cylinder, dive_planner.select_cylinder.cylinders[0]);
        assert!(!dive_planner.dive_results.results.is_empty());
    }

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
        let expected_decompression_steps = DecompressionSteps {
            dive_steps: vec![
                DiveStep { depth: 6, time: 1 },
                DiveStep { depth: 3, time: 3 },
            ],
        };
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
        let dive_stage = dive_stage_test_fixture();
        let mut dive_planner = DivePlanner {
            select_cylinder,
            dive_stage,
            ..Default::default()
        };

        // When
        dive_planner.cylinder_selected(selectable_cylinder);

        // Then
        assert_eq!(expected_cylinder, dive_planner.dive_stage.cylinder);
        assert_eq!(
            expected_decompression_steps,
            dive_planner.decompression_steps
        );
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
        let default_array_old = [
            2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0,
        ];
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_default(default_array_old),
            dive_results: DiveResults {
                results: vec![dive_stage_test_fixture_default(default_array_old)],
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
            redo_buffer: vec![dive_stage_test_fixture_default(default_array_old)],
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
        let default_array_old = [
            2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0,
        ];
        let default_array_latest = [
            4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0,
        ];
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_default(default_array_latest),
            dive_results: DiveResults {
                results: vec![
                    dive_stage_test_fixture_default(default_array_old),
                    dive_stage_test_fixture_default(default_array_latest),
                ],
                ..Default::default()
            },
            redo_buffer: vec![],
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_default(default_array_old),
            dive_results: DiveResults {
                results: vec![dive_stage_test_fixture_default(default_array_old)],
                ..Default::default()
            },
            redo_buffer: vec![dive_stage_test_fixture_default(default_array_latest)],
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
        let default_array_old = [
            2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0,
        ];
        let default_array_latest = [
            4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0,
        ];
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_default(default_array_latest),
            dive_results: DiveResults {
                results: vec![
                    dive_stage_test_fixture_default(default_array_old),
                    dive_stage_test_fixture_default(default_array_latest),
                ],
                ..Default::default()
            },
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_default(default_array_latest),
            dive_results: DiveResults {
                results: vec![
                    dive_stage_test_fixture_default(default_array_old),
                    dive_stage_test_fixture_default(default_array_latest),
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
        let default_array_old = [
            2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0,
        ];
        let default_array_latest = [
            4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0,
        ];
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_default(default_array_old),
            dive_results: DiveResults {
                results: vec![dive_stage_test_fixture_default(default_array_old)],
                ..Default::default()
            },
            redo_buffer: vec![dive_stage_test_fixture_default(default_array_latest)],
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_default(default_array_latest),
            dive_results: DiveResults {
                results: vec![
                    dive_stage_test_fixture_default(default_array_old),
                    dive_stage_test_fixture_default(default_array_latest),
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
        let default_array_old = [
            2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0,
        ];
        let default_array_latest = [
            4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0,
        ];
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_default(default_array_old),
            dive_results: DiveResults {
                results: vec![],
                ..Default::default()
            },
            redo_buffer: vec![
                dive_stage_test_fixture_default(default_array_old),
                dive_stage_test_fixture_default(default_array_latest),
            ],

            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_default(default_array_latest),
            dive_results: DiveResults {
                results: vec![dive_stage_test_fixture_default(default_array_latest)],
                ..Default::default()
            },
            redo_buffer: vec![dive_stage_test_fixture_default(default_array_old)],
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

    fn dive_stage_test_fixture() -> DiveStage {
        let mut dive_model = DiveModel::create_zhl16_dive_model();
        dive_model.dive_profile = dive_profile_test_fixture();

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

    fn dive_profile_test_fixture() -> DiveProfile {
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

    fn dive_stage_test_fixture_default(default_array: [f32; 16]) -> DiveStage {
        DiveStage {
            dive_model: dive_model_test_fixture_default(default_array),
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

    fn dive_model_test_fixture_default(default_array: [f32; 16]) -> DiveModel {
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
