use crate::view_models::dive_planner::DivePlanner;

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
}
