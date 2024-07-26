use iced::widget::column;

use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner};

impl DivePlanner {
    pub fn menu_view(&self) -> iced::widget::Column<Message> {
        column!()
    }
}
