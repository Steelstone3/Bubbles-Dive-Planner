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
    pub maximum_operating_depth: f32,
}

impl Default for GasMixture {
    fn default() -> Self {
        Self {
            oxygen: 0,
            helium: 0,
            nitrogen: DEFAULT_NITROGEN_VALUE,
            maximum_operating_depth: 0.0,
        }
    }
}

impl GasMixture {
    pub fn update_nitrogen(&mut self) {
        self.nitrogen = DEFAULT_NITROGEN_VALUE - self.oxygen - self.helium
    }

    pub fn calculate_maximum_operating_depth(&mut self) {
        if self.oxygen > 0 {
            const TOLERATED_PARTIAL_PRESSURE: f32 = 1.4;
            let oxygen_partial_pressure = self.oxygen as f32 / 100.0;
            let tolerated_pressure = TOLERATED_PARTIAL_PRESSURE / oxygen_partial_pressure;
            self.maximum_operating_depth = (tolerated_pressure * 10.0) - 10.0;
        }
    }

    pub fn validate(&self) -> bool {
        let oxygen_validation =
            self.oxygen < MINIMUM_OXYGEN_VALUE || self.oxygen > MAXIMUM_OXYGEN_VALUE;
        let helium_validation = self.helium > MAXIMUM_OXYGEN_VALUE;
        let gas_mixture_validation = self.helium + self.oxygen > 100;

        if oxygen_validation || helium_validation || gas_mixture_validation {
            return false;
        }

        true
    }

    pub fn display_maximum_operating_depth(&self) -> String {
        format!(
            "Maximum Operating Depth: {:.2} (m)",
            self.maximum_operating_depth
        )
    }
}

#[cfg(test)]
mod gas_mixture_should {
    use super::*;
    use rstest::rstest;

    #[test]
    fn display_read_only_maximum_operating_depth() {
        // Given
        let gas_mixture = GasMixture {
            maximum_operating_depth: 40.555,
            ..Default::default()
        };
        let expected_display = "Maximum Operating Depth: 40.56 (m)";

        // When
        let display = gas_mixture.display_maximum_operating_depth();

        // Then
        assert_eq!(expected_display, display);
    }

    #[test]
    fn calculate_nitrogen_for_a_given_gas_mixture() {
        // Given
        let mut gas_mixture = GasMixture {
            oxygen: 21,
            helium: 10,
            nitrogen: 0,
            maximum_operating_depth: 0.0,
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
            maximum_operating_depth: 0.0,
        };

        // When
        let is_valid_actual = gas_mixture.validate();

        // Then
        assert_eq!(is_valid, is_valid_actual);
    }

    #[rstest]
    #[case(0, 0.0)]
    #[case(21, 56.67)]
    #[case(32, 33.75)]
    #[case(36, 28.89)]
    #[case(50, 18.0)]
    fn calculate_the_maximum_operating_depth(
        #[case] oxygen: u32,
        #[case] maximum_operating_depth: f32,
    ) {
        // Given
        let mut gas_mixture = GasMixture {
            oxygen,
            ..Default::default()
        };
        let expected_gas_mixture = GasMixture {
            oxygen,
            maximum_operating_depth,
            ..Default::default()
        };

        // When
        gas_mixture.calculate_maximum_operating_depth();

        // Then
        assert_eq!(
            format!("{:.2}", expected_gas_mixture.maximum_operating_depth),
            format!("{:.2}", gas_mixture.maximum_operating_depth)
        );
    }
}
