use crate::{commands::messages::Message, models::application::dive_planner::DivePlanner};
use iced::{
    Renderer, Theme,
    widget::{column, text},
};
use iced_aw::widgets::Card;

impl DivePlanner {
    pub fn results_view(&self) -> iced::widget::Column<Message> {
        if self.application_state.is_planning {
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

    fn result_cards(&self) -> Vec<Card<Message, Theme, Renderer>> {
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
                    text(dive_stage.dive_model.dive_profile.to_string()),
                )
                .foot(text(footer)),
            );
        }

        result_cards
    }
}
