use crate::{
    application::messages::message::Message, models::application::dive_planner::DivePlanner,
};
use iced::widget::{column, row, text};
use iced_aw::widgets::Card;

impl DivePlanner {
    pub fn dive_boundaries_view(&self) -> iced::widget::Column<'_, Message> {
        column!(Card::new(
            "Dive Boundaries",
            column!()
                .push(
                    row!()
                        .push(text("Maximum Operating Depth (m):"))
                        .spacing(10)
                        .push(text(format!(
                            "{:.2}",
                            self.dive_stage
                                .cylinder
                                .gas_mixture
                                .get_maximum_operating_depth()
                        )))
                        .spacing(10),
                )
                .spacing(10)
                .push(
                    row!()
                        .push(text("Dive Ceiling (m):"))
                        .spacing(10)
                        .push(text(format!(
                            "{:.2}",
                            self.dive_stage
                                .dive_model
                                .get_dive_profile()
                                .tolerated_surface_pressure
                                .get_dive_ceiling()
                        )))
                        .spacing(10),
                )
                .spacing(10)
        ))
        .spacing(10)
    }
}
