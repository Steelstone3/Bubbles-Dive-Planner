use super::{cylinder::Cylinder, dive_model::DiveModel, dive_step::DiveStep};
use serde::{Deserialize, Serialize};

#[derive(Clone, Copy, Serialize, Deserialize)]
pub struct DiveStage {
    pub dive_model: DiveModel,
    pub dive_step: DiveStep,
    pub selected_cylinder: Cylinder,
}
