#[allow(dead_code)]
pub fn validate_range(input: u32, minimum: u32, maximum: u32) -> u32 {
    if input > maximum {
        return maximum;
    }

    if input < minimum {
        return minimum;
    }

    input
}

pub fn validate_maximum(input: u32, maximum: u32) -> u32 {
    if input > maximum {
        return maximum;
    }

    input
}

#[cfg(test)]
mod validator_should {
    use super::*;

    #[test]
    fn validate_input_when_below_maximum_limit() {
        // When
        let input = validate_maximum(90, 100);

        // Then
        assert_eq!(90, input);
    }

    #[test]
    fn validate_input_when_above_maximum_limit() {
        // When
        let input = validate_maximum(101, 100);

        // Then
        assert_eq!(100, input);
    }

    #[test]
    fn validate_input_when_in_range() {
        // When
        let input = validate_range(300, 100, 500);

        // Then
        assert_eq!(300, input);
    }

    #[test]
    fn validate_input_when_above_range() {
        // When
        let input = validate_range(501, 100, 500);

        // Then
        assert_eq!(500, input);
    }

    #[test]
    fn validate_input_when_below_range() {
        // When
        let input = validate_range(99, 100, 500);

        // Then
        assert_eq!(100, input);
    }
}
