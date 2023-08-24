use iced::widget::{text, Text};
use iced_aw::Card;

use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner};

pub struct ResultsView<'a> {
    pub result_title_text: Text<'a>,
    pub result_text: Card<'a, Message>
}

impl ResultsView<'_> {
    // TODO make a results view. This will contain the tab with the historic results
    pub fn new(dive_planner: &DivePlanner) -> Self {
        Self {
            result_title_text: text("Results"),
            result_text: Card::new(
                "Dive Profile #1",
                text(dive_planner.dive_stage.dive_model.dive_model.dive_profile),
            ).foot("Dive Step\nDepth: Time:\n\nCylinder\nO2%: N%: He:\nRemaining: Used:"),
        }
    }
}
