use crate::view_models::dive_planner::DivePlanner;

use super::gas_mixture::GasMixtureView;

pub struct CylinderView<'a> {
    pub gas_mixture: GasMixtureView<'a>,
}

impl CylinderView<'_> {
    pub fn new(dive_planner: &DivePlanner) -> Self {
        Self {
            gas_mixture: GasMixtureView::new(&dive_planner),
        }
    }
}
