use crate::models::application::dive_planner::DivePlanner;

impl DivePlanner {
    pub fn edit_undo(&mut self) {
        if self.is_undoable() {
            let latest = self.dive_stage.clone();
            self.application_state.redo_buffer.push(latest);
            self.dive_results.results.pop();
            self.dive_stage = self
                .dive_results
                .results
                .last()
                .unwrap_or(&Default::default())
                .clone();
        } else {
            self.file_new();
        }
    }

    pub fn edit_redo(&mut self) {
        if self.is_redoable() {
            let redo = self.application_state.redo_buffer.pop().unwrap_or_default();
            self.dive_results.results.push(redo.clone());
            self.dive_stage = redo;
        }
    }

    pub fn is_undoable(&self) -> bool {
        !self.dive_results.results.is_empty()
    }

    pub fn is_redoable(&self) -> bool {
        !self.application_state.redo_buffer.is_empty()
    }
}

#[cfg(test)]
mod edit_should {
    use crate::{
        models::{
            application::{application_state::ApplicationState, dive_planner::DivePlanner},
            plan::dive_stage::DiveStage,
            result::results::DiveResults,
        },
        test_fixture::dive_stage_test_fixture,
    };
    use rstest::rstest;

    #[rstest]
    #[case(vec![dive_stage_test_fixture()], true)]
    #[case(vec![], false)]
    fn is_undoable(#[case] results: Vec<DiveStage>, #[case] expected_is_undoable: bool) {
        // Given
        let dive_planner = DivePlanner {
            dive_results: DiveResults { results },
            ..Default::default()
        };

        // When
        let is_undoable = dive_planner.is_undoable();

        // Then
        assert_eq!(expected_is_undoable, is_undoable)
    }

    #[rstest]
    #[case(vec![dive_stage_test_fixture()], true)]
    #[case(vec![], false)]
    fn is_redoable(#[case] redo_buffer: Vec<DiveStage>, #[case] expected_is_redoable: bool) {
        // Given

        let dive_planner = DivePlanner {
            application_state: ApplicationState {
                redo_buffer,
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        let is_redoable = dive_planner.is_redoable();

        // Then
        assert_eq!(expected_is_redoable, is_redoable)
    }

    #[test]
    fn undo_a_dive_stage_with_no_results() {
        // Given
        let dive_stage = dive_stage_test_fixture();
        let mut dive_planner = DivePlanner {
            dive_stage,
            dive_results: DiveResults { results: vec![] },
            application_state: ApplicationState {
                redo_buffer: vec![],
                ..Default::default()
            },
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: Default::default(),
            dive_results: DiveResults { results: vec![] },
            application_state: ApplicationState {
                redo_buffer: vec![],
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        dive_planner.edit_undo();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn undo_a_dive_stage_with_one_result() {
        // Given
        let dive_stage = dive_stage_test_fixture();
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage.clone(),
            dive_results: DiveResults {
                results: vec![dive_stage.clone()],
            },
            application_state: ApplicationState {
                redo_buffer: vec![],
                ..Default::default()
            },
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: Default::default(),
            dive_results: DiveResults { results: vec![] },
            application_state: ApplicationState {
                redo_buffer: vec![dive_stage.clone()],
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        dive_planner.edit_undo();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn undo_a_dive_stage_with_multiple_results() {
        // Given
        let dive_stage = dive_stage_test_fixture();
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage.clone(),
            dive_results: DiveResults {
                results: vec![dive_stage.clone(), dive_stage.clone()],
            },
            application_state: ApplicationState {
                redo_buffer: vec![],
                ..Default::default()
            },
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage.clone(),
            dive_results: DiveResults {
                results: vec![dive_stage.clone()],
            },
            application_state: ApplicationState {
                redo_buffer: vec![dive_stage.clone()],
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        dive_planner.edit_undo();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn redo_when_buffer_is_empty() {
        // Given
        let dive_stage = dive_stage_test_fixture();
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage.clone(),
            dive_results: DiveResults {
                results: vec![dive_stage.clone(), dive_stage.clone()],
            },
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage.clone(),
            dive_results: DiveResults {
                results: vec![dive_stage.clone(), dive_stage.clone()],
            },
            ..Default::default()
        };

        // When
        dive_planner.edit_redo();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn redo_dive_stage() {
        // Given
        let dive_stage = dive_stage_test_fixture();
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage.clone(),
            dive_results: DiveResults {
                results: vec![dive_stage.clone()],
            },
            application_state: ApplicationState {
                redo_buffer: vec![dive_stage.clone()],
                ..Default::default()
            },
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage.clone(),
            dive_results: DiveResults {
                results: vec![dive_stage.clone(), dive_stage.clone()],
            },
            ..Default::default()
        };

        // When
        dive_planner.edit_redo();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn redo_multiple_dive_stages() {
        // Given
        let dive_stage = dive_stage_test_fixture();
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage.clone(),
            dive_results: DiveResults { results: vec![] },
            application_state: ApplicationState {
                redo_buffer: vec![dive_stage.clone(), dive_stage.clone()],
                ..Default::default()
            },
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage.clone(),
            dive_results: DiveResults {
                results: vec![dive_stage.clone()],
            },
            application_state: ApplicationState {
                redo_buffer: vec![dive_stage.clone()],
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        dive_planner.edit_redo();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }
}
