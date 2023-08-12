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
}
