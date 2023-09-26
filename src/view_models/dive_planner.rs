use crate::{
    commands::{
        selectable_cylinder::SelectableCylinder,
        selectable_dive_model::SelectableDiveModel,
    },
    controllers::file::{read_dive_planner_state, upsert_dive_planner_state, upsert_dive_results},
    models::{
        central_nervous_system_toxicity::CentralNervousSystemToxicity, cylinder::Cylinder,
        decompression_steps::DecompressionSteps, dive_profile::DiveProfile, dive_stage::DiveStage,
        gas_mixture::GasMixture, results::DiveResults, select_cylinder::SelectCylinder,
        select_dive_model::SelectDiveModel,
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

    pub fn depth_changed(&mut self, depth: u32) {
        self.dive_stage.dive_step.depth = depth;
    }

    pub fn time_changed(&mut self, time: u32) {
        self.dive_stage.dive_step.time = time;
    }

    pub fn cylinder_changed(&mut self, cylinder: Cylinder) {
        self.dive_stage.cylinder = cylinder;
    }

    pub fn surface_air_consumption_changed(&mut self, surface_air_consumption_rate: u32) {
        self.dive_stage
            .cylinder
            .gas_management
            .surface_air_consumption_rate = surface_air_consumption_rate;
    }

    pub fn gas_mixture_changed(&mut self, gas_mixture: GasMixture) {
        self.dive_stage.cylinder.gas_mixture = gas_mixture;
    }

    pub fn cylinder_selected(&mut self, selectable_cylinder: SelectableCylinder) {
        self.select_cylinder
            .on_cylinder_selected(selectable_cylinder, &mut self.dive_stage.cylinder);
    }

    pub fn update_cylinder_selected(&mut self, selectable_cylinder: SelectableCylinder) {
        self.select_cylinder
                .update_cylinder_selected(selectable_cylinder, self.dive_stage.cylinder);
    }

    // TODO up to here

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

    // TODO test
    pub fn assign_decompression_steps(&mut self) {
        self.decompression_steps
            .assign_decompression_steps(self.dive_stage.calculate_decompression_dive_steps());
    }

    // TODO test
    fn assign_selected_cylinder(&mut self) {
        self.select_cylinder
            .assign_cylinder(self.dive_stage.cylinder);
    }

    // TODO test
    fn assign_dive_stage(&mut self, dive_stage: DiveStage) {
        self.dive_stage = dive_stage
    }

    fn add_result(&mut self) {
        self.dive_results.results.push(self.dive_stage);
        self.redo_buffer = Default::default();
    }

    // TODO test
    fn update_visibility(&mut self) {
        self.select_cylinder.read_only_view();
        self.dive_results.is_visible = true;
        self.decompression_steps.update_visibility();
    }
}

#[cfg(test)]
mod dive_step_view_should {
    use crate::models::{
        cylinder::Cylinder, dive_model::DiveModel, dive_profile::DiveProfile, dive_step::DiveStep,
        gas_management::GasManagement, gas_mixture::GasMixture,
    };

    use super::*;

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
    fn add_a_dive_stage_result() {
        // Given
        let dive_stage = dive_stage_test_fixture();
        let mut dive_planner = DivePlanner {
            dive_stage,
            dive_results: Default::default(),
            redo_buffer: vec![dive_stage_test_fixture()],
            ..Default::default()
        };

        // When
        dive_planner.add_result();

        // Then
        assert_eq!(1, dive_planner.dive_results.results.len());
        assert_eq!(dive_stage, dive_planner.dive_results.results[0]);
        assert_eq!(0, dive_planner.redo_buffer.len());
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
