use crate::models::plan::{
    cylinders::cylinder::Cylinder, dive_model::DiveModel, dive_step::DiveStep,
};
use serde::{Deserialize, Serialize};

#[derive(Debug, PartialEq, Clone, Default, Serialize, Deserialize)]
pub struct DiveStage {
    pub dive_model: DiveModel,
    // TODO Need to work out validation
    pub dive_step: DiveStep,
    // TODO Need to work out validation
    pub cylinder: Cylinder,
}

impl DiveStage {
    pub fn new(dive_model: DiveModel, dive_step: DiveStep, cylinder: Cylinder) -> Self {
        Self {
            dive_model,
            dive_step,
            cylinder,
        }
    }

    pub fn is_valid(&self) -> bool {
        if !self.dive_step.is_valid() || !self.cylinder.is_valid() {
            return false;
        }

        true
    }
}

#[cfg(test)]
mod dive_stage_should {}
