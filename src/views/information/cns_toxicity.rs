use iced::widget::{column, text};
use iced_aw::widgets::Card;

use crate::{commands::messages::Message, models::dive_planner::DivePlanner};

impl DivePlanner {
    pub fn cns_toxicity_view(&self) -> iced::widget::Column<Message> {
        column!(Card::new("CNS Toxicity", text(&self.cns_toxicity))).spacing(10)
    }
}
