#[derive(PartialEq, Debug, Copy, Clone, Default)]
pub struct GasManagement {
    pub remaining: u32,
    pub used: u32,
    pub surface_air_consumption_rate: u32,
}

impl GasManagement {}

#[cfg(test)]
mod gas_management_should {}
