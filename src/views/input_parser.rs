pub fn parse_input_u32(input: String, minimum_value: u32) -> u32 {
    input.parse::<u32>().unwrap_or(minimum_value)
}

#[cfg(test)]
mod input_parser_should {
    use super::*;

    #[test]
    fn parse_input_of_u32_type() {
        // When
        let number = parse_input_u32(30.to_string(), 0);

        // Then
        assert_eq!(30, number);
    }

    #[test]
    fn return_minimum_value_for_invalid_input() {
        // When
        let number = parse_input_u32("Jeff".to_string(), 5);

        // Then
        assert_eq!(5, number);
    }
}
