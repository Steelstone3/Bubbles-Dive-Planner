use crate::{commands::messages::Message, models::dive_planner::DivePlanner};
use iced::widget::{column, text, Column};

impl DivePlanner {
    pub fn plan_view(&self) -> Column<Message> {
        // TODO AH Scrollable::new()
        column!()
            .push(text("Plan"))
            .padding(10)
            .spacing(10)
            .push(self.select_dive_model_view())
            .padding(10)
            .spacing(10)
            .push(self.dive_step_view())
            .padding(10)
            .spacing(10)
            .push(self.select_cylinder_view())
            .spacing(10)
            .push(self.cylinder_view())
            .padding(10)
            .spacing(10)
            .push(self.dive_profile_view())
            .padding(10)
            .spacing(10)
    }
}
