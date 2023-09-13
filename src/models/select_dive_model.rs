use crate::commands::selectable_dive_model::SelectableDiveModel;

use super::dive_model::DiveModel;
use serde::{Deserialize, Serialize};

#[derive(Debug, PartialEq, Copy, Clone, Serialize, Deserialize)]
pub struct SelectDiveModel {
    pub dive_model_list: [DiveModel; 2],
    pub selected_dive_model: Option<SelectableDiveModel>,
}

impl Default for SelectDiveModel {
    fn default() -> Self {
        Self {
            dive_model_list: Default::default(),
            selected_dive_model: Some(SelectableDiveModel::Bulhmann),
        }
    }
}
