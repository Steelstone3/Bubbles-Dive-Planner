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

impl GasMixture {
    pub fn update_nitrogen(&mut self) {
        self.nitrogen = 100 - self.oxygen - self.helium
    }
}

#[cfg(test)]
mod gas_mixture_should {
    use super::*;

    #[test]
    fn calculate_nitrogen_for_a_given_gas_mixture() {
        // Given
        let mut gas_mixture = GasMixture {
            oxygen: 21,
            helium: 10,
            nitrogen: 0,
        };

        // When
        gas_mixture.update_nitrogen();

        // Then
        assert_eq!(69, gas_mixture.nitrogen);
    }
}
