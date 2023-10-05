use crate::{
    commands::messages::Message, view_models::dive_planner::DivePlanner,
    views::information::cylinder_read_only::CylinderReadOnlyView,
};
use iced::widget::{button, column, Button, Column};

use super::{
    cylinder_parameters::cylinder::CylinderView, dive_step::DiveStepView,
    select_cylinder::SelectCylinderView, select_dive_model::SelectDiveModelView,
};

pub struct DiveStageView<'a> {
    select_dive_model: Column<'a, Message>,
    dive_step: Column<'a, Message>,
    cylinder: Column<'a, Message>,
    select_cylinder: Column<'a, Message>,
    cylinder_read_only: Column<'a, Message>,
    update_dive_profile: Column<'a, Message>,
}

impl DiveStageView<'_> {
    pub fn build_view<'a>(dive_planner: &DivePlanner) -> Column<'a, Message> {
        let dive_stage = Self::new(dive_planner);

        column![
            dive_stage.select_dive_model.spacing(10.0),
            dive_stage.dive_step.spacing(10.0),
            dive_stage.cylinder.spacing(10.0),
            dive_stage.select_cylinder.spacing(10.0),
            dive_stage.cylinder_read_only.spacing(10.0),
            dive_stage.update_dive_profile.spacing(10.0)
        ]
        .padding(10.0)
        .spacing(10.0)
    }

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
            select_cylinder: SelectCylinderView::build_view(
                dive_planner.select_cylinder.is_visible,
                dive_planner.dive_stage.cylinder.is_read_only,
                &dive_planner.select_cylinder,
            ),
            cylinder_read_only: CylinderReadOnlyView::build_view(&dive_planner.dive_stage.cylinder),
            // TODO NEXT VERSION refactor into a view
            update_dive_profile: column![DiveStageView::is_update_dive_profile_button_enabled(
                dive_planner
            )]
            .padding(10.0)
            .spacing(10.0),
        }
    }

    fn is_update_dive_profile_button_enabled<'a>(
        dive_planner: &DivePlanner,
    ) -> Button<'a, Message> {
        if !dive_planner.dive_stage.validate() {
            return button("Invalid Parameters");
        }

        button("Update Dive Profile").on_press(Message::UpdateDiveProfile)
    }
}
