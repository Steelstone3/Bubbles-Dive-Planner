use crate::models::dive_stage::DiveStage;
use serde::{Deserialize, Serialize};

#[derive(Debug, Default, Clone, PartialEq, Serialize, Deserialize)]
pub struct DiveResults {
    pub is_visible: bool,
    pub results: Vec<DiveStage>,
}

impl DiveResults {
    pub fn add_dive_result(&mut self, dive_stage: DiveStage) {
        self.results.push(dive_stage);
    }
}
