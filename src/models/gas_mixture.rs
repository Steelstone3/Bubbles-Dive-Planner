use serde::{Deserialize, Serialize};

#[derive(PartialEq, Debug, Copy, Clone, Serialize, Deserialize, Default)]
pub struct GasMixture {
    pub oxygen: u32,
    pub helium: u32,
    pub nitrogen: u32,
}

#[cfg(test)]
mod gas_mixture_should {
    #[test]
    #[ignore = "not implemented"]
    fn update() {}
}
