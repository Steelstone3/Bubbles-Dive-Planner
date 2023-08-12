use crate::models::gas_mixture::GasMixture;

#[derive(PartialEq, Debug, Clone, Copy, Default)]
pub struct Cylinder {
    pub cylinder_volume: u32,
    pub cylinder_pressure: u32,
    pub initial_pressurised_cylinder_volume: u32,
    pub gas_mixture: GasMixture,
}

impl Cylinder {
    #[allow(dead_code)]
    pub fn update_initial_pressurised_cylinder_volume(&mut self) {
        self.initial_pressurised_cylinder_volume = self.cylinder_volume * self.cylinder_pressure;
    }
}

#[cfg(test)]
mod cylinder_should {
    use super::*;

    #[test]
    fn calculate_the_initial_pressurised_cylinder_volume() {
        // Given
        let expected_initial_pressurised_cylinder_volume = 2400;
        let mut cylinder = Cylinder {
            cylinder_volume: 12,
            cylinder_pressure: 200,
            ..Default::default()
        };

        // When
        cylinder.update_initial_pressurised_cylinder_volume();

        // Then
        assert_eq!(
            expected_initial_pressurised_cylinder_volume,
            cylinder.initial_pressurised_cylinder_volume
        );
    }
}
