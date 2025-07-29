use super::file_io::{read_dive_planner_state, upsert_dive_planner_state, upsert_dive_results};
use crate::models::application::dive_planner::DivePlanner;

impl DivePlanner {
    pub fn file_new(&mut self) {
        *self = DivePlanner::default();
    }

    pub fn file_save_application_state(&self, file_path: &str) {
        upsert_dive_planner_state(file_path, self);
    }

    pub fn file_save_results(&self, file_path: &str) {
        upsert_dive_results(file_path, &self.dive_results.results);
    }

    pub fn file_load(&mut self, file_path: &str) {
        *self = read_dive_planner_state(file_path)
    }
}

#[cfg(test)]
mod file_should {
    use crate::{
        models::{
            application::{application_state::ApplicationState, dive_planner::DivePlanner},
            result::results::DiveResults,
        },
        test_fixture::dive_stage_test_fixture,
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
            application_state: ApplicationState {
                redo_buffer: vec![dive_stage_test_fixture()],
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        dive_planner.file_new();

        // Then
        assert_eq!(expected, dive_planner);
    }

#[test]
    fn file_saves_results() {
        // Given
        const DIVE_PLAN: &str = "dive_planner.json";
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture(),
            dive_results: DiveResults {
                results: vec![dive_stage_test_fixture()],
                ..Default::default()
            },
            ..Default::default()
        };
        let dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture(),
            dive_results: DiveResults {
                results: vec![dive_stage_test_fixture()],
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        dive_planner.file_save_results(&DIVE_PLAN.to_string());

        // Then
        assert!(fs::metadata(DIVE_PLAN).is_ok());
        assert!(fs::metadata(DIVE_PLAN).unwrap().len() != 0);
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn file_saves_and_loads_application_state_acceptance_test() {
        // Given
        const DIVE_PLANNER_STATE_FILE_NAME: &str = "dive_planner_state.json";
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
        dive_planner.file_save_application_state(&DIVE_PLANNER_STATE_FILE_NAME.to_string());
        dive_planner.file_load(&DIVE_PLANNER_STATE_FILE_NAME.to_string());

        // Then
        assert!(fs::metadata(DIVE_PLANNER_STATE_FILE_NAME).is_ok());
        assert!(fs::metadata(DIVE_PLANNER_STATE_FILE_NAME).unwrap().len() != 0);
        assert_eq!(expected_dive_planner, dive_planner);
    }
}
