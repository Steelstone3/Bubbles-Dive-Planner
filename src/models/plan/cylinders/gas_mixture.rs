use crate::application::input_parser::parse_input_u32;
use serde::{Deserialize, Serialize};

const MAXIMUM_OXYGEN_VALUE: u32 = 100;
const MAXIMUM_HELIUM_VALUE: u32 = 100;
const MINIMUM_OXYGEN_VALUE: u32 = 5;
const MINIMUM_HELIUM_VALUE: u32 = 0;

#[derive(PartialEq, Debug, Clone, Serialize, Deserialize)]
pub struct GasMixture {
    pub oxygen: u32,
    pub helium: u32,
    nitrogen: u32,
    maximum_operating_depth: f32,
}

impl Default for GasMixture {
    fn default() -> Self {
        Self::new(21, MINIMUM_HELIUM_VALUE)
    }
}

impl GasMixture {
    // TODO test
    pub fn new(oxygen: u32, helium: u32) -> Self {
        Self {
            oxygen,
            helium,
            nitrogen: GasMixture::update_nitrogen(oxygen, helium),
            maximum_operating_depth: GasMixture::calculate_maximum_operating_depth(oxygen),
        }
    }

    // TODO test
    pub fn update_oxygen(&self, oxygen: String) -> Self {
        let oxygen = parse_input_u32(
            oxygen,
            MINIMUM_OXYGEN_VALUE,
            MAXIMUM_OXYGEN_VALUE - self.helium,
        );

        Self::new(oxygen, self.helium)
    }

    // TODO test
    pub fn update_helium(&self, helium: String) -> Self {
        let helium = parse_input_u32(
            helium,
            MINIMUM_HELIUM_VALUE,
            MAXIMUM_HELIUM_VALUE - self.oxygen,
        );

        Self::new(self.oxygen, helium)
    }

    // TODO test
    pub fn get_nitrogen(&self) -> u32 {
        self.nitrogen
    }

    // TODO test
    pub fn get_maximum_operating_depth(&self) -> f32 {
        self.maximum_operating_depth
    }

    fn update_nitrogen(oxygen: u32, helium: u32) -> u32 {
        if oxygen + helium > 100 {
            return 0;
        }

        100 - oxygen - helium
    }

    fn calculate_maximum_operating_depth(oxygen: u32) -> f32 {
        const TOLERATED_PARTIAL_PRESSURE: f32 = 1.4;
        let oxygen_partial_pressure = oxygen as f32 / 100.0;
        let tolerated_pressure = TOLERATED_PARTIAL_PRESSURE / oxygen_partial_pressure;

        (tolerated_pressure * 10.0) - 10.0
    }

    pub fn is_valid(&self) -> bool {
        if self.oxygen > MAXIMUM_OXYGEN_VALUE {
            return false;
        } else if self.oxygen < MINIMUM_OXYGEN_VALUE {
            return false;
        } else if self.helium > MAXIMUM_HELIUM_VALUE {
            return false;
        } else if self.helium < MINIMUM_HELIUM_VALUE {
            return false;
        } else if self.helium + self.oxygen + self.nitrogen > 100 {
            return false;
        }

        true
    }
}

#[cfg(test)]
mod gas_mixture_should {
    use crate::models::plan::cylinders::gas_mixture::GasMixture;
    use rstest::rstest;

    #[rstest]
    #[case(21, 0, true)]
    #[case(101, 0, false)]
    #[case(4, 0, false)]
    #[case(21, 101, false)]
    #[case(51, 50, false)]
    #[case(50, 51, false)]
    fn test_validate_gas_mixture(
        #[case] oxygen: u32,
        #[case] helium: u32,
        #[case] expected_is_valid: bool,
    ) {
        // Given
        let gas_mixture = GasMixture::new(oxygen, helium);

        // When
        let is_valid = gas_mixture.is_valid();

        // Then
        assert_eq!(expected_is_valid, is_valid);
    }
}
