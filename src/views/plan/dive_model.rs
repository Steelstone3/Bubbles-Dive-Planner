use crate::{
    commands::{messages::Message, selectable_dive_model::SelectableDiveModel},
    view_models::dive_planner::DivePlanner,
};
use iced::{
    widget::{column, pick_list, Column},
    Length,
};

impl DivePlanner {
    pub fn select_dive_model_view(&self) -> Column<Message> {
        column!().push(
            pick_list(
                &SelectableDiveModel::ALL[..],
                self.select_dive_model.selected_dive_model,
                Message::DiveModelSelected,
            )
            .width(Length::Fill)
            .placeholder("Select Dive Model"),
        )
    }
}
