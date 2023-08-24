use serde::{Deserialize, Serialize};

pub const MAXIMUM_OXYGEN_VALUE: u32 = 100;
pub const MINIMUM_OXYGEN_VALUE: u32 = 5;
pub const MAXIMUM_HELIUM_VALUE: u32 = 100;
pub const MINIMUM_HELIUM_VALUE: u32 = 0;
const DEFAULT_NITROGEN_VALUE: u32 = 100;

#[derive(PartialEq, Debug, Copy, Clone, Serialize, Deserialize)]
pub struct GasMixture {
    pub oxygen: u32,
    pub helium: u32,
    pub nitrogen: u32,
}

impl Default for GasMixture {
    fn default() -> Self {
        Self {
            oxygen: 0,
            helium: 0,
            nitrogen: DEFAULT_NITROGEN_VALUE,
        }
    }
}

impl GasMixture {
    pub fn update_nitrogen(&mut self) {
        self.nitrogen = DEFAULT_NITROGEN_VALUE - self.oxygen - self.helium
    }

    pub fn validate(&self) -> bool {
        let oxygen_validation =
            self.oxygen < MINIMUM_OXYGEN_VALUE || self.oxygen > MAXIMUM_OXYGEN_VALUE;
        let helium_validation = self.helium > MAXIMUM_OXYGEN_VALUE;
        let gas_mixture_validation = self.helium + self.oxygen > 100;

        if oxygen_validation || helium_validation || gas_mixture_validation {
            return false;
        }

        return true;
    }
}

#[cfg(test)]
mod gas_mixture_should {
    use rstest::rstest;

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

    #[rstest]
    #[case(21, 0, 79, true)]
    #[case(101, 0, 0, false)]
    #[case(4, 0, 0, false)]
    #[case(21, 101, 0, false)]
    #[case(50, 51, 0, false)]
    fn validate(
        #[case] oxygen: u32,
        #[case] helium: u32,
        #[case] nitrogen: u32,
        #[case] is_valid: bool,
    ) {
        // Given
        let gas_mixture = GasMixture {
            oxygen,
            helium,
            nitrogen,
        };

        // When
        let is_valid_actual = gas_mixture.validate();

        // Then
        assert_eq!(is_valid, is_valid_actual);
    }
}
