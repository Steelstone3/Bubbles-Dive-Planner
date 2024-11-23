use super::{
    application_state::ApplicationState, select_cylinder::SelectCylinder,
    select_dive_model::SelectDiveModel,
};
use crate::models::{
    information::{
        central_nervous_system_toxicity::CentralNervousSystemToxicity,
        decompression_steps::DecompressionSteps,
    },
    plan::dive_stage::DiveStage,
    result::results::DiveResults,
};
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Serialize, Deserialize, Default)]
pub struct DivePlanner {
    pub application_state: ApplicationState,
    pub select_dive_model: SelectDiveModel,
    pub select_cylinder: SelectCylinder,
    pub dive_stage: DiveStage,
    pub dive_results: DiveResults,
    pub decompression_steps: DecompressionSteps,
    pub cns_toxicity: CentralNervousSystemToxicity,
}
