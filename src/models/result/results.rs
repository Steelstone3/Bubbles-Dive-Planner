use serde::{Deserialize, Serialize};

use crate::models::plan::dive_stage::DiveStage;

#[derive(Debug, Default, Clone, PartialEq, Serialize, Deserialize)]
pub struct DiveResults {
    pub results: Vec<DiveStage>,
}

impl DiveResults {
    pub fn add_dive_result(&mut self, dive_stage: DiveStage) {
        self.results.push(dive_stage);
    }
}
