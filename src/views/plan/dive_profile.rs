use iced::widget::{button, column, Column};

use crate::{commands::messages::Message, models::application::dive_planner::DivePlanner};

impl DivePlanner {
    pub fn dive_profile_view(&self) -> Column<Message> {
        match !self.dive_stage.validate() {
            true => column!().push(button("Invalid Parameters")),
            false => {
                column!().push(button("Run Dive Profile").on_press(Message::UpdateDiveProfile))
            }
        }
    }
}
