use iced::widget::{text, Text};

use crate::view_models::dive_planner::DivePlanner;

pub struct GasManagementReadOnlyView<'a> {
    pub gas_management_read_only_text: Text<'a>,
    pub remaining_read_only_text: Text<'a>,
    pub remaining_read_only_text_value: Text<'a>,
    pub used_read_only_text: Text<'a>,
    pub used_read_only_text_value: Text<'a>,
    pub surface_air_consumption_rate_read_only_text: Text<'a>,
    pub surface_air_consumption_rate_read_only_text_value: Text<'a>,
}

impl GasManagementReadOnlyView<'_> {
    pub fn new(dive_planner: &DivePlanner) -> Self {
        Self {
            gas_management_read_only_text: text("Gas Management"),
            remaining_read_only_text: text("Remaining (l)"),
            remaining_read_only_text_value: text(
                dive_planner.dive_stage.cylinder.gas_management.remaining,
            ),
            used_read_only_text: text("Used (l)"),
            used_read_only_text_value: text(dive_planner.dive_stage.cylinder.gas_management.used),
            surface_air_consumption_rate_read_only_text: text("S.A.C Rate (l/min)"),
            surface_air_consumption_rate_read_only_text_value: text(
                dive_planner
                    .dive_stage
                    .cylinder
                    .gas_management
                    .surface_air_consumption_rate,
            ),
        }
    }
}
