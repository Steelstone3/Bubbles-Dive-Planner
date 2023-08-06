use super::{cylinder::Cylinder, dive_model::DiveModel, dive_step::DiveStep};

#[derive(Clone, Copy, Default)]
pub struct DiveStage {
    pub dive_model: DiveModel,
    pub dive_step: DiveStep,
    pub cylinder: Cylinder,
}
