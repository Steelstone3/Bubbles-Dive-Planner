use super::{
    gas_management_read_only::GasManagementReadOnlyView,
    gas_mixture_read_only::GasMixtureReadOnlyView,
};
use crate::view_models::dive_planner::DivePlanner;
use iced::{
    widget::Text,
    widget::text,
};

pub struct CylinderReadOnlyView<'a> {
    pub cylinder_read_only_text: Text<'a>,
    pub cylinder_read_only_volume_text: Text<'a>,
    pub cylinder_read_only_volume_input: Text<'a>,
    pub cylinder_read_only_pressure_text: Text<'a>,
    pub cylinder_read_only_pressure_input: Text<'a>,
    pub gas_mixture_read_only: GasMixtureReadOnlyView<'a>,
    pub gas_management_read_only: GasManagementReadOnlyView<'a>,
}

impl CylinderReadOnlyView<'_> {
    pub fn new(dive_planner: &DivePlanner) -> Self {
        Self {
            cylinder_read_only_text: text("Cylinder"),
            cylinder_read_only_volume_text: text("Volume (l)"),
            cylinder_read_only_volume_input: text(dive_planner.dive_stage.cylinder.volume),
            cylinder_read_only_pressure_text: text("Pressure (bar)"),
            cylinder_read_only_pressure_input: text(dive_planner.dive_stage.cylinder.pressure),
            gas_mixture_read_only: GasMixtureReadOnlyView::new(dive_planner),
            gas_management_read_only: GasManagementReadOnlyView::new(dive_planner),
        }
    }
}
