use iced::widget::{column, text};

use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner};

impl DivePlanner {
    pub fn dive_boundaries_view(&self) -> iced::widget::Column<Message> {
        column!(text("Dive Boundaries")).spacing(10)
    }
}
