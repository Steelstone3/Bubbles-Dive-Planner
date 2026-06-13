use serde::{Deserialize, Serialize};

use crate::models::plan::dive_stage::DiveStage;

#[derive(Debug, Default, Clone, PartialEq, Serialize, Deserialize)]
pub struct DiveResults {
    pub results: Vec<DiveStage>,
}

#[cfg(test)]
mod dive_results_should {}
