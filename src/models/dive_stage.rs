use super::{cylinder::Cylinder, dive_model::DiveModel, dive_step::DiveStep};
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
        todo!();
    }
}

#[cfg(test)]
mod dive_stage_should {
    use rstest::rstest;

    use super::*;
    use crate::models::{gas_management::GasManagement, gas_mixture::GasMixture};

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
}
