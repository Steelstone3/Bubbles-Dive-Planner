use iced::widget::text;
use iced_aw::Card;

use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner};

pub struct DiveInformationView<'a> {
    pub dive_information_text: Card<'a, Message>,
}

impl DiveInformationView<'_> {
    pub fn new(dive_planner: &DivePlanner) -> Self {
        let maximum_operating_depth = dive_planner
            .dive_stage
            .cylinder
            .gas_mixture
            .display_maximum_operating_depth();
        let dive_ceiling = dive_planner
            .dive_stage
            .dive_model
            .dive_profile
            .display_dive_ceiling();

        Self {
            dive_information_text: Card::new(
                "Dive Information",
                text(format!(
                    "{}Dive Boundaries\n\n{}\n{}",
                    &dive_planner.cns_toxicity, maximum_operating_depth, dive_ceiling
                )),
            )
            .width(iced::Length::Fixed(500.0)),
        }
    }
}
