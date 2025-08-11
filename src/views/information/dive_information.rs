use crate::{commands::messages::Message, models::application::dive_planner::DivePlanner};
use iced::widget::{column, text};

impl DivePlanner {
    pub fn information_view(&self) -> iced::widget::Column<'_, Message> {
        if !self.application_state.is_planning {
            column!()
                .push(text("Information").size(24))
                .padding(10)
                .spacing(10)
                .push(self.dive_boundaries_view())
                .padding(10)
                .spacing(10)
                .push(self.decompression_steps_view())
                .padding(10)
                .spacing(10)
        } else {
            column!()
                .push(text("Information").size(24))
                .padding(10)
                .spacing(10)
                .push(self.dive_boundaries_view())
                .padding(10)
                .spacing(10)
                .push(self.cns_toxicity_view())
                .padding(10)
                .spacing(10)
        }
    }
}
