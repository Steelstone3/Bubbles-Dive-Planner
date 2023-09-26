use crate::{
    commands::messages::Message,
    models::gas_management::{
        GasManagement, MAXIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE,
        MINIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE,
    },
    views::application::input_parser::parse_input_u32,
};
use iced::widget::{column, text, text_input, Column, Text, TextInput};

pub struct GasManagementView<'a> {
    surface_air_consumption_text: Text<'a>,
    surface_air_consumption_input: TextInput<'a, Message>,
}

impl GasManagementView<'_> {
    pub fn build_view<'a>(gas_management: &GasManagement) -> Column<'a, Message> {
        let gas_management = GasManagementView::new(gas_management);

        column![
            gas_management.surface_air_consumption_text,
            gas_management.surface_air_consumption_input
        ]
    }

    fn new<'a>(gas_management: &GasManagement) -> GasManagementView<'a> {
        GasManagementView {
            surface_air_consumption_text: text("S.A.C Rate (l/min)"),
            surface_air_consumption_input: text_input(
                "Enter S.A.C Rate",
                &gas_management.surface_air_consumption_rate.to_string(),
            )
            .on_input(Message::SurfaceAirConsumptionChanged),
        }
    }

    pub fn update_surface_air_consumption_rate(surface_air_consumption: String) -> u32 {
        parse_input_u32(
            surface_air_consumption,
            MINIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE,
            MAXIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE,
        )
    }
}

#[cfg(test)]
mod gas_management_view_should {
    use super::*;

    #[test]
    fn update_surface_air_consumption_rate_by_parsing_and_validating_input_successfully() {
        // Given
        let expected = 12;
        let input = "12".to_string();

        // When
        let validated_surface_air_consumption_rate =
            GasManagementView::update_surface_air_consumption_rate(input);

        // Then
        assert_eq!(expected, validated_surface_air_consumption_rate);
    }

    #[test]
    fn update_surface_air_consumption_rate_by_parsing_an_input_beyond_range() {
        // Given
        let expected = 30;
        let input = "31".to_string();

        // When
        let validated_surface_air_consumption_rate =
            GasManagementView::update_surface_air_consumption_rate(input);

        // Then
        assert_eq!(expected, validated_surface_air_consumption_rate);
    }

    #[test]
    fn update_surface_air_consumption_rate_by_being_unable_to_parse_input() {
        // Given
        let expected = 3;
        let input = "$%45sdg".to_string();

        // When
        let validated_surface_air_consumption_rate =
            GasManagementView::update_surface_air_consumption_rate(input);

        // Then
        assert_eq!(expected, validated_surface_air_consumption_rate);
    }
}
