use crate::models::gas_management::GasManagement;
use crate::models::gas_mixture::GasMixture;
use serde::{Deserialize, Serialize};

#[derive(PartialEq, Debug, Clone, Copy, Serialize, Deserialize, Default)]
pub struct Cylinder {
    pub cylinder_volume: u32,
    pub cylinder_pressure: u32,
    pub initial_pressurised_cylinder_volume: u32,
    pub gas_mixture: GasMixture,
    pub gas_management: GasManagement,
}

#[cfg(test)]
mod cylinder_should {
    #[test]
    #[ignore = "not implemented"]
    fn calculate_initial_pressurised_cylinder_volume() {}

    #[test]
    #[ignore = "not implemented"]
    fn update_cylinder_gas_usage() {}
}
