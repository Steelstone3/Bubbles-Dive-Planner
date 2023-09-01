use iced::widget::{column, text, Column, Text};
use iced_aw::Card;

use crate::{commands::messages::Message, models::dive_stage::DiveStage};

use super::result::ResultView;

pub struct ResultsView<'a> {
    pub result_title_text: Text<'a>,
    pub results_text: Column<'a, Message>,
}

impl ResultsView<'_> {
    pub fn new(dive_stages: &Vec<DiveStage>) -> Self {
        let results_view = ResultsView::to_result_view(dive_stages);
        let cards = ResultsView::to_cards(results_view);
        let column = ResultsView::to_column(cards);

        Self {
            result_title_text: text("Results"),
            results_text: column,
        }
    }

    fn to_result_view<'a>(dive_stages: &Vec<DiveStage>) -> Vec<ResultView<'a>> {
        let mut results_view = vec![];

        for dive_stage in dive_stages {
            results_view.push(ResultView::new(dive_stage));
        }

        results_view
    }

    fn to_cards(results_view: Vec<ResultView<'_>>) -> Vec<Card<'_, Message>> {
        let mut cards = vec![];

        for result_view in results_view {
            cards.push(result_view.result_text);
        }

        cards
    }

    fn to_column(cards: Vec<Card<'_, Message>>) -> Column<'_, Message> {
        let mut column: Column<'_, Message> = column![];

        for card in cards.into_iter() {
            column = column.push(card);
        }

        column
    }
}
