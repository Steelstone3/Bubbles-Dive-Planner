use crate::models::gas_mixture::GasMixture;
use serde::{Deserialize, Serialize};

#[derive(PartialEq, Debug, Clone, Copy, Serialize, Deserialize, Default)]
pub struct Cylinder {
    pub gas_mixture: GasMixture,
}
