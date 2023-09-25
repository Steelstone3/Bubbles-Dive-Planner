use crate::models::dive_step::DiveStep;
use serde::{Deserialize, Serialize};

// TODO PRIORITY add a is_visible: bool to this model which is set to true when dive_steps has values
#[derive(Debug, Default, Clone, PartialEq, Serialize, Deserialize)]
pub struct DecompressionSteps {
    pub is_visible: bool,
    pub dive_steps: Vec<DiveStep>,
}
