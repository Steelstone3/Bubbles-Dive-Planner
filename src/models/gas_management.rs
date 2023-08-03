use serde::{Deserialize, Serialize};

#[derive(PartialEq, Debug, Copy, Clone, Serialize, Deserialize, Default)]
pub struct GasManagement {
    pub gas_used: u32,
    pub surface_air_consumption_rate: u32,
}

#[cfg(test)]
mod gas_management_should {
    #[test]
    #[ignore = "not implemented"]
    fn update() {}
}
