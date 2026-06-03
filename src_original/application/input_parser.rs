pub fn parse_input_u32(input: String, minimum_value: u32, maximum_value: u32) -> u32 {
    let value = input.parse::<u32>().unwrap_or(minimum_value);

    if value > maximum_value {
        return maximum_value;
    }

    value
}

#[cfg(test)]
mod input_parser_should {
    use super::*;

    #[test]
    fn parse_input_of_u32_type() {
        // When
        let number = parse_input_u32(30.to_string(), 0, 50);

        // Then
        assert_eq!(30, number);
    }

    #[test]
    fn return_minimum_value_for_invalid_input() {
        // When
        let number = parse_input_u32("Jeff".to_string(), 5, 200);

        // Then
        assert_eq!(5, number);
    }

    #[test]
    fn return_maximum_value_for_invalid_input() {
        // When
        let number = parse_input_u32("2222".to_string(), 5, 200);

        // Then
        assert_eq!(200, number);
    }
}
