use crate::models::{
    central_nervous_system_toxicity::CentralNervousSystemToxicity, dive_stage::DiveStage,
    dive_step::DiveStep, select_cylinder::SelectCylinder, select_dive_model::SelectDiveModel,
};
use iced::Sandbox;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Serialize, Deserialize)]
pub struct DivePlanner {
    pub select_dive_model: SelectDiveModel,
    pub select_cylinder: SelectCylinder,
    pub dive_stage: DiveStage,
    pub results: Vec<DiveStage>,
    pub decompression_steps: Vec<DiveStep>,
    pub cns_toxicity: CentralNervousSystemToxicity,
    pub redo_buffer: Vec<DiveStage>,
}

impl Default for DivePlanner {
    fn default() -> Self {
        Self::new()
    }
}

impl DivePlanner {
    pub fn reset(&mut self) {
        *self = DivePlanner::default();
    }

    pub fn add_result(&mut self) {
        self.results.push(self.dive_stage);
        self.redo_buffer = Default::default();
    }

    pub fn undo(&mut self) {
        if self.is_undoable() {
            let latest = self.dive_stage;
            self.redo_buffer.push(latest);
            self.results.pop();
            self.dive_stage = *self.results.last().unwrap_or(&Default::default());
        } else {
            self.reset();
        }
    }

    pub fn redo(&mut self) {
        if self.is_redoable() {
            let redo = self.redo_buffer.pop().unwrap();
            self.results.push(redo);
            self.dive_stage = redo;
        }
    }

    pub fn is_undoable(&self) -> bool {
        !self.results.is_empty()
    }

    pub fn is_redoable(&self) -> bool {
        !self.redo_buffer.is_empty()
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
            results: vec![
                dive_stage_test_fixture(),
                dive_stage_test_fixture(),
                dive_stage_test_fixture(),
            ],
            redo_buffer: vec![dive_stage_test_fixture()],
            ..Default::default()
        };

        // When
        dive_planner.reset();

        // Then
        assert_eq!(expected, dive_planner);
    }

    #[test]
    fn add_a_dive_stage_result() {
        // Given
        let dive_stage = dive_stage_test_fixture();
        let mut dive_planner = DivePlanner {
            dive_stage,
            results: Default::default(),
            redo_buffer: vec![dive_stage_test_fixture()],
            ..Default::default()
        };

        // When
        dive_planner.add_result();

        // Then
        assert_eq!(1, dive_planner.results.len());
        assert_eq!(dive_stage, dive_planner.results[0]);
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
            results: vec![],
            redo_buffer: vec![],
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: Default::default(),
            results: vec![],
            redo_buffer: vec![],
            ..Default::default()
        };

        // When
        dive_planner.undo();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn undo_a_dive_stage_with_one_result() {
        // Given
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_old(),
            results: vec![dive_stage_test_fixture_old()],
            redo_buffer: vec![],
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: Default::default(),
            results: vec![],
            redo_buffer: vec![dive_stage_test_fixture_old()],
            ..Default::default()
        };

        // When
        dive_planner.undo();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn undo_a_dive_stage_with_multiple_results() {
        // Given
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_latest(),
            results: vec![
                dive_stage_test_fixture_old(),
                dive_stage_test_fixture_latest(),
            ],
            redo_buffer: vec![],
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_old(),
            results: vec![dive_stage_test_fixture_old()],
            redo_buffer: vec![dive_stage_test_fixture_latest()],
            ..Default::default()
        };

        // When
        dive_planner.undo();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn redo_when_buffer_is_empty() {
        // Given
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_latest(),
            results: vec![
                dive_stage_test_fixture_old(),
                dive_stage_test_fixture_latest(),
            ],
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_latest(),
            results: vec![
                dive_stage_test_fixture_old(),
                dive_stage_test_fixture_latest(),
            ],
            ..Default::default()
        };

        // When
        dive_planner.redo();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn redo_dive_stage() {
        // Given
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_old(),
            results: vec![dive_stage_test_fixture_old()],
            redo_buffer: vec![dive_stage_test_fixture_latest()],
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_latest(),
            results: vec![
                dive_stage_test_fixture_old(),
                dive_stage_test_fixture_latest(),
            ],
            ..Default::default()
        };

        // When
        dive_planner.redo();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn redo_multiple_dive_stages() {
        // Given
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_old(),
            results: vec![],
            redo_buffer: vec![
                dive_stage_test_fixture_old(),
                dive_stage_test_fixture_latest(),
            ],
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_latest(),
            results: vec![dive_stage_test_fixture_latest()],
            redo_buffer: vec![dive_stage_test_fixture_old()],
            ..Default::default()
        };

        // When
        dive_planner.redo();

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
        }
    }
}
