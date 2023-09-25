use crate::models::dive_step::DiveStep;
use serde::{Deserialize, Serialize};

#[derive(Debug, Default, Clone, PartialEq, Serialize, Deserialize)]
pub struct DecompressionSteps {
    pub is_visible: bool,
    pub dive_steps: Vec<DiveStep>,
}

// TODO test
impl DecompressionSteps {
    pub fn update_visibility(&mut self) {
        self.is_visible = !self.dive_steps.is_empty();
    }
}
