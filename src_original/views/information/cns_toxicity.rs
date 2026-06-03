use iced::widget::column;
use iced_aw::widgets::Card;

use crate::{
    application::messages::message::Message, models::application::dive_planner::DivePlanner,
    views::information::cns_toxicity_table::cns_toxicity_table,
};

impl DivePlanner {
    pub fn cns_toxicity_view(&self) -> iced::widget::Column<'_, Message> {
        column!(Card::new(
            "CNS Toxicity",
            cns_toxicity_table(
                &self.dive_information.cns_toxicity,
                self.theme().palette().text
            )
        ))
        .spacing(10)
    }
}
