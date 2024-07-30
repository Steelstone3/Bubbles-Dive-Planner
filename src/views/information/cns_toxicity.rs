use iced::widget::{column, text};

use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner};

impl DivePlanner {
    pub fn cns_toxicity_view(&self) -> iced::widget::Column<Message> {
        column!(text("CNS Toxicity")).spacing(10)
    }
}
