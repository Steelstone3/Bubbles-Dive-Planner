use super::dive_step::DiveStep;

#[derive(PartialEq, Debug, Copy, Clone, Default)]
pub struct GasManagement {
    pub remaining: u32,
    pub used: u32,
    pub surface_air_consumption_rate: u32,
}

impl GasManagement {
    pub fn update_gas_management(&mut self, dive_step: DiveStep) {
        self.calculate_gas_used(dive_step);
        self.calculate_gas_remaining();
    }

    fn calculate_gas_used(&mut self, dive_step: DiveStep) {
        let pressure_at_depth = (dive_step.depth / 10) + 1;
        self.used = pressure_at_depth * dive_step.time * self.surface_air_consumption_rate;
    }

    fn calculate_gas_remaining(&mut self) {
        let is_out_of_air_supply = self.used > self.remaining;

        if is_out_of_air_supply {
            self.remaining = 0;
        } else {
            self.remaining -= self.used;
        }
    }
}

#[cfg(test)]
mod gas_management_should {
    use super::*;

    #[test]
    fn calculate_the_remaining_gas() {
        // Given
        let expected_gas_management = GasManagement {
            remaining: 1680,
            used: 720,
            ..Default::default()
        };
        let mut gas_management = GasManagement {
            remaining: 2400,
            used: 720,
            ..Default::default()
        };

        // When
        gas_management.calculate_gas_remaining();

        // Then
        assert_eq!(expected_gas_management, gas_management);
    }

    #[test]
    fn calculate_the_remaining_gas_is_zero_or_above() {
        // Given
        let expected_gas_management = GasManagement {
            remaining: 0,
            used: 720,
            ..Default::default()
        };
        let mut gas_management = GasManagement {
            remaining: 700,
            used: 720,
            ..Default::default()
        };

        // When
        gas_management.calculate_gas_remaining();

        // Then
        assert_eq!(expected_gas_management, gas_management);
    }

    #[test]
    fn calculate_the_used_gas() {
        // Given
        let expected_gas_management = GasManagement {
            used: 720,
            surface_air_consumption_rate: 12,
            ..Default::default()
        };
        let dive_step = DiveStep {
            depth: 50,
            time: 10,
        };
        let mut gas_management = GasManagement {
            used: 720,
            surface_air_consumption_rate: 12,
            ..Default::default()
        };

        // When
        gas_management.calculate_gas_used(dive_step);

        // Then
        assert_eq!(expected_gas_management, gas_management);
    }

    #[test]
    fn update_the_cylinder_gas_management() {
        // Given
        let expected_gas_management = GasManagement {
            used: 720,
            remaining: 1680,
            surface_air_consumption_rate: 12,
        };
        let dive_step = DiveStep {
            depth: 50,
            time: 10,
        };
        let mut gas_management = GasManagement {
            used: 0,
            remaining: 2400,
            surface_air_consumption_rate: 12,
        };

        // When
        gas_management.update_gas_management(dive_step);

        // Then
        assert_eq!(expected_gas_management, gas_management);
    }
}
