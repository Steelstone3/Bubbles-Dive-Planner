use crate::{
    application::messages::message::Message, models::application::dive_planner::DivePlanner,
};
use iced::widget::{Column, button, column};

impl DivePlanner {
    pub fn dive_profile_view(&self) -> Column<'_, Message> {
        match self.dive_stage.is_valid() {
            true => {
                column!().push(button("Run Dive Profile").on_press(Message::DiveProfileOnClicked))
            }
            false => column!().push(button("Invalid Parameters")),
        }
    }
}
