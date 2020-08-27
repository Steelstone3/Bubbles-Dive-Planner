use super::application_state::ApplicationState;
use crate::{
    application::states::selectable_dive_model::SelectableDiveModel,
    models::{
        information::dive_information::DiveInformation,
        plan::{dive_planning::dive_pre_planning::DivePrePlanning, dive_stage::DiveStage},
        result::results::DiveResults,
    },
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

impl DivePlanner {
    pub fn new_from_load(dive_planner_file: DivePlannerFile) -> DivePlanner {
        DivePlanner {
            application_state: Default::default(),
            dive_planning: DivePrePlanning::new_from_load(dive_planner_file.dive_model),
            dive_stage: dive_planner_file.dive_stage,
            dive_information: dive_planner_file.dive_information,
            dive_results: dive_planner_file.dive_results,
        }
    }

    pub fn convert_to_dive_planner_file(&self) -> DivePlannerFile {
        DivePlannerFile {
            dive_model: self
                .dive_planning
                .select_dive_model
                .selected_dive_model
                .unwrap_or_default(),
            dive_stage: self.dive_stage,
            dive_information: self.dive_information.clone(),
            dive_results: self.dive_results.clone(),
        }
    }
}

#[derive(Debug, Clone, PartialEq, Serialize, Deserialize, Default)]
pub struct DivePlannerFile {
    pub dive_model: SelectableDiveModel,
    pub dive_stage: DiveStage,
    pub dive_information: DiveInformation,
    pub dive_results: DiveResults,
}
