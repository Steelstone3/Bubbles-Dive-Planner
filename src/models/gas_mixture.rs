use serde::{Deserialize, Serialize};

#[derive(PartialEq, Debug, Copy, Clone, Serialize, Deserialize)]
pub struct GasMixture {
    pub oxygen: u32,
    pub helium: u32,
    pub nitrogen: u32,
}

impl Default for GasMixture {
    fn default() -> Self {
        Self {
            oxygen: 21,
            helium: 0,
            nitrogen: 79,
        }
    }
}

#[cfg(test)]
mod gas_mixture_should {
    #[test]
    #[ignore = "not implemented"]
    fn update() {}
}
