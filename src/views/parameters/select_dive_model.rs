use crate::{
    commands::{messages::Message, selectable_dive_model::SelectableDiveModel},
    models::select_dive_model::SelectDiveModel,
    view_models::dive_planner::DivePlanner,
};
use iced::{
    widget::{column, pick_list, Column, PickList},
    Length,
};

pub struct SelectDiveModelView<'a> {
    selectable_dive_model: PickList<'a, SelectableDiveModel, Message>,
}

impl SelectDiveModelView<'_> {
    pub fn build_view<'a>(dive_planner: &DivePlanner) -> Column<'a, Message> {
        if dive_planner.dive_stage.dive_model.is_read_only {
            return column![];
        }

        let select_dive_model = SelectDiveModelView::new(&dive_planner.select_dive_model);

        column![select_dive_model.selectable_dive_model]
            .spacing(10.0)
            .padding(10.0)
    }

    fn new(select_dive_model: &SelectDiveModel) -> Self {
        Self {
            selectable_dive_model: pick_list(
                &SelectableDiveModel::ALL[..],
                select_dive_model.selected_dive_model,
                Message::DiveModelSelected,
            )
            .width(Length::Fill)
            .placeholder("Select Dive Model"),
        }
    }
}
