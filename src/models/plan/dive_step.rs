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
    // TODO test
    pub fn new(depth: u32, time: u32) -> Self {
        Self { depth, time }
    }

    // TODO test
    pub fn is_valid(&self) -> bool {
        if self.depth > MAXIMUM_DEPTH_VALUE {
            return false;
        } else if self.depth < MINIMUM_DEPTH_VALUE {
            return false;
        } else if self.time > MAXIMUM_TIME_VALUE {
            return false;
        } else if self.time < MINIMUM_TIME_VALUE {
            return false;
        }

        true
    }

    // TODO test
    pub fn update_depth(depth: String) -> u32 {
        parse_input_u32(depth, MINIMUM_DEPTH_VALUE, MAXIMUM_DEPTH_VALUE)
    }

    // TODO test
    pub fn update_time(time: String) -> u32 {
        parse_input_u32(time, MINIMUM_TIME_VALUE, MAXIMUM_TIME_VALUE)
    }
}

#[cfg(test)]
mod dive_step_should {}
