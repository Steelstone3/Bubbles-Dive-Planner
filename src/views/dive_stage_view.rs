use iced::widget::column;

use crate::{view_models::dive_planner::DivePlanner, commands::messages::Message};

use super::{
    cylinder::CylinderView, cylinder_read_only::CylinderReadOnlyView, dive_step::DiveStepView,
};

pub struct DiveStageView<'a> {
    pub dive_step: DiveStepView<'a>,
    pub cylinder: CylinderView<'a>,
    pub cylinder_read_only: CylinderReadOnlyView<'a>,
}

impl DiveStageView<'_> {
    pub fn new(dive_planner: &DivePlanner) -> Self {
        Self {
            dive_step: DiveStepView::new(dive_planner),
            cylinder: CylinderView::new(dive_planner),
            cylinder_read_only: CylinderReadOnlyView::new(dive_planner),
        }
    }

    pub fn determine_view<'a>(
        is_read_only: bool,
        dive_step: DiveStepView<'a>,
        cylinder: CylinderView<'a>,
        cylinder_read_only: CylinderReadOnlyView<'a>,
    ) -> iced::widget::Column<'a, Message> {
        if is_read_only {
            DiveStageView::create_read_only_view(dive_step, cylinder_read_only)
        } else {
            DiveStageView::create_setup_view(dive_step, cylinder)
        }
    }
    
    fn create_setup_view<'a>(
        dive_step: DiveStepView<'a>,
        cylinder: CylinderView<'a>,
    ) -> iced::widget::Column<'a, Message> {
        column![
            dive_step.dive_step_text,
            dive_step.depth_text,
            dive_step.depth_input,
            dive_step.time_text,
            dive_step.time_input,
            cylinder.cylinder_setup_text,
            cylinder.cylinder_volume_text,
            cylinder.cylinder_volume_input,
            cylinder.cylinder_pressure_text,
            cylinder.cylinder_pressure_input,
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
        .spacing(10)
    }
    
    fn create_read_only_view<'a>(
        dive_step: DiveStepView<'a>,
        cylinder_read_only: CylinderReadOnlyView<'a>,
    ) -> iced::widget::Column<'a, Message> {
        column![
            dive_step.dive_step_text,
            dive_step.depth_text,
            dive_step.depth_input,
            dive_step.time_text,
            dive_step.time_input,
            cylinder_read_only.cylinder_read_only_text,
            cylinder_read_only.cylinder_read_only_volume_text,
            cylinder_read_only.cylinder_read_only_volume_text_value,
            cylinder_read_only.cylinder_read_only_pressure_text,
            cylinder_read_only.cylinder_read_only_pressure_text_value,
            cylinder_read_only.cylinder_read_only_initial_pressurised_cylinder_volume_text,
            cylinder_read_only.cylinder_read_only_initial_pressurised_cylinder_volume_text_value,
            cylinder_read_only
                .gas_mixture_read_only
                .gas_mixture_read_only_text,
            cylinder_read_only
                .gas_mixture_read_only
                .oxygen_read_only_text,
            cylinder_read_only
                .gas_mixture_read_only
                .oxygen_read_only_text_value,
            cylinder_read_only
                .gas_mixture_read_only
                .helium_read_only_text,
            cylinder_read_only
                .gas_mixture_read_only
                .helium_read_only_text_value,
            cylinder_read_only
                .gas_mixture_read_only
                .nitrogen_read_only_text,
            cylinder_read_only
                .gas_mixture_read_only
                .nitrogen_read_only_text_value,
            cylinder_read_only
                .gas_management_read_only
                .gas_management_read_only_text,
            cylinder_read_only
                .gas_management_read_only
                .remaining_read_only_text,
            cylinder_read_only
                .gas_management_read_only
                .remaining_read_only_text_value,
            cylinder_read_only
                .gas_management_read_only
                .used_read_only_text,
            cylinder_read_only
                .gas_management_read_only
                .used_read_only_text_value,
            cylinder_read_only
                .gas_management_read_only
                .surface_air_consumption_rate_read_only_text,
            cylinder_read_only
                .gas_management_read_only
                .surface_air_consumption_rate_read_only_text_value,
        ]
        .spacing(10)
    }
}
