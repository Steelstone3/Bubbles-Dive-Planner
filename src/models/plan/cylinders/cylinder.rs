use serde::{Deserialize, Serialize};

use crate::{
    application::input_parser::parse_input_u32,
    models::plan::cylinders::gas_management::GasManagement,
};

use super::gas_mixture::GasMixture;

pub const MAXIMUM_VOLUME_VALUE: u32 = 30;
pub const MINIMUM_VOLUME_VALUE: u32 = 3;
pub const MAXIMUM_PRESSURE_VALUE: u32 = 300;
pub const MINIMUM_PRESSURE_VALUE: u32 = 50;

#[derive(PartialEq, Debug, Clone, Copy, Default, Serialize, Deserialize)]
pub struct Cylinder {
    pub volume: u32,
    pub pressure: u32,
    pub initial_pressurised_cylinder_volume: u32,
    pub gas_mixture: GasMixture,
    pub gas_management: GasManagement,
}

impl Cylinder {}

#[cfg(test)]
mod cylinder_should {}
