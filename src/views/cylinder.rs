use crate::{
    commands::messages::Message, models::gas_mixture::GasMixture,
    view_models::dive_planner::DivePlanner, views::input_parser::parse_input_u32,
};
use iced::widget::{text, text_input, Text, TextInput};

pub struct CylinderView<'a> {
    pub oxygen_text: Text<'a>,
    pub oxygen_input: TextInput<'a, Message>,
    pub helium_text: Text<'a>,
    pub helium_input: TextInput<'a, Message>,
    pub nitrogen_text: Text<'a>,
    pub nitrogen_text_value: Text<'a>,
}

impl CylinderView<'_> {
    pub fn new(dive_planner: &DivePlanner) -> Self {
        Self {
            oxygen_text: text("Oxygen"),
            oxygen_input: text_input(
                "Enter Oxygen",
                &dive_planner
                    .dive_stage
                    .cylinder
                    .gas_mixture
                    .oxygen
                    .to_string(),
            )
            .width(100)
            .on_input(Message::OxygenChanged),

            helium_text: text("Helium"),
            helium_input: text_input(
                "Enter Helium",
                &dive_planner
                    .dive_stage
                    .cylinder
                    .gas_mixture
                    .helium
                    .to_string(),
            )
            .width(100)
            .on_input(Message::HeliumChanged),

            nitrogen_text: text("Nitrogen"),
            nitrogen_text_value: text(dive_planner.dive_stage.cylinder.gas_mixture.nitrogen),
        }
    }

    pub fn update_oxygen(oxygen: String, helium: u32) -> GasMixture {
        let oxygen_input = parse_input_u32(oxygen, 5);

        GasMixture::validate_oxygen(oxygen_input, helium)
    }

    pub fn update_helium(helium: String, oxygen: u32) -> GasMixture {
        let helium_input = parse_input_u32(helium, 0);

        GasMixture::validate_helium(oxygen, helium_input)
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
        };
        let input = "21".to_string();

        // When
        let validated_gas_mixture = CylinderView::update_oxygen(input, 0);

        // Then
        assert_eq!(expected, validated_gas_mixture);
    }

    #[test]
    fn update_oxygen_by_parsing_an_input_beyond_range() {
        // Given
        let expected = GasMixture {
            oxygen: 100,
            helium: 0,
            nitrogen: 0,
        };
        let input = "101".to_string();

        // When
        let validated_gas_mixture = CylinderView::update_oxygen(input, 0);

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
        };
        let input = "101£%^asda".to_string();

        // When
        let validated_gas_mixture = CylinderView::update_oxygen(input, 0);

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
        };
        let input = "21".to_string();

        // When
        let validated_gas_mixture = CylinderView::update_helium(input, 0);

        // Then
        assert_eq!(expected, validated_gas_mixture);
    }

    #[test]
    fn update_helium_by_parsing_an_input_beyond_range() {
        // Given
        let expected = GasMixture {
            oxygen: 0,
            helium: 100,
            nitrogen: 0,
        };
        let input = "101".to_string();

        // When
        let validated_gas_mixture = CylinderView::update_helium(input, 0);

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
        };
        let input = "101£%^&sdfd".to_string();

        // When
        let validated_gas_mixture = CylinderView::update_helium(input, 0);

        // Then
        assert_eq!(expected, validated_gas_mixture);
    }
}
