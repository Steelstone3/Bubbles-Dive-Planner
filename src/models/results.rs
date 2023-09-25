use crate::models::dive_stage::DiveStage;
use serde::{Deserialize, Serialize};

#[derive(Debug, Default, Clone, PartialEq, Serialize, Deserialize)]
pub struct DiveResults {
    pub is_visible: bool,
    pub results: Vec<DiveStage>,
}
