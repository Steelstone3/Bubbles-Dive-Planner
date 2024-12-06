use crate::{
    commands::{messages::Message, selectable_dive_model::SelectableDiveModel},
    models::application::dive_planner::DivePlanner,
};
use iced::{
    widget::{column, pick_list, text, Column},
    Length,
};
use iced_aw::Card;

impl DivePlanner {
    pub fn select_dive_model_view(&self) -> Column<Message> {
        match self.application_state.is_planning {
            true => column!().push(Card::new(
                "Select Dive Model",
                pick_list(
                    &SelectableDiveModel::ALL[..],
                    self.select_dive_model.selected_dive_model,
                    Message::SelectedDiveModelChanged,
                )
                .width(Length::Fill)
                .placeholder("Select Dive Model"),
            )),
            false => match self.select_dive_model.selected_dive_model {
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
