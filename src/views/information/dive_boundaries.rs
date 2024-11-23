use iced::widget::{column, text};
use iced_aw::widgets::Card;

use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner};

impl DivePlanner {
    pub fn dive_boundaries_view(&self) -> iced::widget::Column<Message> {
        column!(Card::new(
            "Dive Boundaries",
            text(format!(
                "{}\n{}",
                self.dive_stage
                    .cylinder
                    .gas_mixture
                    .display_maximum_operating_depth(),
                self.dive_stage
                    .dive_model
                    .dive_profile
                    .display_dive_ceiling()
            ))
        ))
        .spacing(10)
    }
}