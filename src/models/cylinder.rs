use super::dive_step::DiveStep;
use crate::models::gas_management::GasManagement;
use crate::models::gas_mixture::GasMixture;
use crate::presenters::presenter::{confirmation, parse_numeric_value, text_prompt};
use inquire::Select;
use serde::{Deserialize, Serialize};
use std::fmt::Display;

#[derive(PartialEq, Debug, Clone, Copy, Serialize, Deserialize, Default)]
pub struct Cylinder {
    pub cylinder_volume: u32,
    pub cylinder_pressure: u32,
    pub initial_pressurised_cylinder_volume: u32,
    pub gas_mixture: GasMixture,
    pub gas_management: GasManagement,
}

impl Cylinder {
    pub fn new_collection() -> Vec<Cylinder> {
        let mut cylinders: Vec<Cylinder> = vec![Cylinder::new()];

        while confirmation("Create cylinder:") {
            cylinders.push(Cylinder::new());
        }

        cylinders
    }

    pub fn select(cylinders: Vec<Cylinder>) -> Cylinder {
        Select::new("Select cylinder:", cylinders).prompt().unwrap()
    }

    pub fn update_gas_usage(mut cylinder: Cylinder, dive_step: DiveStep) -> Cylinder {
        cylinder.gas_management = cylinder.gas_management.update_gas_management(dive_step);

        cylinder
    }

    fn new() -> Self {
        let cylinder_volume = parse_numeric_value(text_prompt(
            "Enter cylinder volume (L):",
            "Enter a value 3 - 30",
            "12",
        ));

        let cylinder_pressure = parse_numeric_value(text_prompt(
            "Enter cylinder pressure (BAR):",
            "Enter a value 50 - 300",
            "200",
        ));

        let initial_pressurised_cylinder_volume =
            Self::calculate_initial_pressurised_cylinder_volume(cylinder_volume, cylinder_pressure);

        Self {
            cylinder_volume,
            cylinder_pressure,
            initial_pressurised_cylinder_volume,
            gas_mixture: GasMixture::new(),
            gas_management: GasManagement::new(),
        }
    }

    fn calculate_initial_pressurised_cylinder_volume(
        cylinder_volume: u32,
        cylinder_pressure: u32,
    ) -> u32 {
        cylinder_volume * cylinder_pressure
    }
}

impl Display for Cylinder {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        write!(
            f,
            "Mix: Oxygen: {}%, Nitrogen: {}%, Helium: {}% Initial Pressurised Volume: {}L Gas Used: {}L",
            self.gas_mixture.oxygen,
            self.gas_mixture.nitrogen,
            self.gas_mixture.helium,
            self.initial_pressurised_cylinder_volume,
            self.gas_management.gas_used
        )
    }
}

#[cfg(test)]
mod cylinder_should {
    use super::*;

    #[test]
    fn calculate_initial_pressurised_cylinder_volume() {
        //Arrange
        let cylinder_volume = 12;
        let cylinder_pressure = 200;
        let expected_pressurised_cylinder_volume = 2400;

        //Act
        let result = Cylinder::calculate_initial_pressurised_cylinder_volume(
            cylinder_volume,
            cylinder_pressure,
        );

        //Assert
        assert_eq!(expected_pressurised_cylinder_volume, result);
    }

    #[test]
    fn update_cylinder_gas_usage() {
        //Arrange
        let dive_step = dive_step_test_fixture();
        let expected_cylinder = cylinder_test_fixture();

        //Act
        let cylinder = Cylinder::update_gas_usage(expected_cylinder, dive_step);

        //Assert
        assert_eq!(expected_cylinder, cylinder);
    }

    fn dive_step_test_fixture() -> DiveStep {
        DiveStep {
            depth: 50,
            time: 10,
        }
    }

    fn cylinder_test_fixture() -> Cylinder {
        Cylinder {
            cylinder_volume: 12,
            cylinder_pressure: 200,
            initial_pressurised_cylinder_volume: 2400,
            gas_mixture: GasMixture {
                oxygen: 21,
                helium: 10,
                nitrogen: 69,
            },
            gas_management: GasManagement {
                gas_used: 720,
                surface_air_consumption_rate: 12,
            },
        }
    }
}
