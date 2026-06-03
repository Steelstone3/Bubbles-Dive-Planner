use crate::{
    application::messages::message::Message, models::application::dive_planner::DivePlanner,
};
use iced::widget::{Column, button, column};

impl DivePlanner {
    pub fn dive_profile_view(&self) -> Column<'_, Message> {
        column!().push(button("Run Dive Profile").on_press(Message::DiveProfileOnClicked))
    }
}
