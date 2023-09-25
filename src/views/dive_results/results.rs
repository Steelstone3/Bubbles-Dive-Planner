use iced::widget::{column, text, Column, Text};
use iced_aw::Card;

use crate::{
    commands::messages::Message,
    models::{dive_stage::DiveStage, results::DiveResults}, view_models::dive_planner::DivePlanner,
};

use super::result::ResultView;

pub struct ResultsView<'a> {
    pub result_title_text: Text<'a>,
    pub results_text: Column<'a, Message>,
}

impl ResultsView<'_> {
    pub fn new(dive_results: &DiveResults) -> Self {
        let result_views = ResultsView::to_result_views(&dive_results.results);
        let cards = ResultsView::to_cards(result_views);
        let column = ResultsView::to_column(cards);

        Self {
            result_title_text: text("Results"),
            results_text: column,
        }
    }

    fn to_result_views<'a>(dive_stages: &Vec<DiveStage>) -> Vec<ResultView<'a>> {
        let mut result_views = vec![];

        for dive_stage in dive_stages {
            result_views.push(ResultView::new(dive_stage));
        }

        result_views
    }

    fn to_cards(result_views: Vec<ResultView<'_>>) -> Vec<Card<'_, Message>> {
        let mut cards = vec![];

        for result_view in result_views {
            cards.push(result_view.result_text);
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

    // fn determine_view<'a>(dive_planner: &DivePlanner) -> Column<'a, Message>{
    //     if !dive_planner.dive_results.is_visible {
    //         return Default::default()
    //     }

    //     column![

    //     ]
    // }

    // fn determine_view<'a>(dive_results: &DiveResults) -> Text<'a> {
    //     if !dive_results.is_visible {
    //         return text("");
    //     }

    //     text("Results")
    // }
}
