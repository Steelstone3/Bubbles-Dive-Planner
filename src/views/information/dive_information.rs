use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner};
use iced::widget::{column, text};

impl DivePlanner {
    pub fn information_view(&self) -> iced::widget::Column<Message> {
        column!()
            .push(text("Information"))
            .padding(10)
            .spacing(10)
            .push(self.dive_boundaries_view())
            .padding(10)
            .spacing(10)
            .push(self.decompression_steps_view())
            .padding(10)
            .spacing(10)
    }
}
