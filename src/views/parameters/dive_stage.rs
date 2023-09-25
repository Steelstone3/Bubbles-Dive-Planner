use crate::{
    commands::messages::Message, view_models::dive_planner::DivePlanner,
    views::information::cylinder_read_only::CylinderReadOnlyView,
};
use iced::{
    widget::{button, column, Button, Column},
    Length,
};

use super::{
    cylinder_parameters::cylinder::CylinderView, dive_step::DiveStepView,
    select_cylinder::SelectCylinderView, select_dive_model::SelectDiveModelView,
};

pub struct DiveStageView<'a> {
    select_dive_model: Column<'a, Message>,
    dive_step: Column<'a, Message>,
    cylinder: Column<'a, Message>,
    select_cylinder: SelectCylinderView<'a>,
    cylinder_read_only: CylinderReadOnlyView<'a>,
}

impl DiveStageView<'_> {
    pub fn build_view<'a>(dive_planner: &DivePlanner) -> Column<'a, Message> {
        let dive_stage = Self::new(dive_planner);

        column![
            dive_stage.select_dive_model.spacing(10.0).padding(10.0),
            dive_stage.dive_step.spacing(10.0).padding(10.0),
            dive_stage.cylinder.spacing(10.0).padding(10.0),
            Self::determine_select_cylinder_view(
                dive_planner.select_cylinder.is_visible,
                dive_planner.dive_stage.cylinder.is_read_only,
                dive_stage.select_cylinder
            ),
            dive_stage.cylinder_read_only.cylinder_read_only_text,
            DiveStageView::is_update_dive_profile_button_enabled(dive_planner)
        ]
        .padding(10.0)
        .spacing(10.0)
    }

    // TODO make the parameters here more specific
    fn new<'a>(dive_planner: &DivePlanner) -> DiveStageView<'a> {
        DiveStageView {
            select_dive_model: SelectDiveModelView::build_view(
                dive_planner.dive_stage.dive_model.is_read_only,
                &dive_planner.select_dive_model,
            ),
            dive_step: DiveStepView::build_view(&dive_planner.dive_stage.dive_step),
            cylinder: CylinderView::build_view(
                dive_planner.dive_stage.cylinder.is_read_only,
                &dive_planner.dive_stage.cylinder,
            ),
            select_cylinder: SelectCylinderView::new(dive_planner),
            cylinder_read_only: CylinderReadOnlyView::new(&dive_planner.dive_stage.cylinder),
        }
    }

    fn determine_select_cylinder_view(
        is_visible: bool,
        is_read_only: bool,
        select_cylinder: SelectCylinderView<'_>,
    ) -> Column<'_, Message> {
        if is_visible && !is_read_only {
            return column![
                select_cylinder.update_cylinder,
                select_cylinder.cylinder_read_only_text_title,
                select_cylinder.selectable_cylinder,
            ]
            .spacing(10.0);
        } else if is_visible && is_read_only {
            return column![
                select_cylinder.cylinder_read_only_text_title,
                select_cylinder.selectable_cylinder,
            ]
            .spacing(10.0);
        } else {
            return column![];
        }
    }

    fn is_update_dive_profile_button_enabled<'a>(
        dive_planner: &DivePlanner,
    ) -> Button<'a, Message> {
        if !dive_planner.dive_stage.validate() {
            return button("Invalid Parameters").width(Length::Fill);
        }

        button("Update Dive Profile").on_press(Message::UpdateDiveProfile)
    }
}
