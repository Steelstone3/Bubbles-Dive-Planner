use iced_aw::Card;

use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner};

pub struct DecompressionStepsView<'a> {
    pub decompression_steps_text: Card<'a, Message>,
}

impl DecompressionStepsView<'_> {
    pub fn new(dive_planner: &DivePlanner) -> Self {
        todo!()
    }
}
