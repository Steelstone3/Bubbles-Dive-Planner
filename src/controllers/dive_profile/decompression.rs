use std::collections::VecDeque;

use crate::models::{
    application::dive_planner::DivePlanner,
    plan::{dive_stage::DiveStage, dive_step::DiveStep},
};

impl DiveStage {
    pub fn calculate_decompression_dive_steps(&self) -> Vec<DiveStep> {
        if self
            .dive_model
            .dive_profile
            .tolerated_surface_pressure
            .get_dive_ceiling()
            <= 0.0
        {
            return Default::default();
        }

        let mut updated_dive_stage = self.clone();
        let mut decompression_steps = vec![];

        while updated_dive_stage.get_dive_ceiling() > 0.0 {
            let decompression_step = updated_dive_stage.calculate_decompression_dive_step();
            decompression_steps.push(decompression_step.clone());
            updated_dive_stage.dive_step = decompression_step.clone();

            updated_dive_stage = DivePlanner::update_dive_profile(&updated_dive_stage);
        }

        decompression_steps
    }

    // TODO Test
    pub fn decompression_update_dive_profile(&self) -> Vec<DiveStage> {
        let mut dive_stage = self.clone();
        let mut dive_results = vec![];

        let mut decompression_steps: VecDeque<DiveStep> =
            self.calculate_decompression_dive_steps().into();

        while let Some(decompression_step) = decompression_steps.pop_front() {
            dive_stage.dive_step = decompression_step;
            dive_stage = DivePlanner::update_dive_profile(&dive_stage.clone());
            dive_stage.decompression_steps = decompression_steps.clone();
            dive_results.push(dive_stage.clone());
        }

        dive_results
    }

    fn calculate_decompression_dive_step(&self) -> DiveStep {
        let mut updated_dive_stage = self.clone();
        let nearest_decompression_depth = self.find_nearest_decompression_depth();
        let mut time = 0;

        while updated_dive_stage.get_dive_ceiling() < nearest_decompression_depth as f32
            && updated_dive_stage.get_dive_ceiling() > 0.0
        {
            updated_dive_stage = DivePlanner::update_dive_profile(&updated_dive_stage);
            time += 1;
        }

        DiveStep::new(nearest_decompression_depth, time)
    }

    fn find_nearest_decompression_depth(&self) -> u32 {
        let step_interval = 3;
        let nearest_decompression_depth = (self
            .dive_model
            .dive_profile
            .tolerated_surface_pressure
            .get_dive_ceiling()
            / (step_interval as f32))
            .ceil() as u32
            * step_interval;
        nearest_decompression_depth
    }

    fn get_dive_ceiling(&self) -> f32 {
        self.dive_model
            .dive_profile
            .tolerated_surface_pressure
            .get_dive_ceiling()
    }
}

#[cfg(test)]
mod dive_stage_should {
    use crate::{models::plan::dive_step::DiveStep, test::test_fixture::dive_stage_test_fixture};

    #[test]
    fn test_calculate_decompression_dive_steps() {
        // Given
        let expected_decompression_steps = vec![DiveStep::new(6, 1), DiveStep::new(3, 4)];
        let dive_stage = dive_stage_test_fixture();

        // When
        let decompression_steps = dive_stage.calculate_decompression_dive_steps();

        // Then
        pretty_assertions::assert_eq!(expected_decompression_steps, decompression_steps);
    }
}
