use super::input_parser::parse_input_u32;
use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner, models::gas_management::{MINIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE, MAXIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE}};
use iced::widget::{text, text_input, Text, TextInput};

pub struct GasManagementView<'a> {
    pub surface_air_consumption_text: Text<'a>,
    pub surface_air_consumption_input: TextInput<'a, Message>,
}

impl GasManagementView<'_> {
    pub fn new(dive_planner: &DivePlanner) -> Self {
        Self {
            surface_air_consumption_text: text("S.A.C Rate (l/min)"),
            surface_air_consumption_input: text_input(
                "Enter S.A.C Rate",
                &dive_planner
                    .dive_stage
                    .cylinder
                    .gas_management
                    .surface_air_consumption_rate
                    .to_string(),
            )
            .on_input(Message::SurfaceAirConsumptionChanged),
        }
    }

    pub fn update_surface_air_consumption_rate(surface_air_consumption: String) -> u32 {
        parse_input_u32(surface_air_consumption, MINIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE, MAXIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE)
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
