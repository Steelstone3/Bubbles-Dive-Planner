use iced::Sandbox;

use crate::models::dive_stage::DiveStage;

pub struct DivePlanner {
    pub dive_stage: DiveStage,
}

impl Default for DivePlanner {
    fn default() -> Self {
        Self::new()
    }
}

#[derive(Debug, Clone)]
pub enum Message {
    CalculateDivePlan,
    DepthChanged(String),
    TimeChanged(String),
    OxygenChanged(String),
    HeliumChanged(String),
}
