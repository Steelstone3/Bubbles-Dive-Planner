use iced::{
    widget::{text, text_input, Text, TextInput}, Renderer,
};

use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner};

pub struct DiveStepView<'a> {
    pub depth_text: Text<'a, Renderer>,
    pub depth_input: TextInput<'a, Message, Renderer>,
    pub time_text: Text<'a>,
    pub time_input: TextInput<'a, Message>,
}

impl DiveStepView<'_> {
    pub fn new(dive_planner: &DivePlanner) -> Self {
        DiveStepView {
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
}
