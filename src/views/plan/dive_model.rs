use crate::{
    application::{messages::message::Message, states::selectable_dive_model::SelectableDiveModel},
    models::application::dive_planner::DivePlanner,
};
use iced::{
    Length,
    widget::{Column, column, pick_list, text},
};
use iced_aw::Card;

impl DivePlanner {
    pub fn select_dive_model_view(&self) -> Column<'_, Message> {
        match self.dive_planning.is_planning {
            true => column!().push(Card::new(
                "Select Dive Model",
                pick_list(
                    &SelectableDiveModel::ALL[..],
                    self.dive_planning.select_dive_model.selected_dive_model,
                    Message::DiveModelSelectionOnSelect,
                )
                .width(Length::Fill)
                .placeholder("Select Dive Model"),
            )),
            false => match self.dive_planning.select_dive_model.selected_dive_model {
                Some(selected_dive_model) => column!().push(Card::new(
                    "Selected Dive Model",
                    text!("{}", selected_dive_model),
                )),
                None => {
                    column!()
                }
            },
        }
    }
}
