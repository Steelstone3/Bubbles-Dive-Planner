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

#[cfg(test)]
mod dive_planner_should {
    #[test]
    #[ignore = "not implemented"]
    fn update() {}
}
