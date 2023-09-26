use iced::widget::{button, column, text, Button, Column, Text};
use iced_aw::Card;

use crate::{
    commands::messages::Message, models::dive_step::DiveStep,
    view_models::dive_planner::DivePlanner,
};

use super::decompression_step::DecompressionStepView;

// TODO refresh button isn't ideal should find a way to do it on cylinder change that doesn't cause regressions
pub struct DecompressionStepsView<'a> {
    decompression_steps_title_text: Text<'a>,
    decompression_steps_text: Column<'a, Message>,
    refresh_decompression: Button<'a, Message>,
    calculate_decompression: Button<'a, Message>,
}

impl DecompressionStepsView<'_> {
    pub fn build_view<'a>(dive_planner: &DivePlanner) -> Column<'a, Message> {
        let decompression_steps = DecompressionStepsView::new(dive_planner);

        if !dive_planner.decompression_steps.is_visible {
            return column![];
        }
        column![
            decompression_steps.decompression_steps_title_text,
            decompression_steps.decompression_steps_text,
            decompression_steps.refresh_decompression,
            decompression_steps.calculate_decompression,
        ]
        .spacing(10.0)
    }

    fn new<'a>(dive_planner: &DivePlanner) -> DecompressionStepsView<'a> {
        let decompression_step_views = DecompressionStepsView::to_decompression_step_views(
            &dive_planner.decompression_steps.dive_steps,
        );
        let cards = DecompressionStepsView::to_cards(decompression_step_views);
        let column = DecompressionStepsView::to_column(cards);

        DecompressionStepsView {
            decompression_steps_title_text: text("Decompression Steps"),
            decompression_steps_text: column,
            calculate_decompression: button("Update Dive Profile")
                .on_press(Message::DecompressionUpdateDiveProfile),
            refresh_decompression: button("Refresh").on_press(Message::RefreshDecompression),
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

    // fn determine_view<'a>() -> Column<'a, Message>{

    // }
}
