use crate::{
    commands::messages::Message,
    models::gas_mixture::{
        GasMixture, MAXIMUM_HELIUM_VALUE, MAXIMUM_OXYGEN_VALUE, MINIMUM_HELIUM_VALUE,
        MINIMUM_OXYGEN_VALUE,
    },
    views::application::input_parser::parse_input_u32,
};
use iced::widget::{column, text, text_input, Column, Text, TextInput};

pub struct GasMixtureView<'a> {
    gas_mixture_text: Text<'a>,
    oxygen_text: Text<'a>,
    oxygen_input: TextInput<'a, Message>,
    helium_text: Text<'a>,
    helium_input: TextInput<'a, Message>,
    nitrogen_text: Text<'a>,
    nitrogen_text_value: Text<'a>,
}

impl GasMixtureView<'_> {
    pub fn build_view<'a>(gas_mixture: &GasMixture) -> Column<'a, Message> {
        let gas_mixture = GasMixtureView::new(gas_mixture);

        column![
            gas_mixture.gas_mixture_text,
            gas_mixture.oxygen_text,
            gas_mixture.oxygen_input,
            gas_mixture.helium_text,
            gas_mixture.helium_input,
            gas_mixture.nitrogen_text,
            gas_mixture.nitrogen_text_value,
        ]
    }

    fn new<'a>(gas_mixture: &GasMixture) -> GasMixtureView<'a> {
        GasMixtureView {
            gas_mixture_text: text("Gas Mixture"),
            oxygen_text: text("Oxygen (%)"),
            oxygen_input: text_input("Enter Oxygen", &gas_mixture.oxygen.to_string())
                .on_input(Message::OxygenChanged),

            helium_text: text("Helium (%)"),
            helium_input: text_input("Enter Helium", &gas_mixture.helium.to_string())
                .on_input(Message::HeliumChanged),

            nitrogen_text: text("Nitrogen (%)"),
            nitrogen_text_value: text(gas_mixture.nitrogen),
        }
    }

    pub fn update_oxygen(oxygen: String, helium: u32) -> GasMixture {
        let oxygen_input =
            parse_input_u32(oxygen, MINIMUM_OXYGEN_VALUE, MAXIMUM_OXYGEN_VALUE - helium);

        let mut gas_mixture = GasMixture {
            oxygen: oxygen_input,
            helium,
            ..Default::default()
        };

        gas_mixture.update_nitrogen();
        gas_mixture.calculate_maximum_operating_depth();

        gas_mixture
    }

    pub fn update_helium(helium: String, oxygen: u32) -> GasMixture {
        let helium_input =
            parse_input_u32(helium, MINIMUM_HELIUM_VALUE, MAXIMUM_HELIUM_VALUE - oxygen);

        let mut gas_mixture = GasMixture {
            helium: helium_input,
            oxygen,
            ..Default::default()
        };

        gas_mixture.update_nitrogen();

        gas_mixture
    }
}

#[cfg(test)]
mod cylinder_view_should {
    use super::*;

    #[test]
    fn update_oxygen_by_parsing_and_validating_input_successfully() {
        // Given
        let expected = GasMixture {
            oxygen: 21,
            helium: 0,
            nitrogen: 79,
            maximum_operating_depth: 56.66667,
        };
        let input = "21".to_string();

        // When
        let validated_gas_mixture = GasMixtureView::update_oxygen(input, 0);

        // Then
        assert_eq!(expected, validated_gas_mixture);
    }

    #[test]
    fn update_oxygen_by_parsing_an_input_beyond_range() {
        // Given
        let expected = GasMixture {
            oxygen: 90,
            helium: 10,
            nitrogen: 0,
            maximum_operating_depth: 5.5555553,
        };
        let input = "101".to_string();

        // When
        let validated_gas_mixture = GasMixtureView::update_oxygen(input, 10);

        // Then
        assert_eq!(expected, validated_gas_mixture);
    }

    #[test]
    fn update_oxygen_by_being_unable_to_parse_input() {
        // Given
        let expected = GasMixture {
            oxygen: 5,
            helium: 0,
            nitrogen: 95,
            maximum_operating_depth: 270.0,
        };
        let input = "101£%^asda".to_string();

        // When
        let validated_gas_mixture = GasMixtureView::update_oxygen(input, 0);

        // Then
        assert_eq!(expected, validated_gas_mixture);
    }

    #[test]
    fn update_helium_by_parsing_and_validating_input_successfully() {
        // Given
        let expected = GasMixture {
            oxygen: 0,
            helium: 21,
            nitrogen: 79,
            maximum_operating_depth: 0.0,
        };
        let input = "21".to_string();

        // When
        let validated_gas_mixture = GasMixtureView::update_helium(input, 0);

        // Then
        assert_eq!(expected, validated_gas_mixture);
    }

    #[test]
    fn update_helium_by_parsing_an_input_beyond_range() {
        // Given
        let expected = GasMixture {
            oxygen: 10,
            helium: 90,
            nitrogen: 0,
            maximum_operating_depth: 0.0,
        };
        let input = "101".to_string();

        // When
        let validated_gas_mixture = GasMixtureView::update_helium(input, 10);

        // Then
        assert_eq!(expected, validated_gas_mixture);
    }

    #[test]
    fn update_helium_by_being_unable_to_parse_input() {
        // Given
        let expected = GasMixture {
            oxygen: 0,
            helium: 0,
            nitrogen: 100,
            maximum_operating_depth: 0.0,
        };
        let input = "101£%^&sdfd".to_string();

        // When
        let validated_gas_mixture = GasMixtureView::update_helium(input, 0);

        // Then
        assert_eq!(expected, validated_gas_mixture);
    }
}
