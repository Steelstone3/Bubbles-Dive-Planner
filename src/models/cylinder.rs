use crate::models::gas_mixture::GasMixture;

#[derive(PartialEq, Debug, Clone, Copy, Default)]
pub struct Cylinder {
    pub gas_mixture: GasMixture,
}
