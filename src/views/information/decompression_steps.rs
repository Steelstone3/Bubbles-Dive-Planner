use crate::{commands::messages::Message, models::application::dive_planner::DivePlanner};
use iced::{
    widget::{button, column, text},
    Renderer, Theme,
};
use iced_aw::widgets::Card;

impl DivePlanner {
    pub fn decompression_steps_view(&self) -> iced::widget::Column<Message> {
        let mut contents = column![];

        if !self.decompression_steps.dive_steps.is_empty() {
            for decompression_steps in self.decompression_steps_cards() {
                contents = contents.push(decompression_steps).spacing(10);
            }

            contents = contents
                .push(
                    button("Run Decompression Profile")
                        .on_press(Message::DecompressionUpdateDiveProfile),
                )
                .spacing(10);
        }

        contents
    }

    fn decompression_steps_cards(&self) -> Vec<Card<Message, Theme, Renderer>> {
        let mut decompression_steps_cards = vec![];

        for decompression_step in &self.decompression_steps.dive_steps {
            decompression_steps_cards.push(Card::new(
                "Decompression Step",
                text(decompression_step.to_string()),
            ))
        }

        decompression_steps_cards
    }
}
