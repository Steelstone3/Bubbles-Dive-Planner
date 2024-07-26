use iced::widget::{column, text};

use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner};

impl DivePlanner {
    pub fn decompression_steps_view(&self) -> iced::widget::Column<Message> {
        column!(text("Decompression Steps")).spacing(10)
    }
}
