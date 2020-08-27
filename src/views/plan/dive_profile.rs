use iced::widget::{Column, button, column};

use crate::{
    application::messages::message::Message, models::application::dive_planner::DivePlanner,
};

impl DivePlanner {
    pub fn dive_profile_view(&self) -> Column<'_, Message> {
        match !self.dive_stage.validate() {
            true => column!().push(button("Invalid Parameters")),
            false => {
                column!().push(button("Run Dive Profile").on_press(Message::DiveProfileOnClicked))
            }
        }
    }
}
