use crate::{
    commands::messages::Message,
    models::dive_step::DiveStep,
};
use iced::widget::{column, text, text_input, Column, Text, TextInput};

pub struct DiveStepView<'a> {
    dive_step_text: Text<'a>,
    depth_text: Text<'a>,
    depth_input: TextInput<'a, Message>,
    time_text: Text<'a>,
    time_input: TextInput<'a, Message>,
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
        .spacing(10.0)
        .padding(10.0)
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
