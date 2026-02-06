use crate::{
    application::messages::message::Message, models::application::dive_planner::DivePlanner,
    views::dive_results_table::dive_results_table,
};
use iced::{
    Renderer, Theme,
    widget::{column, text},
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
            let footer = format!(
                "Parameters\n\n{}\n\n{}",
                dive_stage.dive_step,
                dive_stage.cylinder.display_cylinder_summary()
            )
            .to_string();

            result_cards.push(
                Card::new(
                    "Dive Profile",
                    dive_results_table(
                        &dive_stage.dive_model.dive_profile,
                        self.theme().palette().text,
                    ),
                )
                .foot(text(footer)),
            );
        }

        result_cards
    }
}
