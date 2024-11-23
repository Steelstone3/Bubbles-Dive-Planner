use crate::models::dive_planner::DivePlanner;

use super::file_io::{read_dive_planner_state, upsert_dive_planner_state, upsert_dive_results};

const DIVE_PLANNER_STATE_FILE_NAME: &str = "dive_planner_state.json";
const DIVE_PLAN: &str = "dive_plan.json";

impl DivePlanner {
    pub fn file_new(&mut self) {
        *self = DivePlanner::default();
    }

    pub fn file_save(&self) {
        upsert_dive_planner_state(DIVE_PLANNER_STATE_FILE_NAME, self);
        upsert_dive_results(DIVE_PLAN, &self.dive_results.results);
    }

    pub fn file_load(&mut self) {
        *self = read_dive_planner_state(DIVE_PLANNER_STATE_FILE_NAME)
    }
}

#[cfg(test)]
mod file_should {
    use crate::{
        controllers::files::file::{DIVE_PLAN, DIVE_PLANNER_STATE_FILE_NAME},
        models::results::DiveResults,
        test_fixture::dive_stage_test_fixture,
        models::dive_planner::DivePlanner,
    };
    use std::fs;

    #[test]
    fn reset_dive_planner_to_default_state() {
        // Given
        let expected = DivePlanner::default();
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture(),
            dive_results: DiveResults {
                results: vec![
                    dive_stage_test_fixture(),
                    dive_stage_test_fixture(),
                    dive_stage_test_fixture(),
                ],
                ..Default::default()
            },
            redo_buffer: vec![dive_stage_test_fixture()],
            ..Default::default()
        };

        // When
        dive_planner.file_new();

        // Then
        assert_eq!(expected, dive_planner);
    }

    #[test]
    fn file_saves_and_loads_acceptance_test() {
        // Given
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture(),
            dive_results: DiveResults {
                results: vec![dive_stage_test_fixture()],
                ..Default::default()
            },
            ..Default::default()
        };
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture(),
            dive_results: DiveResults {
                results: vec![dive_stage_test_fixture()],
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        dive_planner.file_save();
        dive_planner.file_load();

        // Then
        assert!(fs::metadata(DIVE_PLANNER_STATE_FILE_NAME).is_ok());
        assert!(fs::metadata(DIVE_PLANNER_STATE_FILE_NAME).unwrap().len() != 0);
        assert!(fs::metadata(DIVE_PLAN).is_ok());
        assert!(fs::metadata(DIVE_PLAN).unwrap().len() != 0);
        assert_eq!(expected_dive_planner, dive_planner);
    }
}
