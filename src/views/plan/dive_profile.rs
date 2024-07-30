use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner};
use iced::widget::{button, column, Column};

impl DivePlanner {
    pub fn dive_profile_view(&self) -> Column<Message> {
        match !self.dive_stage.validate() {
            true => column!().push(button("Invalid Parameters")),
            false => {
                column!().push(button("Update Dive Profile").on_press(Message::UpdateDiveProfile))
            }
        }
    }
}
