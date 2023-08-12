use crate::models::gas_mixture::GasMixture;

use super::gas_management::GasManagement;

#[derive(PartialEq, Debug, Clone, Copy, Default)]
pub struct Cylinder {
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
}

#[cfg(test)]
mod cylinder_should {
    use super::*;

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
        assert_eq!(
            expected_cylinder,
            cylinder
        );
    }
}
