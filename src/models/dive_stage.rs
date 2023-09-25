use super::{
    cylinder::Cylinder, dive_model::DiveModel, dive_profile::DiveProfile, dive_step::DiveStep,
};
use serde::{Deserialize, Serialize};

#[derive(Debug, PartialEq, Clone, Copy, Default, Serialize, Deserialize)]
pub struct DiveStage {
    pub dive_model: DiveModel,
    pub dive_step: DiveStep,
    pub cylinder: Cylinder,
}

impl DiveStage {
    pub fn validate(&self) -> bool {
        if !self.dive_step.validate() || !self.cylinder.validate() {
            return false;
        }

        true
    }

    pub fn calculate_decompression_dive_steps(&self) -> Vec<DiveStep> {
        let mut dive_steps = vec![];
        let mut dive_stage = *self;

        if dive_stage.dive_model.dive_profile.dive_ceiling <= 0.0 {
            return Default::default();
        }

        while dive_stage.dive_model.dive_profile.dive_ceiling > 0.0 {
            dive_stage.dive_step = DiveStep {
                depth: DiveStage::find_nearest_decompression_depth(
                    dive_stage.dive_model.dive_profile.dive_ceiling,
                ),
                time: 1,
            };

            dive_stage = DiveStage::calculate_decompression_time_at_depth(dive_stage);

            dive_steps.push(dive_stage.dive_step);
        }

        dive_steps
    }

    fn find_nearest_decompression_depth(dive_ceiling: f32) -> u32 {
        let step_interval = 3;

        if dive_ceiling <= 0.0 {
            return 0;
        }

        return (dive_ceiling / (step_interval as f32)).ceil() as u32 * step_interval;
    }

    fn calculate_decompression_time_at_depth(mut dive_stage: DiveStage) -> DiveStage {
        let mut time = 0;

        while dive_stage.dive_step.depth
            == DiveStage::find_nearest_decompression_depth(
                dive_stage.dive_model.dive_profile.dive_ceiling,
            )
        {
            dive_stage = DiveProfile::update_dive_profile(dive_stage);
            time += 1;
        }

        let dive_step = DiveStep {
            depth: dive_stage.dive_step.depth,
            time,
        };

        dive_stage.dive_step = dive_step;

        dive_stage
    }
}

#[cfg(test)]
mod dive_stage_should {
    use super::*;
    use crate::models::{gas_management::GasManagement, gas_mixture::GasMixture};
    use rstest::rstest;

