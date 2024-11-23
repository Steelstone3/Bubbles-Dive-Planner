use crate::view_models::dive_planner::DivePlanner;

impl DivePlanner {
    pub fn edit_undo(&mut self) {
        if self.is_undoable() {
            let latest = self.dive_stage;
            self.redo_buffer.push(latest);
            self.dive_results.results.pop();
            self.dive_stage = *self
                .dive_results
                .results
                .last()
                .unwrap_or(&Default::default());
        } else {
            self.file_new();
        }
    }

    pub fn edit_redo(&mut self) {
        if self.is_redoable() {
            let redo = self.redo_buffer.pop().unwrap();
            self.dive_results.results.push(redo);
            self.dive_stage = redo;
        }
    }

    pub fn is_undoable(&self) -> bool {
        !self.dive_results.results.is_empty()
    }

    pub fn is_redoable(&self) -> bool {
        !self.redo_buffer.is_empty()
    }
}

#[cfg(test)]
mod edit_should {
    use crate::{
        models::{dive_stage::DiveStage, results::DiveResults},
        test_fixture::{dive_stage_test_fixture, dive_stage_test_fixture_default},
        view_models::dive_planner::DivePlanner,
    };
    use rstest::rstest;

    #[rstest]
    #[case(vec![dive_stage_test_fixture()], true)]
    #[case(vec![], false)]
    fn is_undoable(#[case] results: Vec<DiveStage>, #[case] expected_is_undoable: bool) {
        // Given
        let dive_planner = DivePlanner {
            dive_results: DiveResults {
                results,
                ..Default::default()
            },
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
            redo_buffer,
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
        let mut dive_planner = DivePlanner {
            dive_stage: Default::default(),
            dive_results: DiveResults {
                results: vec![],
                ..Default::default()
            },
            redo_buffer: vec![],
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: Default::default(),
            dive_results: DiveResults {
                results: vec![],
                ..Default::default()
            },
            redo_buffer: vec![],
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
        let default_array_old = [
            2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0,
        ];
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_default(default_array_old),
            dive_results: DiveResults {
                results: vec![dive_stage_test_fixture_default(default_array_old)],
                ..Default::default()
            },
            redo_buffer: vec![],
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: Default::default(),
            dive_results: DiveResults {
                results: vec![],
                ..Default::default()
            },
            redo_buffer: vec![dive_stage_test_fixture_default(default_array_old)],
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
        let default_array_old = [
            2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0,
        ];
        let default_array_latest = [
            4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0,
        ];
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_default(default_array_latest),
            dive_results: DiveResults {
                results: vec![
                    dive_stage_test_fixture_default(default_array_old),
                    dive_stage_test_fixture_default(default_array_latest),
                ],
                ..Default::default()
            },
            redo_buffer: vec![],
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_default(default_array_old),
            dive_results: DiveResults {
                results: vec![dive_stage_test_fixture_default(default_array_old)],
                ..Default::default()
            },
            redo_buffer: vec![dive_stage_test_fixture_default(default_array_latest)],
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
        let default_array_old = [
            2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0,
        ];
        let default_array_latest = [
            4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0,
        ];
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_default(default_array_latest),
            dive_results: DiveResults {
                results: vec![
                    dive_stage_test_fixture_default(default_array_old),
                    dive_stage_test_fixture_default(default_array_latest),
                ],
                ..Default::default()
            },
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_default(default_array_latest),
            dive_results: DiveResults {
                results: vec![
                    dive_stage_test_fixture_default(default_array_old),
                    dive_stage_test_fixture_default(default_array_latest),
                ],
                ..Default::default()
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
        let default_array_old = [
            2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0,
        ];
        let default_array_latest = [
            4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0,
        ];
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_default(default_array_old),
            dive_results: DiveResults {
                results: vec![dive_stage_test_fixture_default(default_array_old)],
                ..Default::default()
            },
            redo_buffer: vec![dive_stage_test_fixture_default(default_array_latest)],
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_default(default_array_latest),
            dive_results: DiveResults {
                results: vec![
                    dive_stage_test_fixture_default(default_array_old),
                    dive_stage_test_fixture_default(default_array_latest),
                ],
                ..Default::default()
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
        let default_array_old = [
            2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0,
        ];
        let default_array_latest = [
            4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0,
        ];
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_default(default_array_old),
            dive_results: DiveResults {
                results: vec![],
                ..Default::default()
            },
            redo_buffer: vec![
                dive_stage_test_fixture_default(default_array_old),
                dive_stage_test_fixture_default(default_array_latest),
            ],

            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_default(default_array_latest),
            dive_results: DiveResults {
                results: vec![dive_stage_test_fixture_default(default_array_latest)],
                ..Default::default()
            },
            redo_buffer: vec![dive_stage_test_fixture_default(default_array_old)],
            ..Default::default()
        };

        // When
        dive_planner.edit_redo();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }
}
