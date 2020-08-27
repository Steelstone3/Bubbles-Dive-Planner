use crate::{
    application::messages::message::Message, models::application::dive_planner::DivePlanner,
};
use iced::widget::{Column, column, text};

impl DivePlanner {
    pub fn plan_view(&self) -> Column<'_, Message> {
        column!()
            .push(text("Plan").size(24))
            .padding(10)
            .spacing(10)
            .push(self.select_dive_model_view())
            .padding(10)
            .spacing(10)
            .push(self.dive_step_view())
            .padding(10)
            .spacing(10)
            .push(self.cylinder_view())
            .padding(10)
            .spacing(10)
            .push(self.dive_profile_view())
            .padding(10)
            .spacing(10)
    }
}
