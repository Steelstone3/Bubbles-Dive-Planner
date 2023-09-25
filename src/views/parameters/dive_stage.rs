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
    pub select_dive_model: SelectDiveModelView<'a>,
    pub dive_step: DiveStepView<'a>,
    pub cylinder: CylinderView<'a>,
    pub select_cylinder: SelectCylinderView<'a>,
    pub cylinder_read_only: CylinderReadOnlyView<'a>,
}

// TODO make the parameters here more specific
// TODO refactor down to dive_stage to make it clearer. Too many parameters smell
impl DiveStageView<'_> {
    pub fn new(dive_planner: &DivePlanner) -> Self {
        Self {
            select_dive_model: SelectDiveModelView::new(dive_planner),
            dive_step: DiveStepView::new(dive_planner),
            cylinder: CylinderView::new(dive_planner),
            select_cylinder: SelectCylinderView::new(dive_planner),
            cylinder_read_only: CylinderReadOnlyView::new(&dive_planner.dive_stage.cylinder),
        }
    }

    pub fn determine_view<'a>(
        dive_planner: &DivePlanner,
        select_dive_model: SelectDiveModelView<'a>,
        dive_step: DiveStepView<'a>,
        cylinder: CylinderView<'a>,
        select_cylinder: SelectCylinderView<'a>,
        cylinder_read_only: CylinderReadOnlyView<'a>,
    ) -> iced::widget::Column<'a, Message> {
        DiveStageView::determine_parameters_view(
            dive_planner,
            select_dive_model,
            dive_step,
            cylinder,
            select_cylinder,
            cylinder_read_only,
        )
    }

    fn determine_parameters_view<'a>(
        dive_planner: &DivePlanner,
        select_dive_model: SelectDiveModelView<'a>,
        dive_step: DiveStepView<'a>,
        cylinder: CylinderView<'a>,
        select_cylinder: SelectCylinderView<'a>,
        cylinder_read_only: CylinderReadOnlyView<'a>,
    ) -> Column<'a, Message> {
        column![
            Self::determine_dive_model_view(
                dive_planner.dive_stage.dive_model.is_read_only,
                select_dive_model
            ),
            dive_step.dive_step_text,
            dive_step.depth_text,
            dive_step.depth_input,
            dive_step.time_text,
            dive_step.time_input,
            Self::determine_cylinder_view(
                dive_planner.dive_stage.cylinder.is_read_only,
                cylinder
            ),
            Self::determine_select_cylinder_view(
                dive_planner.select_cylinder.is_visible,
                dive_planner.dive_stage.cylinder.is_read_only,
                select_cylinder
            ),
            cylinder_read_only.cylinder_read_only_text,
            DiveStageView::is_update_dive_profile_button_enabled(dive_planner)
        ]
    }

    fn determine_dive_model_view(
        is_read_only: bool,
        select_dive_model: SelectDiveModelView<'_>,
    ) -> Column<'_, Message> {
        if is_read_only {
            return column![];
        }

        column![select_dive_model.selectable_dive_model].spacing(10.0)
    }

    fn determine_cylinder_view(
        is_read_only: bool,
        cylinder: CylinderView<'_>,
    ) -> Column<'_, Message> {
        if is_read_only {
            return column![];
        }

        column![
            cylinder.cylinder_setup_text,
            cylinder.cylinder_volume_text,
            cylinder.cylinder_volume_input,
            cylinder.cylinder_pressure_text,
            cylinder.cylinder_pressure_input,
            cylinder.cylinder_initial_pressurised_cylinder_volume_text,
            cylinder.cylinder_initial_pressurised_cylinder_volume_text_value,
            cylinder.gas_management.surface_air_consumption_text,
            cylinder.gas_management.surface_air_consumption_input,
            cylinder.gas_mixture.gas_mixture_text,
            cylinder.gas_mixture.oxygen_text,
            cylinder.gas_mixture.oxygen_input,
            cylinder.gas_mixture.helium_text,
            cylinder.gas_mixture.helium_input,
            cylinder.gas_mixture.nitrogen_text,
            cylinder.gas_mixture.nitrogen_text_value,
        ]
        .spacing(10.0)
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
