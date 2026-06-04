use serde::{Deserialize, Serialize};

use crate::application::{
    input_parser::parse_input_u32, messages::message::Message::SurfaceAirConsumptionOnChanged,
};

#[derive(PartialEq, Debug, Clone, Default, Serialize, Deserialize)]
pub struct GasManagement {
    remaining: u32,
    used: u32,
    surface_air_consumption_rate: u32,
}

impl GasManagement {
    // TODO test
    pub fn new(
        initial_pressurised_cylinder_volume: u32,
        surface_air_consumption_rate: u32,
    ) -> Self {
        Self {
            remaining: initial_pressurised_cylinder_volume,
            used: 0,
            surface_air_consumption_rate,
        }
    }

    // TODO test
    pub fn update_surface_air_consumption_rate(
        &self,
        surface_air_consumption_rate: String,
    ) -> Self {
        pub const MINIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE: u32 = 3;
        pub const MAXIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE: u32 = 30;

        let surface_air_consumption_rate = parse_input_u32(
            surface_air_consumption_rate,
            MINIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE,
            MAXIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE,
        );

        GasManagement::new(self.remaining, surface_air_consumption_rate)
    }

    // TODO test
    pub fn get_remaining(&self) -> u32 {
        self.remaining
    }

    // TODO test
    pub fn get_used(&self) -> u32 {
        self.used
    }

    // TODO test
    pub fn get_surface_air_consumption_rate(&self) -> u32 {
        self.surface_air_consumption_rate
    }
}

#[cfg(test)]
mod gas_management_should {}
