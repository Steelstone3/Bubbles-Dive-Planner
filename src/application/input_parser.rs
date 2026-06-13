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
    use rstest::rstest;

    #[rstest]
    #[case("30".to_string(), 30)]
    #[case("Jeff".to_string(), 5)]
    #[case("".to_string(), 5)]
    #[case("2222".to_string(), 50)]
    fn test_parse_input_u32(#[case] input: String, #[case] expected_output: u32) {
        // when
        let output = parse_input_u32(input, 5, 50);

        // then
        pretty_assertions::assert_eq!(expected_output, output)
    }
}
