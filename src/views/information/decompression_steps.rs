use iced::widget::{text, Text, Button, button};
use iced_aw::Card;

use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner, models::{dive_step::DiveStep, decompression_steps::DecompressionSteps}};

pub struct DecompressionStepsView<'a> {
    pub decompression_steps_title_text: Text<'a>,
    pub decompression_steps_text: Card<'a, Message>,
    pub calculate_decompression: Button<'a, Message>,
}

impl DecompressionStepsView<'_> {
    pub fn new(decompression_steps: &DecompressionSteps) -> Self {
        Self {
            decompression_steps_title_text: text("Decompression Steps"),
            decompression_steps_text: Card::new(
                "Dive Step",
                text(""), //dive_planner.decompression_steps
            )
            .width(iced::Length::Fixed(500.0)),
            calculate_decompression: button("Update Dive Profile"),
        }
    }
}
