use iced::widget::text;
use iced_aw::Card;

use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner};

pub struct DiveInformationView<'a> {
    pub dive_information_text: Card<'a, Message>,
}

impl DiveInformationView<'_> {
    pub fn new(dive_planner: &DivePlanner) -> Self {
        let footer = format!(
            "Dive Boundaries\n\nMaximum Operating Depth: {}\nDive Ceiling: {}",
            0, // change to maximum operating depth from gas mixture
            0, // change to dive ceiling from dive profile
        )
        .to_string();

        Self {
            dive_information_text: Card::new("Dive Information", text(&dive_planner.cns_toxicity))
                .foot(text(footer))
                .width(iced::Length::Fixed(500.0)),
        }
    }
}
