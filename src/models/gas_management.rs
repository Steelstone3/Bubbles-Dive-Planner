use super::dive_step::DiveStep;
use crate::presenters::presenter::{parse_numeric_value, text_prompt};
use serde::{Deserialize, Serialize};

#[derive(PartialEq, Debug, Copy, Clone, Serialize, Deserialize, Default)]
pub struct GasManagement {
    pub gas_used: u32,
    pub surface_air_consumption_rate: u32,
}

impl GasManagement {
    pub fn new() -> Self {
        Self {
            gas_used: 0,
            surface_air_consumption_rate: parse_numeric_value(text_prompt(
                "Enter surface air consumption rate (L/min):",
                "Enter a value 3 - 30",
                "12",
            )),
        }
    }

    pub fn update_gas_management(self, dive_step: DiveStep) -> Self {
        let gas_used =
            GasManagement::calculate_gas_used(dive_step, self.surface_air_consumption_rate);
        GasManagement {
            gas_used,
            surface_air_consumption_rate: self.surface_air_consumption_rate,
        }
    }

    fn calculate_gas_used(dive_step: DiveStep, surface_air_consumption_rate: u32) -> u32 {
        ((dive_step.depth / 10) + 1) * dive_step.time * surface_air_consumption_rate
    }
}

#[cfg(test)]
mod gas_management_should {
    use super::*;

    #[test]
    fn update_gas_management() {
        //Arrange
        let dive_step = dive_step_test_fixture();
        let gas_management = gas_management_test_fixture();

        //Act
        let result = GasManagement::update_gas_management(gas_management, dive_step);

        //Assert
        assert_eq!(720, result.gas_used);
    }

    fn dive_step_test_fixture() -> DiveStep {
        DiveStep {
            depth: 50,
            time: 10,
        }
    }

    fn gas_management_test_fixture() -> GasManagement {
        GasManagement {
            surface_air_consumption_rate: 12,
            gas_used: 0,
        }
    }
}
