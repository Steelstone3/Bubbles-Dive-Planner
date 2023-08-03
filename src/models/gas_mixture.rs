use crate::presenters::presenter::{parse_numeric_value, text_prompt};
use serde::{Deserialize, Serialize};

#[derive(PartialEq, Debug, Copy, Clone, Serialize, Deserialize, Default)]
pub struct GasMixture {
    pub oxygen: u32,
    pub helium: u32,
    pub nitrogen: u32,
}

impl GasMixture {
    pub fn new() -> Self {
        let oxygen = parse_numeric_value(text_prompt(
            "Enter oxygen (%):",
            "Enter a value 5 - 100",
            "21",
        ));
        let helium = parse_numeric_value(text_prompt(
            "Enter helium (%):",
            "Enter a value 0 - 100",
            "0",
        ));

        GasMixture::assign_gas_mixture(oxygen, helium)
    }

    fn assign_gas_mixture(mut oxygen: u32, mut helium: u32) -> GasMixture {
        if oxygen > 100 {
            oxygen = 100 - helium;
        }

        if helium > 100 {
            helium = 100 - oxygen;
        }

        GasMixture {
            oxygen,
            helium,
            nitrogen: GasMixture::calculate_nitrogen_percentage(oxygen, helium),
        }
    }

    fn calculate_nitrogen_percentage(oxygen: u32, helium: u32) -> u32 {
        100 - oxygen - helium
    }
}

#[cfg(test)]
mod gas_mixture_should {
    use super::*;

    #[test]
    fn allow_assignment_of_oxygen() {
        let gas_mixture = GasMixture::assign_gas_mixture(70, 0);

        assert_eq!(70, gas_mixture.oxygen);
        assert_eq!(0, gas_mixture.helium);
        assert_eq!(30, gas_mixture.nitrogen);
    }

    #[test]
    fn allow_overflow_assignment_of_oxygen() {
        let gas_mixture = GasMixture::assign_gas_mixture(120, 5);

        assert_eq!(95, gas_mixture.oxygen);
        assert_eq!(5, gas_mixture.helium);
        assert_eq!(0, gas_mixture.nitrogen);
    }

    #[test]
    fn allow_assignment_of_helium() {
        let gas_mixture = GasMixture::assign_gas_mixture(21, 70);

        assert_eq!(70, gas_mixture.helium);
        assert_eq!(21, gas_mixture.oxygen);
        assert_eq!(9, gas_mixture.nitrogen);
    }

    #[test]
    fn allow_overflow_assignment_of_helium() {
        let gas_mixture = GasMixture::assign_gas_mixture(5, 120);

        assert_eq!(95, gas_mixture.helium);
        assert_eq!(5, gas_mixture.oxygen);
        assert_eq!(0, gas_mixture.nitrogen);
    }

    #[test]
    fn calculate_nitrogen_percentage() {
        let gas_mixture = GasMixture::assign_gas_mixture(40, 40);

        assert_eq!(40, gas_mixture.oxygen);
        assert_eq!(40, gas_mixture.helium);
        assert_eq!(20, gas_mixture.nitrogen);
    }
}
