use serde::{Deserialize, Serialize};

use crate::{
    application::states::selectable_dive_model::SelectableDiveModel,
    models::plan::dive_planning::select_dive_model::SelectDiveModel,
};

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

impl DivePrePlanning {
    pub fn new_from_load(selected_dive_model: SelectableDiveModel) -> Self {
        Self {
            is_planning: false,
            select_dive_model: SelectDiveModel {
                dive_model_list: Default::default(),
                selected_dive_model: Some(selected_dive_model),
            },
        }
    }
}