    #[rstest]
    #[case(50, 10, true)]
    #[case(101, 10, false)]
    #[case(0, 10, false)]
    #[case(50, 61, false)]
    #[case(50, 0, false)]
    fn validate_dive_step(#[case] depth: u32, #[case] time: u32, #[case] is_valid: bool) {
        // Given
        let dive_stage = DiveStage {
            dive_step: DiveStep { depth, time },
            cylinder: Cylinder {
                volume: 12,
                pressure: 200,
                gas_mixture: GasMixture {
                    oxygen: 21,
                    helium: 0,
                    ..Default::default()
                },
                gas_management: GasManagement {
                    surface_air_consumption_rate: 12,
                    ..Default::default()
                },
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        let is_valid_actual = dive_stage.validate();

        // Then
        assert_eq!(is_valid, is_valid_actual);
    }

    #[rstest]
    #[case(12, 200, true)]
    #[case(31, 200, false)]
    #[case(2, 200, false)]
    #[case(12, 301, false)]
    #[case(12, 49, false)]
    fn validate_cylinder(#[case] volume: u32, #[case] pressure: u32, #[case] is_valid: bool) {
        // Given
        let dive_stage = DiveStage {
            dive_step: DiveStep {
                depth: 50,
                time: 10,
            },
            cylinder: Cylinder {
                volume,
                pressure,
                gas_mixture: GasMixture {
                    oxygen: 21,
                    helium: 0,
                    ..Default::default()
                },
                gas_management: GasManagement {
                    surface_air_consumption_rate: 12,
                    ..Default::default()
                },
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        let is_valid_actual = dive_stage.validate();

        // Then
        assert_eq!(is_valid, is_valid_actual);
    }

    #[rstest]
    #[case(21, 0, true)]
    #[case(101, 0, false)]
    #[case(4, 0, false)]
    #[case(21, 101, false)]
    #[case(51, 50, false)]
    #[case(50, 51, false)]
    fn validate_gas_mixture(#[case] oxygen: u32, #[case] helium: u32, #[case] is_valid: bool) {
        // Given
        let dive_stage = DiveStage {
            dive_step: DiveStep {
                depth: 50,
                time: 10,
            },
            cylinder: Cylinder {
                volume: 12,
                pressure: 200,
                gas_mixture: GasMixture {
                    oxygen,
                    helium,
                    ..Default::default()
                },
                gas_management: GasManagement {
                    surface_air_consumption_rate: 12,
                    ..Default::default()
                },
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        let is_valid_actual = dive_stage.validate();

        // Then
        assert_eq!(is_valid, is_valid_actual);
    }

    #[rstest]
    #[case(12, true)]
    #[case(31, false)]
    #[case(2, false)]
    fn validate_gas_management(#[case] surface_air_consumption_rate: u32, #[case] is_valid: bool) {
        // Given
        let dive_stage = DiveStage {
            dive_step: DiveStep {
                depth: 50,
                time: 10,
            },
            cylinder: Cylinder {
                volume: 12,
                pressure: 200,
                gas_mixture: GasMixture {
                    oxygen: 21,
                    helium: 0,
                    ..Default::default()
                },
                gas_management: GasManagement {
                    surface_air_consumption_rate,
                    ..Default::default()
                },
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        let is_valid_actual = dive_stage.validate();

        // Then
        assert_eq!(is_valid, is_valid_actual);
    }

    #[test]
    fn calculate_decompression_steps_where_dive_ceiling_is_zero() {
        // Given
        let expected_decompression_steps: Vec<DiveStep> = Default::default();
        let dive_stage = DiveStage {
            dive_model: DiveModel::create_zhl16_dive_model(),
            ..Default::default()
        };

        // When
        let decompression_steps = dive_stage.calculate_decompression_dive_steps();

        // Then
        assert_eq!(expected_decompression_steps, decompression_steps)
    }

    #[test]
    fn calculate_current_decompression_dive_steps() {
        // Given
        let expected_decompression_steps = vec![
            DiveStep { depth: 6, time: 1 },
            DiveStep { depth: 3, time: 3 },
        ];
        let mut dive_stage = DiveStage {
            dive_model: DiveModel::create_zhl16_dive_model(),
            dive_step: dive_step_test_fixture(),
            cylinder: cylinder_test_fixture(),
        };
        dive_stage.dive_model.dive_profile = dive_profile_test_fixture();

        // When
        let decompression_steps = dive_stage.calculate_decompression_dive_steps();

        // Then
        assert_eq!(expected_decompression_steps, decompression_steps)
    }

    #[rstest]
    #[case(-10.0, 0)]
    #[case(0.0, 0)]
    #[case(4.1, 6)]
    #[case(11.6, 12)]
    fn find_nearest_decompression_depth(
        #[case] dive_ceiling: f32,
        #[case] expected_dive_ceiling: u32,
    ) {
        // When
        let dive_ceiling = DiveStage::find_nearest_decompression_depth(dive_ceiling);

        // Then
        assert_eq!(expected_dive_ceiling, dive_ceiling)
    }

    #[test]
    fn calculate_decompression_time_at_depth() {
        // Given
        let expected_dive_step = DiveStep { depth: 6, time: 1 };
        let mut dive_stage = DiveStage {
            dive_model: DiveModel::create_zhl16_dive_model(),
            dive_step: expected_dive_step,
            cylinder: cylinder_test_fixture(),
        };
        dive_stage.dive_model.dive_profile = dive_profile_test_fixture();

        // When
        let dive_stage = DiveStage::calculate_decompression_time_at_depth(dive_stage);

        // Then
        assert_eq!(expected_dive_step, dive_stage.dive_step);
        assert_eq!(2.6097548, dive_stage.dive_model.dive_profile.dive_ceiling)
    }

    fn dive_step_test_fixture() -> DiveStep {
        DiveStep {
            depth: 50,
            time: 10,
        }
    }

    fn cylinder_test_fixture() -> Cylinder {
        Cylinder {
            gas_mixture: GasMixture {
                oxygen: 21,
                helium: 10,
                nitrogen: 69,
                maximum_operating_depth: 0.0,
            },
            initial_pressurised_cylinder_volume: 2400,
            volume: 12,
            pressure: 200,
            gas_management: GasManagement {
                remaining: 2400,
                used: 720,
                surface_air_consumption_rate: 12,
            },
            ..Default::default()
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
}
