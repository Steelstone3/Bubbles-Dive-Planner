use super::{cylinder::Cylinder, dive_step::DiveStep, select_dive_model::SelectDiveModel};
use serde::{Deserialize, Serialize};

#[derive(Debug, PartialEq, Clone, Copy, Default, Serialize, Deserialize)]
pub struct DiveStage {
    pub dive_model: SelectDiveModel,
    pub dive_step: DiveStep,
    pub cylinder: Cylinder,
}
