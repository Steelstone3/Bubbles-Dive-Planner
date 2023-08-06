use super::input_parser::parse_input_u32;
use crate::{
    commands::messages::Message, models::dive_step::DiveStep,
    view_models::dive_planner::DivePlanner,
};
use iced::{
    widget::{text, text_input, Text, TextInput},
    Renderer,
};

pub struct DiveStepView<'a> {
    pub depth_text: Text<'a, Renderer>,
    pub depth_input: TextInput<'a, Message, Renderer>,
    pub time_text: Text<'a>,
    pub time_input: TextInput<'a, Message>,
}

impl DiveStepView<'_> {
    pub fn new(dive_planner: &DivePlanner) -> Self {
        Self {
            depth_text: text("Depth"),
            depth_input: text_input(
                "Enter Depth",
                &dive_planner.dive_stage.dive_step.depth.to_string(),
            )
            .width(100)
            .on_input(Message::DepthChanged),
            time_text: text("Time"),
            time_input: text_input(
                "Enter Time",
                &dive_planner.dive_stage.dive_step.time.to_string(),
            )
            .width(100)
            .on_input(Message::TimeChanged),
        }
    }

    pub fn update_depth(depth: String) -> u32 {
        let depth_input = parse_input_u32(depth, 0);
        DiveStep::validate(depth_input, 100)
    }

    pub fn update_time(time: String) -> u32 {
        let time_input = parse_input_u32(time, 0);
        DiveStep::validate(time_input, 60)
    }
}

#[cfg(test)]
mod dive_step_view_should {
    #[test]
    #[ignore]
    fn update_depth_by_parsing_and_validating_input_successfully() {}
    
    #[test]
    #[ignore]
    fn update_depth_by_parsing_with_invalid_input() {}

    #[test]
    #[ignore]
    fn update_depth_by_being_unable_to_parse_input() {}
    
    #[test]
    #[ignore]
    fn update_time_by_parsing_and_validating_input_successfully() {}

    #[test]
    #[ignore]
    fn update_time_by_parsing_with_invalid_input() {}

    #[test]
    #[ignore]
    fn update_time_by_being_unable_to_parse_input() {}
}