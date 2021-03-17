use crate::models::application::dive_planner::DivePlanner;

impl DivePlanner {
    pub fn edit_undo(&mut self) {
        if self.is_undoable() {
            let latest = self.dive_stage;
            self.application_state.redo_buffer.push(latest);
            self.dive_results.results.pop();
            self.dive_stage = *self
                .dive_results
                .results
                .last()
                .unwrap_or(&Default::default());

            // Refresh decompression steps
            self.decompression_steps
                .assign_decompression_steps(self.dive_stage.calculate_decompression_dive_steps());
        } else {
            self.file_new();
        }
    }

    pub fn edit_redo(&mut self) {
        if self.is_redoable() {
            let redo = self.application_state.redo_buffer.pop().unwrap_or_default();
            self.dive_results.results.push(redo);
            self.dive_stage = redo;

            // Refresh decompression steps
            self.decompression_steps
                .assign_decompression_steps(self.dive_stage.calculate_decompression_dive_steps());
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
            information::decompression_steps::DecompressionSteps,
            plan::{dive_stage::DiveStage, dive_step::DiveStep},
            result::results::DiveResults,
        },
        test::test_fixture::dive_stage_test_fixture,
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
            dive_results: DiveResults {
                results: vec![],
                ..Default::default()
            },
            application_state: ApplicationState {
                redo_buffer: vec![],
                ..Default::default()
            },
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: Default::default(),
            dive_results: DiveResults {
                results: vec![],
                ..Default::default()
            },
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
            dive_stage,
            dive_results: DiveResults {
                results: vec![dive_stage],
                ..Default::default()
            },
            application_state: ApplicationState {
                redo_buffer: vec![],
                ..Default::default()
            },
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: Default::default(),
            dive_results: DiveResults {
                results: vec![],
                ..Default::default()
            },
            application_state: ApplicationState {
                redo_buffer: vec![dive_stage],
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
            dive_stage,
            dive_results: DiveResults {
                results: vec![dive_stage, dive_stage],
                ..Default::default()
            },
            application_state: ApplicationState {
                redo_buffer: vec![],
                ..Default::default()
            },
            decompression_steps: decompression_steps_test_fixture(),
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage,
            dive_results: DiveResults {
                results: vec![dive_stage],
                ..Default::default()
            },
            application_state: ApplicationState {
                redo_buffer: vec![dive_stage],
                ..Default::default()
            },
            decompression_steps: decompression_steps_test_fixture(),
            ..Default::default()
        };

        // When
        dive_planner.edit_undo();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn undo_dive_stage_recalculates_decompression_steps() {
        let dive_stage = dive_stage_test_fixture();
        let mut dive_planner = DivePlanner {
            dive_stage,
            dive_results: DiveResults {
                results: vec![dive_stage, dive_stage],
                ..Default::default()
            },
            application_state: ApplicationState {
                redo_buffer: vec![],
                ..Default::default()
            },
            decompression_steps: large_decompression_steps_test_fixture(),
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage,
            dive_results: DiveResults {
                results: vec![dive_stage],
                ..Default::default()
            },
            application_state: ApplicationState {
                redo_buffer: vec![dive_stage],
                ..Default::default()
            },
            decompression_steps: decompression_steps_test_fixture(),
            ..Default::default()
        };

        // When
        dive_planner.edit_undo();

        // Then
        assert_eq!(
            expected_dive_planner.decompression_steps,
            dive_planner.decompression_steps
        );
    }

    #[test]
    fn redo_when_buffer_is_empty() {
        // Given
        let dive_stage = dive_stage_test_fixture();
        let mut dive_planner = DivePlanner {
            dive_stage,
            dive_results: DiveResults {
                results: vec![dive_stage, dive_stage],
                ..Default::default()
            },
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage,
            dive_results: DiveResults {
                results: vec![dive_stage, dive_stage],
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
        let dive_stage = dive_stage_test_fixture();
        let mut dive_planner = DivePlanner {
            dive_stage,
            dive_results: DiveResults {
                results: vec![dive_stage],
                ..Default::default()
            },
            application_state: ApplicationState {
                redo_buffer: vec![dive_stage],
                ..Default::default()
            },
            decompression_steps: decompression_steps_test_fixture(),
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage,
            dive_results: DiveResults {
                results: vec![dive_stage, dive_stage],
                ..Default::default()
            },
            decompression_steps: decompression_steps_test_fixture(),
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
            dive_stage,
            dive_results: DiveResults {
                results: vec![],
                ..Default::default()
            },
            application_state: ApplicationState {
                redo_buffer: vec![dive_stage, dive_stage],
                ..Default::default()
            },
            decompression_steps: decompression_steps_test_fixture(),
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage,
            dive_results: DiveResults {
                results: vec![dive_stage],
                ..Default::default()
            },
            application_state: ApplicationState {
                redo_buffer: vec![dive_stage],
                ..Default::default()
            },
            decompression_steps: decompression_steps_test_fixture(),
            ..Default::default()
        };

        // When
        dive_planner.edit_redo();

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn redo_dive_stage_recalculates_decompression_steps() {
        // Given
        let dive_stage = dive_stage_test_fixture();
        let mut dive_planner = DivePlanner {
            dive_stage,
            dive_results: DiveResults {
                results: vec![],
                ..Default::default()
            },
            application_state: ApplicationState {
                redo_buffer: vec![dive_stage, dive_stage],
                ..Default::default()
            },
            decompression_steps: large_decompression_steps_test_fixture(),
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage,
            dive_results: DiveResults {
                results: vec![dive_stage],
                ..Default::default()
            },
            application_state: ApplicationState {
                redo_buffer: vec![dive_stage],
                ..Default::default()
            },
            decompression_steps: decompression_steps_test_fixture(),
            ..Default::default()
        };

        // When
        dive_planner.edit_redo();

        // Then
        assert_eq!(
            expected_dive_planner.decompression_steps,
            dive_planner.decompression_steps
        );
    }

    fn decompression_steps_test_fixture() -> DecompressionSteps {
        DecompressionSteps {
            dive_steps: vec![
                DiveStep { depth: 6, time: 1 },
                DiveStep { depth: 3, time: 3 },
            ],
        }
    }

    fn large_decompression_steps_test_fixture() -> DecompressionSteps {
        DecompressionSteps {
            dive_steps: vec![
                DiveStep {
                    depth: 15,
                    time: 10,
                },
                DiveStep { depth: 12, time: 6 },
                DiveStep { depth: 6, time: 2 },
                DiveStep { depth: 3, time: 7 },
            ],
        }
    }
}
