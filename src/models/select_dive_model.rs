use crate::commands::selectable_dive_model::SelectableDiveModel;

use super::dive_model::DiveModel;
use serde::{Deserialize, Serialize};

#[derive(Debug, PartialEq, Default, Copy, Clone, Serialize, Deserialize)]
pub struct SelectDiveModel {
    pub dive_model_list: [DiveModel; 2],
    pub selected_dive_model: Option<SelectableDiveModel>,
    pub dive_model: DiveModel,
}