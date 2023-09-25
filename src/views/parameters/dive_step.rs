use crate::{
    commands::messages::Message,
    models::dive_step::{
        DiveStep, MAXIMUM_DEPTH_VALUE, MAXIMUM_TIME_VALUE, MINIMUM_DEPTH_VALUE, MINIMUM_TIME_VALUE,
    },
    views::application::input_parser::parse_input_u32,
};
use iced::widget::{column, text, text_input, Column, Text, TextInput};

pub struct DiveStepView<'a> {
    pub dive_step_text: Text<'a>,
    pub depth_text: Text<'a>,
    pub depth_input: TextInput<'a, Message>,
    pub time_text: Text<'a>,
    pub time_input: TextInput<'a, Message>,
}

impl DiveStepView<'_> {
    pub fn build_view<'a>(dive_step: &DiveStep) -> Column<'a, Message> {
        let dive_step = DiveStepView::new(dive_step);

        column![
            dive_step.dive_step_text,
            dive_step.depth_text,
            dive_step.depth_input,
            dive_step.time_text,
            dive_step.time_input,
        ]
    }

    pub fn update_depth(depth: String) -> u32 {
        parse_input_u32(depth, MINIMUM_DEPTH_VALUE, MAXIMUM_DEPTH_VALUE)
    }

    pub fn update_time(time: String) -> u32 {
        parse_input_u32(time, MINIMUM_TIME_VALUE, MAXIMUM_TIME_VALUE)
    }

    fn new<'a>(dive_step: &DiveStep) -> DiveStepView<'a> {
        DiveStepView {
            dive_step_text: text("Dive Step"),
            depth_text: text("Depth (m)"),
            depth_input: text_input("Enter Depth", &dive_step.depth.to_string())
                .on_input(Message::DepthChanged),
            time_text: text("Time (min)"),
            time_input: text_input("Enter Time", &dive_step.time.to_string())
                .on_input(Message::TimeChanged),
        }
    }
}

#[cfg(test)]
mod dive_step_view_should {
    use super::*;

    #[test]
    fn update_depth_by_parsing_and_validating_input_successfully() {
        // Given
        let expected = 50;
        let input = "50".to_string();

        // When
        let validated_depth = DiveStepView::update_depth(input);

        // Then
        assert_eq!(expected, validated_depth);
    }

    #[test]
    fn update_depth_by_parsing_an_input_beyond_range() {
        // Given
        let expected = 100;
        let input = "101".to_string();

        // When
        let validated_depth = DiveStepView::update_depth(input);

        // Then
        assert_eq!(expected, validated_depth);
    }

    #[test]
    fn update_depth_by_being_unable_to_parse_input() {
        // Given
        let expected = 1;
        let input = "$%45sdg".to_string();

        // When
        let validated_depth = DiveStepView::update_depth(input);

        // Then
        assert_eq!(expected, validated_depth);
    }

    #[test]
    fn update_time_by_parsing_and_validating_input_successfully() {
        // Given
        let expected = 10;
        let input = "10".to_string();

        // When
        let validated_depth = DiveStepView::update_time(input);

        // Then
        assert_eq!(expected, validated_depth);
    }

    #[test]
    fn update_time_by_parsing_an_input_beyond_range() {
        // Given
        let expected = 60;
        let input = "61".to_string();

        // When
        let validated_depth = DiveStepView::update_time(input);

        // Then
        assert_eq!(expected, validated_depth);
    }

    #[test]
    fn update_time_by_being_unable_to_parse_input() {
        // Given
        let expected = 1;
        let input = "$£61asd".to_string();

        // When
        let validated_depth = DiveStepView::update_time(input);

        // Then
        assert_eq!(expected, validated_depth);
    }
}
