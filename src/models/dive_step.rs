use serde::{Deserialize, Serialize};
use std::fmt::Display;

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
    fn display_read_only_dive_step() {
        // Given
        let dive_step = DiveStep {
            depth: 50,
            time: 10,
        };
        let expected_display = "Dive Step\nDepth: 50 (m) Time: 10 (min)";

        // When
        let display = dive_step.display_dive_step();

        // Then
        assert_eq!(expected_display, display);
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
