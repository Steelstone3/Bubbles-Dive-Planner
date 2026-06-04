use serde::{Deserialize, Serialize};

use crate::{
    application::input_parser::parse_input_u32,
    models::plan::{cylinders::gas_management::GasManagement, dive_step::DiveStep},
};

use super::gas_mixture::GasMixture;

#[derive(PartialEq, Debug, Clone, Serialize, Deserialize)]
pub struct Cylinder {
    pub volume: u32,
    pub pressure: u32,
    initial_pressurised_cylinder_volume: u32,
    pub gas_mixture: GasMixture,
    pub gas_management: GasManagement,
}

impl Default for Cylinder {
    fn default() -> Self {
        let gas_mixture = GasMixture::new(21,0);
        Self::new(3, 50, gas_mixture, 12)
    }
}

impl Cylinder {
    // TODO test
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

    // TODO test, speculative generality
    pub fn update_gas_management(&self, dive_step: &DiveStep) -> Cylinder {
        Cylinder {
            volume: self.volume,
            pressure: self.pressure,
            initial_pressurised_cylinder_volume: self.initial_pressurised_cylinder_volume,
            gas_mixture: self.gas_mixture.clone(),
            gas_management: self.gas_management.update_gas_management(dive_step),
        }
    }

    // TODO test
    pub fn update_cylinder_volume(&self, volume: String) -> Self {
        const MINIMUM_VOLUME_VALUE: u32 = 3;
        const MAXIMUM_VOLUME_VALUE: u32 = 30;

        let volume = parse_input_u32(volume, MINIMUM_VOLUME_VALUE, MAXIMUM_VOLUME_VALUE);

        Self::new(
            volume,
            self.pressure,
            self.gas_mixture.clone(),
            self.gas_management.get_surface_air_consumption_rate(),
        )
    }

    // TODO test
    pub fn update_cylinder_pressure(&self, pressure: String) -> Self {
        const MINIMUM_PRESSURE_VALUE: u32 = 50;
        const MAXIMUM_PRESSURE_VALUE: u32 = 300;

        let pressure = parse_input_u32(pressure, MINIMUM_PRESSURE_VALUE, MAXIMUM_PRESSURE_VALUE);

        Self::new(
            self.volume,
            pressure,
            self.gas_mixture.clone(),
            self.gas_management.get_surface_air_consumption_rate(),
        )
    }

    // TODO test
    pub fn get_initial_pressurised_cylinder_volume(&self) -> u32 {
        self.initial_pressurised_cylinder_volume
    }

    fn initial_pressurised_cylinder_volume(volume: u32, pressure: u32) -> u32 {
        volume * pressure
    }
}

#[cfg(test)]
mod cylinder_should {}
