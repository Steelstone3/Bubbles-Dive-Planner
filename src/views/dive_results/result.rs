use iced::widget::text;
use iced_aw::Card;

use crate::{commands::messages::Message, models::dive_stage::DiveStage};

pub struct ResultView<'a> {
    pub result_text: Card<'a, Message>,
}

impl ResultView<'_> {
    // TODO add a build view
    pub fn new(dive_stage: &DiveStage) -> Self {
        let footer = format!(
            "Parameters\n\n{}\n\n{}",
            dive_stage.dive_step,
            dive_stage.cylinder.display_cylinder_summary()
        )
        .to_string();

        Self {
            result_text: Card::new("Dive Profile", text(dive_stage.dive_model.dive_profile))
                .foot(text(footer))
                .width(iced::Length::Fixed(500.0)),
        }
    }
}
