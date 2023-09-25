use iced::widget::{button, column, text, Button, Column, Text};
use iced_aw::Card;

use crate::{
    commands::messages::Message,
    models::{decompression_steps::DecompressionSteps, dive_step::DiveStep},
};

use super::decompression_step::DecompressionStepView;

pub struct DecompressionStepsView<'a> {
    pub decompression_steps_title_text: Text<'a>,
    pub decompression_steps_text: Column<'a, Message>,
    pub calculate_decompression: Button<'a, Message>,
}

impl DecompressionStepsView<'_> {
    pub fn new(decompression_steps: &DecompressionSteps) -> Self {
        let decompression_step_views =
            DecompressionStepsView::to_decompression_step_views(&decompression_steps.dive_steps);
        let cards = DecompressionStepsView::to_cards(decompression_step_views);
        let column = DecompressionStepsView::to_column(cards);

        Self {
            decompression_steps_title_text: text("Decompression Steps"),
            decompression_steps_text: column,
            calculate_decompression: button("Update Dive Profile"),
        }
    }

    fn to_decompression_step_views<'a>(
        dive_steps: &Vec<DiveStep>,
    ) -> Vec<DecompressionStepView<'a>> {
        let mut decompression_step_views = vec![];

        for dive_step in dive_steps {
            decompression_step_views.push(DecompressionStepView::new(dive_step));
        }

        decompression_step_views
    }

    fn to_cards(
        decompression_step_views: Vec<DecompressionStepView<'_>>,
    ) -> Vec<Card<'_, Message>> {
        let mut cards = vec![];

        for decompression_step_view in decompression_step_views {
            cards.push(decompression_step_view.decompression_step_text);
        }

        cards
    }

    fn to_column(cards: Vec<Card<'_, Message>>) -> Column<'_, Message> {      
        let mut column = column![];

        for card in cards.into_iter() {
            column = column.push(card);
        }

        column
    }
}
