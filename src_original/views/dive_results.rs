use crate::{
    application::messages::message::Message, models::application::dive_planner::DivePlanner,
    views::dive_results_table::dive_results_table,
};
use iced::{
    Renderer, Theme,
    widget::{column, row, text},
};
use iced_aw::widgets::Card;

impl DivePlanner {
    pub fn results_view(&self) -> iced::widget::Column<'_, Message> {
        if self.dive_planning.is_planning {
            column!()
        } else {
            let mut column = column![];

            column = column
                .push(text("Results").size(24))
                .padding(10)
                .spacing(10);

            for result in self.result_cards() {
                column = column.push(result).padding(10).spacing(10);
            }

            column
        }
    }

    fn result_cards(&self) -> Vec<Card<'_, Message, Theme, Renderer>> {
        let mut result_cards = vec![];

        for dive_stage in &self.dive_results.results {
            result_cards.push(
                Card::new(
                    "Dive Profile",
                    dive_results_table(
                        &dive_stage.dive_model.dive_profile,
                        self.theme().palette().text,
                    ),
                )
                .foot(
                    column!()
                        .push(Card::new(
                            "Dive Step",
                            column!()
                                .push(
                                    row!()
                                        .push(text("Depth (m):"))
                                        .spacing(10)
                                        .push(text(dive_stage.dive_step.depth.to_string()))
                                        .spacing(10),
                                )
                                .spacing(10)
                                .push(
                                    row!()
                                        .push(text("Time (min):"))
                                        .spacing(10)
                                        .push(text(dive_stage.dive_step.time.to_string()))
                                        .spacing(10),
                                )
                                .spacing(10),
                        ))
                        .spacing(10)
                        .push(Card::new(
                            "Cylinder",
                            column!()
                                .push(
                                    row!()
                                        .push(text("Volume (l):"))
                                        .spacing(10)
                                        .push(text(dive_stage.cylinder.volume.to_string()))
                                        .spacing(10),
                                )
                                .spacing(10)
                                .push(
                                    row!()
                                        .push(text("Pressure (bar):"))
                                        .spacing(10)
                                        .push(text(dive_stage.cylinder.pressure.to_string()))
                                        .spacing(10),
                                )
                                .spacing(10)
                                .push(
                                    row!()
                                        .push(text("Used (l):"))
                                        .spacing(10)
                                        .push(text(dive_stage.cylinder.gas_management.used))
                                        .spacing(10),
                                )
                                .spacing(10)
                                .push(
                                    row!()
                                        .push(text("Remaining (l):"))
                                        .spacing(10)
                                        .push(text(format!(
                                            "{}/{}",
                                            dive_stage.cylinder.gas_management.remaining,
                                            dive_stage.cylinder.initial_pressurised_cylinder_volume
                                        )))
                                        .spacing(10),
                                )
                                .spacing(10)
                                .push(Card::new(
                                    "Gas Mixture",
                                    column!()
                                        .push(
                                            row!()
                                                .push(text("Oxygen (%):"))
                                                .spacing(10)
                                                .push(text(dive_stage.cylinder.gas_mixture.oxygen))
                                                .spacing(10),
                                        )
                                        .spacing(10)
                                        .push(
                                            row!()
                                                .push(text("Nitrogen (%):"))
                                                .spacing(10)
                                                .push(text(
                                                    dive_stage.cylinder.gas_mixture.nitrogen,
                                                ))
                                                .spacing(10),
                                        )
                                        .spacing(10)
                                        .push(
                                            row!()
                                                .push(text("Helium (%):"))
                                                .spacing(10)
                                                .push(text(dive_stage.cylinder.gas_mixture.helium))
                                                .spacing(10),
                                        )
                                        .spacing(10),
                                ))
                                .spacing(10),
                        ))
                        .spacing(10),
                ),
            );
        }

        result_cards
    }
}
