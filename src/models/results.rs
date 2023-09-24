use serde::{Deserialize, Serialize};
use crate::models::dive_stage::DiveStage;

#[derive(Debug, Default, Clone, PartialEq, Serialize, Deserialize)]
pub struct DiveResults {
    pub results: Vec<DiveStage>,
}
