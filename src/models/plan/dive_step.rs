use crate::application::input_parser::parse_input_u32;
use serde::{Deserialize, Serialize};

const MAXIMUM_DEPTH_VALUE: u32 = 100;
const MAXIMUM_TIME_VALUE: u32 = 60;
const MINIMUM_DEPTH_VALUE: u32 = 1;
const MINIMUM_TIME_VALUE: u32 = 1;

#[derive(Debug, PartialEq, Clone, Serialize, Deserialize)]
pub struct DiveStep {
    pub depth: u32,
    pub time: u32,
}

impl Default for DiveStep {
    fn default() -> Self {
        Self { depth: 1, time: 1 }
    }
}

impl DiveStep {
    pub fn new(depth: u32, time: u32) -> Self {
        Self { depth, time }
    }

    pub fn is_valid(&self) -> bool {
        if self.depth > MAXIMUM_DEPTH_VALUE
            || self.depth < MINIMUM_DEPTH_VALUE
            || self.time > MAXIMUM_TIME_VALUE
            || self.time < MINIMUM_TIME_VALUE
        {
            return false;
        }

        true
    }

    pub fn update_depth(depth: String) -> u32 {
        parse_input_u32(depth, MINIMUM_DEPTH_VALUE, MAXIMUM_DEPTH_VALUE)
    }

    pub fn update_time(time: String) -> u32 {
        parse_input_u32(time, MINIMUM_TIME_VALUE, MAXIMUM_TIME_VALUE)
    }
}

#[cfg(test)]
mod dive_step_should {
    use crate::models::plan::dive_step::DiveStep;
    use rstest::rstest;

    #[test]
    fn test_update_depth() {
        // given
        let expected_depth = 50;

        // when
        let depth = DiveStep::update_depth("50".to_string());

        // then
        pretty_assertions::assert_eq!(expected_depth, depth);
    }

    #[test]
    fn test_update_time() {
        // given
        let expected_time = 10;

        // when
        let time = DiveStep::update_time("10".to_string());

        // then
        pretty_assertions::assert_eq!(expected_time, time);
    }

    #[rstest]
    #[case(50, 10, true)]
    #[case(100, 60, true)]
    #[case(1, 1, true)]
    #[case(101, 10, false)]
    #[case(0, 10, false)]
    #[case(50, 61, false)]
    #[case(50, 0, false)]
    fn test_is_valid(#[case] depth: u32, #[case] time: u32, #[case] expected_is_valid: bool) {
        // given
        let dive_step = DiveStep { depth, time };

        // when
        let is_valid = dive_step.is_valid();

        // then
        pretty_assertions::assert_eq!(expected_is_valid, is_valid);
    }
}
