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

            match DivePlanner::update_dive_profile(&updated_dive_stage) {
                Some(dive_stage) => updated_dive_stage = dive_stage,
                None => break,
            }
        }

        decompression_steps
    }

    fn calculate_decompression_dive_step(&self) -> DiveStep {
        let mut updated_dive_stage = self.clone();
        let nearest_decompression_depth = self.find_nearest_decompression_depth();
        let mut time = 0;

        while updated_dive_stage.get_dive_ceiling() < nearest_decompression_depth as f32
            && updated_dive_stage.get_dive_ceiling() > 0.0
        {
            match DivePlanner::update_dive_profile(&updated_dive_stage) {
                Some(dive_stage) => {
                    updated_dive_stage = dive_stage;
                    time += 1;
                }
                None => break,
            }
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
    use crate::{
        models::plan::{dive_stage::DiveStage, dive_step::DiveStep},
        test::test_fixture::dive_stage_test_fixture,
    };

    #[test]
    fn test_calculate_decompression_dive_step() {
        // Given
        let expected_decompression_step = DiveStep::new(6, 1);
        let dive_stage = dive_stage_test_fixture();

        // When
        let decompression_step = DiveStage::calculate_decompression_dive_step(&dive_stage);

        // Then
        pretty_assertions::assert_eq!(expected_decompression_step, decompression_step);
    }

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
