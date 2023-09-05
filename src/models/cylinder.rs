use super::gas_management::GasManagement;
use crate::models::gas_mixture::GasMixture;
use serde::{Deserialize, Serialize};
use std::fmt::Display;

pub const MAXIMUM_VOLUME_VALUE: u32 = 30;
pub const MINIMUM_VOLUME_VALUE: u32 = 3;
pub const MAXIMUM_PRESSURE_VALUE: u32 = 300;
pub const MINIMUM_PRESSURE_VALUE: u32 = 50;

#[derive(PartialEq, Debug, Clone, Copy, Default, Serialize, Deserialize)]
pub struct Cylinder {
    pub is_read_only: bool,
    pub volume: u32,
    pub pressure: u32,
    pub initial_pressurised_cylinder_volume: u32,
    pub gas_mixture: GasMixture,
    pub gas_management: GasManagement,
}

impl Cylinder {
    pub fn update_initial_pressurised_cylinder_volume(&mut self) {
        self.initial_pressurised_cylinder_volume = self.volume * self.pressure;
        self.gas_management.remaining = self.initial_pressurised_cylinder_volume;
    }

    pub fn validate(&self) -> bool {
        let volume_validation =
            self.volume < MINIMUM_VOLUME_VALUE || self.volume > MAXIMUM_VOLUME_VALUE;
        let pressure_validation =
            self.pressure < MINIMUM_PRESSURE_VALUE || self.pressure > MAXIMUM_PRESSURE_VALUE;
        if !self.gas_management.validate()
            || !self.gas_mixture.validate()
            || volume_validation
            || pressure_validation
        {
            return false;
        }

        true
    }

    pub fn display_cylinder_summary(&self) -> String {
        format!( "Cylinder\nVolume: {} (l) Pressure: {} (l)\nO2 (%): {} N (%): {} He (%): {}\nRemaining: {}/{} (l) Used: {} (l)" , self.volume, self.pressure, self.gas_mixture.oxygen, self.gas_mixture.nitrogen, self.gas_mixture.helium, self.gas_management.remaining, self.initial_pressurised_cylinder_volume, self.gas_management.used)
    }

    fn display_cylinder(&self) -> String {
        format!(
            "Cylinder\n\nVolume: {} (l)\nPressure: {} (bar)\n\nGas Mixture\n\nOxygen: {} (%)\nNitrogen: {} (%)\nHelium: {} (%)\n\nCylinder Management\n\nRemaining: {}/{} (l)\nUsed: {} (l)",
            self.volume,
            self.pressure,
            self.gas_mixture.oxygen,
            self.gas_mixture.nitrogen,
            self.gas_mixture.helium,
            self.gas_management.remaining,
            self.initial_pressurised_cylinder_volume,
            self.gas_management.used,
        )
    }
}

impl Display for Cylinder {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        write!(f, "{}", self.display_cylinder())
    }
}

#[cfg(test)]
mod cylinder_should {
    use super::*;
    use rstest::rstest;

    #[test]
    fn display_read_only_cylinder_summary() {
        // Given
        let cylinder = Cylinder {
            volume: 12,
            pressure: 200,
            initial_pressurised_cylinder_volume: 2400,
            gas_management: GasManagement {
                remaining: 1680,
                used: 720,
                surface_air_consumption_rate: 12,
            },
            is_read_only: true,
            gas_mixture: GasMixture {
                oxygen: 21,
                helium: 10,
                nitrogen: 69,
                maximum_operating_depth: 55.66,
            },
        };
        let expected_display = "Cylinder\nVolume: 12 (l) Pressure: 200 (l)\nO2 (%): 21 N (%): 69 He (%): 10\nRemaining: 1680/2400 (l) Used: 720 (l)";

        // When
        let display = cylinder.display_cylinder_summary();

        // Then
        assert_eq!(expected_display, display);
    }

    #[test]
    fn display_read_only_cylinder() {
        // Given
        let cylinder = Cylinder {
            volume: 12,
            pressure: 200,
            initial_pressurised_cylinder_volume: 2400,
            gas_management: GasManagement {
                remaining: 1680,
                used: 720,
                surface_air_consumption_rate: 12,
            },
            is_read_only: true,
            gas_mixture: GasMixture {
                oxygen: 21,
                helium: 10,
                nitrogen: 69,
                maximum_operating_depth: 55.66,
            },
        };
        let expected_display = "Cylinder\n\nVolume: 12 (l)\nPressure: 200 (bar)\n\nGas Mixture\n\nOxygen: 21 (%)\nNitrogen: 69 (%)\nHelium: 10 (%)\n\nCylinder Management\n\nRemaining: 1680/2400 (l)\nUsed: 720 (l)";

        // When
        let display = cylinder.display_cylinder();

        // Then
        assert_eq!(expected_display, display);
    }

    #[test]
    fn calculate_the_initial_pressurised_cylinder_volume() {
        // Given
        let expected_cylinder = Cylinder {
            volume: 12,
            pressure: 200,
            initial_pressurised_cylinder_volume: 2400,
            gas_management: GasManagement {
                remaining: 2400,
                ..Default::default()
            },
            ..Default::default()
        };
        let mut cylinder = Cylinder {
            volume: 12,
            pressure: 200,
            ..Default::default()
        };

        // When
        cylinder.update_initial_pressurised_cylinder_volume();

        // Then
        assert_eq!(expected_cylinder, cylinder);
    }

    #[rstest]
    #[case(12, 200, true)]
    #[case(31, 200, false)]
    #[case(2, 200, false)]
    #[case(12, 301, false)]
    #[case(12, 49, false)]
    fn validate_cylinder(#[case] volume: u32, #[case] pressure: u32, #[case] is_valid: bool) {
        // Given
        let cylinder = Cylinder {
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
        };

        // When
        let is_valid_actual: bool = cylinder.validate();

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
        let cylinder = Cylinder {
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
        };

        // When
        let is_valid_actual: bool = cylinder.validate();

        // Then
        assert_eq!(is_valid, is_valid_actual);
    }

    #[rstest]
    #[case(12, true)]
    #[case(31, false)]
    #[case(2, false)]
    fn validate_gas_management(#[case] surface_air_consumption_rate: u32, #[case] is_valid: bool) {
        // Given
        let cylinder = Cylinder {
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
        };

        // When
        let is_valid_actual: bool = cylinder.validate();

        // Then
        assert_eq!(is_valid, is_valid_actual);
    }
}
