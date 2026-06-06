use crate::{application::input_parser::parse_input_u32, models::plan::dive_step::DiveStep};
use serde::{Deserialize, Serialize};

pub const MAXIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE: u32 = 30;
pub const MINIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE: u32 = 3;

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
        used: u32,
        surface_air_consumption_rate: u32,
    ) -> Self {
        Self {
            remaining: initial_pressurised_cylinder_volume,
            used,
            surface_air_consumption_rate,
        }
    }

    // TODO test
    pub fn is_valid(&self) -> bool {
        if self.surface_air_consumption_rate > MAXIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE {
            return false;
        } else if self.surface_air_consumption_rate < MINIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE {
            return false;
        }

        true
    }

    // TODO test
    pub fn update_surface_air_consumption_rate(
        &self,
        surface_air_consumption_rate: String,
    ) -> Self {
        let surface_air_consumption_rate = parse_input_u32(
            surface_air_consumption_rate,
            MINIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE,
            MAXIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE,
        );

        GasManagement::new(self.remaining, self.used, surface_air_consumption_rate)
    }

    // TODO test
    pub fn update_gas_management(&self, dive_step: &DiveStep) -> GasManagement {
        let pressure_at_depth = (dive_step.depth / 10) + 1;
        let used = pressure_at_depth * dive_step.time * self.surface_air_consumption_rate;

        if used > self.remaining {
            // is out of air
            Self::new(0, used, self.surface_air_consumption_rate)
        } else {
            let remaining = self.remaining - used;
            Self::new(remaining, used, self.surface_air_consumption_rate)
        }
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
