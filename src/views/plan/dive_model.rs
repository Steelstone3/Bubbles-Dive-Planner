use crate::{
    commands::{messages::Message, selectable_dive_model::SelectableDiveModel},
    models::dive_planner::DivePlanner,
};
use iced::{
    widget::{column, pick_list, Column},
    Length,
};
use iced_aw::Card;

impl DivePlanner {
    pub fn select_dive_model_view(&self) -> Column<Message> {
        match self.is_planning {
            true => column!().push(Card::new(
                "Select Dive Model",
                pick_list(
                    &SelectableDiveModel::ALL[..],
                    self.select_dive_model.selected_dive_model,
                    Message::DiveModelSelected,
                )
                .width(Length::Fill)
                .placeholder("Select Dive Model"),
            )),
            false => column!(),
        }
    }
}
