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
    pub fn validate_oxygen(mut oxygen: u32, mut helium: u32) -> GasMixture {
        if oxygen > 100 {
            oxygen = 100;
        }
        
        if oxygen + helium > 100 {
            helium = 100 - oxygen;
        }

        GasMixture {
            oxygen,
            helium,
            nitrogen: GasMixture::calculate_nitrogen(oxygen, helium),
        }
    }

    pub fn validate_helium(mut oxygen: u32, mut helium: u32) -> GasMixture {
        if helium > 100 {
            helium = 100;
        }
        
        if oxygen + helium > 100 {
            oxygen = 100 - helium;
        }
        
        GasMixture {
            oxygen,
            helium,
            nitrogen: GasMixture::calculate_nitrogen(oxygen, helium),
        }
    }

    fn calculate_nitrogen(oxygen: u32, helium: u32) -> u32 {
        100 - oxygen - helium
    }
}

#[cfg(test)]
mod gas_mixture_should {
    use super::*;

    #[test]
    fn validate_assignment_of_oxygen() {
        // When
        let gas_mixture = GasMixture::validate_oxygen(21, 0);

        // Then
        assert_eq!(21, gas_mixture.oxygen);
        assert_eq!(0, gas_mixture.helium);
        assert_eq!(79, gas_mixture.nitrogen);
    }

    #[test]
    fn validate_assignment_of_oxygen_where_oxygen_is_above_maximum() {
        // When
        let gas_mixture = GasMixture::validate_oxygen(101, 0);

        // Then
        assert_eq!(100, gas_mixture.oxygen);
        assert_eq!(0, gas_mixture.helium);
        assert_eq!(0, gas_mixture.nitrogen);
    }

    #[test]
    fn validate_assignment_of_oxygen_where_oxygen_and_helium_are_above_maximum() {
        // When
        let gas_mixture = GasMixture::validate_oxygen(95, 6);

        // Then
        assert_eq!(95, gas_mixture.oxygen);
        assert_eq!(5, gas_mixture.helium);
        assert_eq!(0, gas_mixture.nitrogen);
    }

    #[test]
    fn validate_assignment_of_helium() {
        // When
        let gas_mixture = GasMixture::validate_helium(0, 10);

        // Then
        assert_eq!(0, gas_mixture.oxygen);
        assert_eq!(10, gas_mixture.helium);
        assert_eq!(90, gas_mixture.nitrogen);
    }

    #[test]
    fn validate_assignment_of_helium_where_helium_is_above_maximum() {
        // When
        let gas_mixture = GasMixture::validate_helium(0, 101);

        // Then
        assert_eq!(0, gas_mixture.oxygen);
        assert_eq!(100, gas_mixture.helium);
        assert_eq!(0, gas_mixture.nitrogen);
    }

    #[test]
    fn validate_assignment_of_helium_where_helium_and_oxygen_are_above_maximum() {
        // When
        let gas_mixture = GasMixture::validate_helium(50, 60);

        // Then
        assert_eq!(40, gas_mixture.oxygen);
        assert_eq!(60, gas_mixture.helium);
        assert_eq!(0, gas_mixture.nitrogen);
    }

    #[test]
    fn calculate_nitrogen_for_a_given_gas_mixture() {
        // Given
        let gas_mixture = GasMixture{ oxygen: 21, helium: 10, nitrogen: 0 };

        // When
        let nitrogen = GasMixture::calculate_nitrogen(gas_mixture.oxygen, gas_mixture.helium);

        // Then
        assert_eq!(69, nitrogen);
    }
}
