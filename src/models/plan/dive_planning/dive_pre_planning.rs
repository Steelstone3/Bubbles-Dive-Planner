use serde::{Deserialize, Serialize};

use crate::models::plan::dive_planning::select_dive_model::SelectDiveModel;

#[derive(Debug, PartialEq, Copy, Clone, Serialize, Deserialize)]
pub struct DivePrePlanning {
    pub is_planning: bool,
    pub select_dive_model: SelectDiveModel,
}

impl Default for DivePrePlanning {
    fn default() -> Self {
        Self {
            is_planning: true,
            select_dive_model: Default::default(),
        }
    }
}
