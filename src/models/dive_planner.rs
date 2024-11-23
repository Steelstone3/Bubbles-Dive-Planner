use crate::models::{
    central_nervous_system_toxicity::CentralNervousSystemToxicity,
    decompression_steps::DecompressionSteps, dive_stage::DiveStage, results::DiveResults,
    select_cylinder::SelectCylinder, select_dive_model::SelectDiveModel,
};
use iced::Sandbox;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Serialize, Deserialize)]
pub struct DivePlanner {
    pub select_dive_model: SelectDiveModel,
    pub select_cylinder: SelectCylinder,
    pub dive_stage: DiveStage,
    pub dive_results: DiveResults,
    pub decompression_steps: DecompressionSteps,
    pub cns_toxicity: CentralNervousSystemToxicity,
    pub redo_buffer: Vec<DiveStage>,
    pub is_planning: bool,
}

impl Default for DivePlanner {
    fn default() -> Self {
        Self::new()
    }
}
