use crate::{commands::messages::Message, models::dive_planner::DivePlanner};
use iced::widget::{column, text};

impl DivePlanner {
    pub fn information_view(&self) -> iced::widget::Column<Message> {
        if self.is_planning {
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
        } else {
            column!()
                .push(text("Information"))
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
