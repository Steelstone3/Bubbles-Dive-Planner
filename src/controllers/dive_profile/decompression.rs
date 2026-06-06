use crate::models::{
    application::dive_planner::DivePlanner,
    plan::{dive_stage::DiveStage, dive_step::DiveStep},
};

impl DiveStage {
    pub fn calculate_decompression_dive_steps(&self) -> DiveStage {
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
        // let mut decompression_steps = vec![];

        while (updated_dive_stage
            .dive_model
            .dive_profile
            .tolerated_surface_pressure
            .get_dive_ceiling()
            > 0.0)
        {
            match DivePlanner::update_dive_profile(&updated_dive_stage) {
                Some(mut dive_stage) => {
                    dive_stage
                        .decompression_steps
                        .push(dive_stage.calculate_decompression_dive_step());
                    updated_dive_stage = dive_stage
                }
                None => break,
            }
        }

        //         Some(dive_stage) => {
        //             depth = dive_stage.dive_step.depth;
        //             time += 1;
        //         }
        //         None => return None,
        //     }
        // }

        // let nearest_decompression_dive_step = DiveStage::find_nearest_decompression_depth(
        //     self.dive_model
        //         .dive_profile
        //         .tolerated_surface_pressure
        //         .get_dive_ceiling(),
        // );

        // match DiveStage::calculate_decompression_time_at_depth(&self) {
        //     Some(dive_step) => {
        //         dive_steps.push(dive_step);
        //     }
        //     None => (),
        // }

        updated_dive_stage
    }

    fn calculate_decompression_dive_step(&self) -> DiveStep {
        let mut updated_dive_stage = self.clone();
        let nearest_decompression_depth = self.find_nearest_decompression_depth();
        let mut time = 0;

        while DiveStage::get_dive_ceiling(&updated_dive_stage) < nearest_decompression_depth as f32
        {
            match DivePlanner::update_dive_profile(&self) {
                Some(dive_stage) => {
                    updated_dive_stage = dive_stage;
                    time += 1;
                }
                None => (),
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

    // fn find_nearest_decompression_depth(dive_ceiling: f32) -> Option<DiveStep> {
    //     let step_interval = 3;

    //     if dive_ceiling <= 0.0 {
    //         return None;
    //     }

    //     let nearest_decompression_depth =
    //         (dive_ceiling / (step_interval as f32)).ceil() as u32 * step_interval;

    //     Some(DiveStep::new(nearest_decompression_depth, 1))
    // }

    // fn calculate_decompression_time_at_depth(dive_stage: &DiveStage) -> Option<DiveStep> {
    //     let mut depth = 0;
    //     let mut time = 0;

    //     let nearest_decompression_depth = match DiveStage::find_nearest_decompression_depth(
    //         dive_stage
    //             .dive_model
    //             .dive_profile
    //             .tolerated_surface_pressure
    //             .get_dive_ceiling(),
    //     ) {
    //         Some(dive_step) => dive_step.depth,
    //         None => return None,
    //     };

    //     while dive_stage.dive_step.depth == nearest_decompression_depth {
    //         match DivePlanner::update_dive_profile(&dive_stage) {
    //             Some(dive_stage) => {
    //                 depth = dive_stage.dive_step.depth;
    //                 time += 1;
    //             }
    //             None => return None,
    //         }
    //     }

    //     Some(DiveStep::new(depth, time))
    // }
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
        let expected_decompression_steps = vec![DiveStep::new(6, 1), DiveStep::new(3, 3)];
        let dive_stage = dive_stage_test_fixture();

        // When
        let dive_stage = dive_stage.calculate_decompression_dive_steps();

        // Then
        pretty_assertions::assert_eq!(expected_decompression_steps, dive_stage.decompression_steps);
    }
}
