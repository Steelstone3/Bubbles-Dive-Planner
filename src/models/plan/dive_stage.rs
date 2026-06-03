use crate::models::plan::{cylinders::cylinder::Cylinder, dive_model::DiveModel, dive_step::DiveStep};
use serde::{Deserialize, Serialize};

#[derive(Debug, PartialEq, Clone, Default, Serialize, Deserialize)]
pub struct DiveStage {
    pub dive_model: DiveModel,
    pub dive_step: DiveStep,
    pub cylinder: Cylinder,
}

impl DiveStage {
    
}

#[cfg(test)]
mod dive_stage_should {
   
}
