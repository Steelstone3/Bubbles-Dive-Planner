use super::gas_mixture::GasMixture;
use crate::{
    application::input_parser::parse_input_u32,
    models::plan::{cylinders::gas_management::GasManagement, dive_step::DiveStep},
};
use serde::{Deserialize, Serialize};

const MAXIMUM_VOLUME_VALUE: u32 = 30;
const MAXIMUM_PRESSURE_VALUE: u32 = 300;
const MINIMUM_VOLUME_VALUE: u32 = 3;
const MINIMUM_PRESSURE_VALUE: u32 = 50;

#[derive(PartialEq, Debug, Clone, Serialize, Deserialize)]
pub struct Cylinder {
    volume: u32,
    pressure: u32,
    initial_pressurised_cylinder_volume: u32,
    pub gas_mixture: GasMixture,
    pub gas_management: GasManagement,
}

impl Default for Cylinder {
    fn default() -> Self {
        let gas_mixture = GasMixture::new(21, 0);
        Self::new(12, 200, gas_mixture, 12)
    }
}

impl Cylinder {
    pub fn new(
        volume: u32,
        pressure: u32,
        gas_mixture: GasMixture,
        surface_air_consumption_rate: u32,
    ) -> Self {
        let initial_pressurised_cylinder_volume =
            Cylinder::initial_pressurised_cylinder_volume(volume, pressure);

        Self {
            volume,
            pressure,
            initial_pressurised_cylinder_volume,
            gas_mixture,
            gas_management: GasManagement::new(
                initial_pressurised_cylinder_volume,
                0,
                surface_air_consumption_rate,
            ),
        }
    }

    pub fn update_gas_management(&self, dive_step: &DiveStep) -> Cylinder {
        Cylinder {
            volume: self.volume,
            pressure: self.pressure,
            initial_pressurised_cylinder_volume: self.initial_pressurised_cylinder_volume,
            gas_mixture: self.gas_mixture.clone(),
            gas_management: self.gas_management.update_gas_management(dive_step),
        }
    }

    pub fn update_cylinder_volume(&self, volume: String) -> Self {
        let volume = parse_input_u32(volume, MINIMUM_VOLUME_VALUE, MAXIMUM_VOLUME_VALUE);

        Self::new(
            volume,
            self.pressure,
            self.gas_mixture.clone(),
            self.gas_management.get_surface_air_consumption_rate(),
        )
    }

    pub fn update_cylinder_pressure(&self, pressure: String) -> Self {
        let pressure = parse_input_u32(pressure, MINIMUM_PRESSURE_VALUE, MAXIMUM_PRESSURE_VALUE);

        Self::new(
            self.volume,
            pressure,
            self.gas_mixture.clone(),
            self.gas_management.get_surface_air_consumption_rate(),
        )
    }

    pub fn get_volume(&self) -> u32 {
        self.volume
    }

    pub fn get_pressure(&self) -> u32 {
        self.pressure
    }

    pub fn get_initial_pressurised_cylinder_volume(&self) -> u32 {
        self.initial_pressurised_cylinder_volume
    }

    pub fn is_valid(&self) -> bool {
        if self.volume > MAXIMUM_VOLUME_VALUE
            || self.volume < MINIMUM_VOLUME_VALUE
            || self.pressure > MAXIMUM_PRESSURE_VALUE
            || self.pressure < MINIMUM_PRESSURE_VALUE
            || !self.gas_mixture.is_valid()
            || !self.gas_management.is_valid()
        {
            return false;
        }

        true
    }

    fn initial_pressurised_cylinder_volume(volume: u32, pressure: u32) -> u32 {
        volume * pressure
    }
}

#[cfg(test)]
mod cylinder_should {
    use crate::models::plan::cylinders::cylinder::Cylinder;
    use crate::models::plan::cylinders::gas_management::GasManagement;
    use crate::models::plan::cylinders::gas_mixture::GasMixture;
    use crate::models::plan::dive_step::DiveStep;
    use rstest::rstest;

    #[test]
    fn test_update_gas_management() {
        // Given
        let dive_step = DiveStep::new(50, 10);
        let original_cylinder = Cylinder::new(12, 200, GasMixture::new(21, 0), 12);
        let expected_cylinder = Cylinder {
            volume: 12,
            pressure: 200,
            initial_pressurised_cylinder_volume: 2400,
            gas_mixture: GasMixture::new(21, 0),
            gas_management: GasManagement::new(1680, 720, 12),
        };

        // When
        let cylinder = original_cylinder.update_gas_management(&dive_step);

        // Then
        pretty_assertions::assert_eq!(expected_cylinder, cylinder);
    }

    #[test]
    fn test_update_cylinder_volume() {
        // Given
        let volume = "15".to_string();
        let original_cylinder = Cylinder::new(12, 200, GasMixture::new(21, 0), 12);
        let expected_cylinder = Cylinder::new(15, 200, GasMixture::new(21, 0), 12);

        // When
        let cylinder = original_cylinder.update_cylinder_volume(volume.to_string());

        // Then
        pretty_assertions::assert_eq!(expected_cylinder, cylinder);
    }

    #[test]
    fn test_update_cylinder_pressure() {
        // Given
        let pressure = "300".to_string();
        let original_cylinder = Cylinder::new(12, 200, GasMixture::new(21, 0), 12);
        let expected_cylinder = Cylinder::new(12, 300, GasMixture::new(21, 0), 12);

        // When
        let cylinder = original_cylinder.update_cylinder_pressure(pressure.to_string());

        // Then
        pretty_assertions::assert_eq!(expected_cylinder, cylinder);
    }

    #[test]
    fn test_get_volume() {
        // Given
        let expected_volume = 12;
        let cylinder = Cylinder::new(expected_volume, 200, GasMixture::new(21, 0), 12);

        // When
        let volume = cylinder.get_volume();

        // Then
        pretty_assertions::assert_eq!(expected_volume, volume);
    }

    #[test]
    #[ignore]
    fn test_get_pressure() {}

    #[test]
    #[ignore]
    fn test_get_initial_pressurised_cylinder_volume() {}

    #[rstest]
    #[case(12, 200, true)]
    #[case(30, 300, true)]
    #[case(3, 50, true)]
    #[case(31, 200, false)]
    #[case(2, 200, false)]
    #[case(12, 301, false)]
    #[case(12, 49, false)]
    fn test_is_valid(#[case] volume: u32, #[case] pressure: u32, #[case] is_valid: bool) {
        // Given
        let cylinder = Cylinder::new(volume, pressure, GasMixture::default(), 12);

        // When
        let is_valid_actual = cylinder.is_valid();

        // Then
        assert_eq!(is_valid, is_valid_actual);
    }
}
