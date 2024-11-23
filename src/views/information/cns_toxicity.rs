use crate::{commands::messages::Message, models::application::dive_planner::DivePlanner};
use iced::widget::{column, text};
use iced_aw::widgets::Card;

impl DivePlanner {
    pub fn cns_toxicity_view(&self) -> iced::widget::Column<Message> {
        column!(Card::new(
            "CNS Toxicity",
            text(self.cns_toxicity.to_string())
        ))
        .spacing(10)
    }
}
