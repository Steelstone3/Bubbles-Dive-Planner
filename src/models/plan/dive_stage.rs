use crate::models::plan::{
    cylinders::cylinder::Cylinder, dive_model::DiveModel, dive_step::DiveStep,
};
use serde::{Deserialize, Serialize};
use std::collections::VecDeque;

#[derive(Debug, PartialEq, Clone, Default, Serialize, Deserialize)]
pub struct DiveStage {
    pub dive_model: DiveModel,
    pub dive_step: DiveStep,
    pub cylinder: Cylinder,
    pub decompression_steps: VecDeque<DiveStep>,
}

impl DiveStage {
    pub fn new(dive_model: DiveModel, dive_step: DiveStep, cylinder: Cylinder) -> Self {
        Self {
            dive_model,
            dive_step,
            cylinder,
            decompression_steps: Default::default(),
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
mod dive_stage_should {
    use crate::models::plan::{
        cylinders::{cylinder::Cylinder, gas_mixture::GasMixture}, dive_model::DiveModel, dive_stage::DiveStage,
        dive_step::DiveStep,
    };

    #[test]
    fn test_is_valid() {
        // Given
        let dive_stage = DiveStage::new(
            DiveModel::new_zhl16_dive_model(),
            DiveStep::new(50, 10),
            Cylinder::new(12,200,GasMixture::new(21,0),12),
        );

        // When
        let is_valid = dive_stage.is_valid();

        // Then
        assert_eq!(true, is_valid)
    }

        #[test]
    fn test_is_valid_invalid_dive_step() {
        // Given
        let dive_stage = DiveStage::new(
            DiveModel::new_zhl16_dive_model(),
            DiveStep::new(0, 0),
            Cylinder::new(12,200,GasMixture::new(21,0),12),
        );

        // When
        let is_valid = dive_stage.is_valid();

        // Then
        assert_eq!(false, is_valid)
    }

            #[test]
    fn test_is_valid_invalid_cylinder() {
        // Given
        let dive_stage = DiveStage::new(
            DiveModel::new_zhl16_dive_model(),
            DiveStep::new(50, 10),
            Cylinder::new(0,0,GasMixture::new(100,100),100),
        );

        // When
        let is_valid = dive_stage.is_valid();

        // Then
        assert_eq!(false, is_valid)
    }
}
