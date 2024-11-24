use crate::{commands::tab_identifier::TabIdentifier, models::{
    central_nervous_system_toxicity::CentralNervousSystemToxicity,
    decompression_steps::DecompressionSteps, dive_stage::DiveStage, results::DiveResults,
    select_cylinder::SelectCylinder, select_dive_model::SelectDiveModel,
}};
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Serialize, Deserialize)]
pub struct DivePlanner {
    pub tab_identifier: TabIdentifier,
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
        Self {
            tab_identifier: TabIdentifier::Plan,
            select_dive_model: Default::default(),
            select_cylinder: Default::default(),
            dive_stage: Default::default(),
            dive_results: Default::default(),
            decompression_steps: Default::default(),
            cns_toxicity: Default::default(),
            redo_buffer: Default::default(),
            is_planning: true,
        }
    }
}
