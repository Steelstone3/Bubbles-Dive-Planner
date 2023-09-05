use crate::{
    commands::{messages::Message, selectable_dive_model::SelectableDiveModel},
    view_models::dive_planner::DivePlanner,
};
use iced::{
    widget::{pick_list, PickList},
    Length,
};

pub struct SelectDiveModelView<'a> {
    pub selectable_dive_model: PickList<'a, SelectableDiveModel, Message>,
}

impl SelectDiveModelView<'_> {
    pub fn new(dive_planner: &DivePlanner) -> Self {
        Self {
            selectable_dive_model: pick_list(
                &SelectableDiveModel::ALL[..],
                dive_planner.select_dive_model.selected_dive_model,
                Message::DiveModelSelected,
            )
            .width(Length::Fill)
            .placeholder("Select Dive Model"),
        }
    }
}
