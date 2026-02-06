use super::application_state::ApplicationState;
use crate::models::{
    information::dive_information::DiveInformation,
    plan::{dive_planning::dive_pre_planning::DivePrePlanning, dive_stage::DiveStage},
    result::results::DiveResults,
};
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Serialize, Deserialize, Default)]
pub struct DivePlanner {
    pub application_state: ApplicationState,
    pub dive_planning: DivePrePlanning,
    pub dive_stage: DiveStage,
    pub dive_information: DiveInformation,
    pub dive_results: DiveResults,
}
