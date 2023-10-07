use serde::{Deserialize, Serialize};
use std::fmt::Display;

use crate::views::application::input_parser::parse_input_u32;

pub const MAXIMUM_DEPTH_VALUE: u32 = 100;
pub const MINIMUM_DEPTH_VALUE: u32 = 1;
pub const MAXIMUM_TIME_VALUE: u32 = 60;
pub const MINIMUM_TIME_VALUE: u32 = 1;

#[derive(Debug, PartialEq, Copy, Clone, Default, Serialize, Deserialize)]
pub struct DiveStep {
    pub depth: u32,
    pub time: u32,
}

impl DiveStep {
    pub fn update_depth(&mut self, depth: String) {
        self.depth = parse_input_u32(depth, MINIMUM_DEPTH_VALUE, MAXIMUM_DEPTH_VALUE)
    }

    pub fn update_time(&mut self, time: String) {
        self.time = parse_input_u32(time, MINIMUM_TIME_VALUE, MAXIMUM_TIME_VALUE)
    }

    pub fn validate(&self) -> bool {
        let depth_validation = self.depth < MINIMUM_DEPTH_VALUE || self.depth > MAXIMUM_DEPTH_VALUE;
        let time_validation = self.time < MINIMUM_TIME_VALUE || self.time > MAXIMUM_TIME_VALUE;

        if depth_validation || time_validation {
            return false;
        }

        true
    }

    fn display_dive_step(&self) -> String {
        format!(
            "Dive Step\nDepth: {} (m) Time: {} (min)",
            self.depth, self.time
        )
    }
}

impl Display for DiveStep {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        write!(f, "{}", self.display_dive_step())
    }
}

#[cfg(test)]
mod dive_step_should {
    use super::*;
    use rstest::rstest;

    #[test]
    fn update_depth_by_parsing_and_validating_input_successfully() {
        // Given
        let expected = 50;
        let input = "50".to_string();
        let mut dive_step = DiveStep {
            ..Default::default()
        };

        // When
        dive_step.update_depth(input);

        // Then
        assert_eq!(expected, dive_step.depth);
    }

    #[test]
    fn update_depth_by_parsing_an_input_beyond_range() {
        // Given
        let expected = 100;
        let input = "101".to_string();
        let mut dive_step = DiveStep {
            ..Default::default()
        };

        // When
        dive_step.update_depth(input);

        // Then
        assert_eq!(expected, dive_step.depth);
    }

    #[test]
    fn update_depth_by_being_unable_to_parse_input() {
        // Given
        let expected = 1;
        let input = "$%45sdg".to_string();
        let mut dive_step = DiveStep {
            ..Default::default()
        };

        // When
        dive_step.update_depth(input);

        // Then
        assert_eq!(expected, dive_step.depth);
    }

    #[test]
    fn update_time_by_parsing_and_validating_input_successfully() {
        // Given
        let expected = 10;
        let input = "10".to_string();
        let mut dive_step = DiveStep {
            ..Default::default()
        };

        // When
        dive_step.update_time(input);

        // Then
        assert_eq!(expected, dive_step.time);
    }

    #[test]
    fn update_time_by_parsing_an_input_beyond_range() {
        // Given
        let expected = 60;
        let input = "61".to_string();
        let mut dive_step = DiveStep {
            ..Default::default()
        };

        // When
        dive_step.update_time(input);

        // Then
        assert_eq!(expected, dive_step.time);
    }

    #[test]
    fn update_time_by_being_unable_to_parse_input() {
        // Given
        let expected = 1;
        let input = "$£61asd".to_string();
        let mut dive_step = DiveStep {
            ..Default::default()
        };

        // When
        dive_step.update_time(input);

        // Then
        assert_eq!(expected, dive_step.time);
    }

    #[test]
    fn display_read_only_dive_step() {
        // Given
        let dive_step = DiveStep {
            depth: 50,
            time: 10,
        };
        let expected_display = "Dive Step\nDepth: 50 (m) Time: 10 (min)";

        // Then
        assert_eq!(expected_display, dive_step.to_string());
    }

    #[rstest]
    #[case(50, 10, true)]
    #[case(101, 10, false)]
    #[case(0, 10, false)]
    #[case(50, 61, false)]
    #[case(50, 0, false)]
    fn validate(#[case] depth: u32, #[case] time: u32, #[case] is_valid: bool) {
        // Given
        let dive_step = DiveStep { depth, time };

        // When
        let is_valid_actual: bool = dive_step.validate();

        // Then
        assert_eq!(is_valid, is_valid_actual);
    }
}
