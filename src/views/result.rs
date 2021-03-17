use iced::widget::{text, Text};

use crate::view_models::dive_planner::DivePlanner;

pub struct ResultView<'a> {
    pub result_title_text: Text<'a>,
    pub result_text: Text<'a>,
}

impl ResultView<'_> {
    // TODO make a results view. This will contain the tab with the historic results
    pub fn new(dive_planner: &DivePlanner) -> Self {
        Self {
            result_title_text: text("Result"),
            result_text: text(dive_planner.dive_stage.dive_model.dive_model.dive_profile),
        }
    }
}
