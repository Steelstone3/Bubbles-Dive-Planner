use iced::widget::text;
use iced_aw::Card;

use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner};

pub struct DiveInformationView<'a> {
    pub dive_information_text: Card<'a, Message>,
}

impl DiveInformationView<'_> {
    pub fn new(dive_planner: &DivePlanner) -> Self {
        let maximum_operating_depth = format!(
            "{:.2}",
            dive_planner
                .dive_stage
                .cylinder
                .gas_mixture
                .maximum_operating_depth
        );
        let dive_ceiling = format!(
            "{:.2}",
            dive_planner
                .dive_stage
                .dive_model
                .dive_model
                .dive_profile
                .dive_ceiling
        );

        let footer = format!(
            "Dive Boundaries\n\nMaximum Operating Depth: {}\nDive Ceiling: {}",
            maximum_operating_depth, dive_ceiling,
        )
        .to_string();

        Self {
            dive_information_text: Card::new("Dive Information", text(&dive_planner.cns_toxicity))
                .foot(text(footer))
                .width(iced::Length::Fixed(500.0)),
        }
    }
}
