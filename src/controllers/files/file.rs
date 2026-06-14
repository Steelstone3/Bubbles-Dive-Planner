use super::file_io::{read_dive_planner_state, upsert_dive_planner_state};
use crate::models::application::dive_planner::DivePlanner;

impl DivePlanner {
    pub fn file_new(&mut self) {
        *self = DivePlanner::default();
    }

    pub fn file_save_application_state(&self, file_path: &str) {
        upsert_dive_planner_state(file_path, &DivePlanner::convert_to_dive_planner_file(self));
    }

    pub fn file_load(&mut self, file_path: &str) {
        *self = DivePlanner::new_from_load(read_dive_planner_state(file_path))
    }
}

#[cfg(test)]
mod file_should {
    use crate::{
        controllers::files::test_file_guard::file_guard::TestFileGuard,
        models::{
            application::{application_state::ApplicationState, dive_planner::DivePlanner},
            plan::dive_planning::dive_pre_planning::DivePrePlanning,
            result::results::DiveResults,
        },
        test_fixture::zhl16_dive_stage_test_fixture,
    };
    use std::fs;

    #[test]
    fn test_file_new() {
        // given
        let expected = DivePlanner::default();
        let mut dive_planner = DivePlanner {
            dive_stage: zhl16_dive_stage_test_fixture(),
            dive_results: DiveResults {
                results: vec![
                    zhl16_dive_stage_test_fixture(),
                    zhl16_dive_stage_test_fixture(),
                    zhl16_dive_stage_test_fixture(),
                ],
            },
            application_state: ApplicationState {
                redo_buffer: vec![zhl16_dive_stage_test_fixture()],
                ..Default::default()
            },
            ..Default::default()
        };

        // when
        dive_planner.file_new();

        // then
        assert_eq!(expected, dive_planner);
    }

    #[test]
    fn acceptance_test_file_saves_and_loads_application_state() {
        // given
        const DIVE_PLANNER_STATE_FILE_NAME: &str = "dive_planner_state_1.toml";
        let expected_dive_planner = DivePlanner {
            dive_planning: DivePrePlanning {
                is_planning: false,
                ..Default::default()
            },
            dive_stage: zhl16_dive_stage_test_fixture(),
            dive_results: DiveResults {
                results: vec![zhl16_dive_stage_test_fixture()],
            },
            ..Default::default()
        };
        let mut dive_planner = DivePlanner {
            dive_stage: zhl16_dive_stage_test_fixture(),
            dive_results: DiveResults {
                results: vec![zhl16_dive_stage_test_fixture()],
            },
            ..Default::default()
        };
        let _guard = TestFileGuard::new(DIVE_PLANNER_STATE_FILE_NAME);

        // when
        dive_planner.file_save_application_state(DIVE_PLANNER_STATE_FILE_NAME);
        dive_planner.file_load(DIVE_PLANNER_STATE_FILE_NAME);

        // then
        assert!(fs::metadata(DIVE_PLANNER_STATE_FILE_NAME).is_ok());
        pretty_assertions::assert_eq!(expected_dive_planner, dive_planner);
    }
}
