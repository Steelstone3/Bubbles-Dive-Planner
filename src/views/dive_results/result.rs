use iced::widget::text;
use iced_aw::Card;

use crate::{commands::messages::Message, models::dive_stage::DiveStage};

pub struct ResultView<'a> {
    pub result_text: Card<'a, Message>,
}

impl ResultView<'_> {
    pub fn new(dive_stage: &DiveStage) -> Self {
        let footer = format!(
            "Parameters\n\nDive Step\nDepth: {} Time: {}\n\nCylinder\nO2%: {} N%: {} He: {}\nRemaining: {} Used: {}", 
            dive_stage.dive_step.depth,
            dive_stage.dive_step.time,
            dive_stage.cylinder.gas_mixture.oxygen,
            dive_stage.cylinder.gas_mixture.nitrogen,
            dive_stage.cylinder.gas_mixture.helium,
            dive_stage.cylinder.gas_management.remaining,
            dive_stage.cylinder.gas_management.used,
        ).to_string();

        Self {
            result_text: Card::new(
                "Dive Profile",
                text(dive_stage.dive_model.dive_model.dive_profile),
            )
            .foot(text(footer))
            .width(iced::Length::Fixed(500.0)),
        }
    }
}
