use serde::{Deserialize, Serialize};

use crate::{application::input_parser::parse_input_u32, models::plan::dive_step::DiveStep};

pub const MAXIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE: u32 = 30;
pub const MINIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE: u32 = 3;

#[derive(PartialEq, Debug, Copy, Clone, Default, Serialize, Deserialize)]
pub struct GasManagement {
    pub remaining: u32,
    pub used: u32,
    pub surface_air_consumption_rate: u32,
}

impl GasManagement {}

#[cfg(test)]
mod gas_management_should {}
