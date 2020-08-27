use crate::{
    application::states::tab_identifier::TabIdentifier, models::plan::dive_stage::DiveStage,
};
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Serialize, Deserialize)]
pub struct ApplicationState {
    pub tab_identifier: TabIdentifier,
    pub is_light_theme: bool,
    pub redo_buffer: Vec<DiveStage>,
}

impl Default for ApplicationState {
    fn default() -> Self {
        Self {
            tab_identifier: TabIdentifier::Plan,
            is_light_theme: Default::default(),
            redo_buffer: Default::default(),
        }
    }
}
