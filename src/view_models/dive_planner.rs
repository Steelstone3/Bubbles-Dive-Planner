use crate::models::dive_stage::DiveStage;
use iced::Sandbox;
use serde::{Deserialize, Serialize};

#[derive(Debug, Copy, Clone, PartialEq, Serialize, Deserialize)]
pub struct DivePlanner {
    pub dive_stage: DiveStage,
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
}

#[cfg(test)]
mod dive_step_view_should {
    use crate::{
        commands::selectable_dive_model::SelectableDiveModel,
        models::{
            cylinder::Cylinder, dive_model::DiveModel, dive_profile::DiveProfile,
            dive_step::DiveStep, gas_management::GasManagement, gas_mixture::GasMixture,
            select_dive_model::SelectDiveModel,
        },
    };

    use super::*;

    #[test]
    fn reset_dive_planner_to_default_state() {
        // Given

        let expected = DivePlanner::default();
        let mut dive_planner = DivePlanner {
            dive_stage: DiveStage {
                dive_model: SelectDiveModel {
                    dive_model_list: [dive_model_test_fixture(), dive_model_test_fixture()],
                    dive_model: dive_model_test_fixture(),
                    selected_dive_model: Some(SelectableDiveModel::Usn),
                },
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
                    },
                    gas_management: GasManagement {
                        remaining: 1680,
                        used: 720,
                        surface_air_consumption_rate: 12,
                    },
                },
            },
        };

        // When
        dive_planner.reset();

        // Then
        assert_eq!(expected, dive_planner);
    }

    fn dive_model_test_fixture() -> DiveModel {
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
            },
        }
    }
}
