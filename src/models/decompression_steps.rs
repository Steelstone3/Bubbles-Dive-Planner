use crate::models::dive_step::DiveStep;
use serde::{Deserialize, Serialize};

#[derive(Debug, Default, Clone, PartialEq, Serialize, Deserialize)]
pub struct DecompressionSteps {
    dive_steps: Vec<DiveStep>,
}