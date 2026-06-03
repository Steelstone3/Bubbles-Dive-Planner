use crate::application::input_parser::parse_input_u32;
use iced::wgpu::wgc::validation;
use serde::{Deserialize, Serialize};

#[derive(Debug, PartialEq, Clone, Default, Serialize, Deserialize)]
pub struct DiveStep {
    pub depth: u32,
    pub time: u32,
}

impl DiveStep {
    // TODO test
    pub fn new(mut depth: u32, mut time: u32) -> Self {
        const MAXIMUM_DEPTH_VALUE: u32 = 100;
        const MINIMUM_DEPTH_VALUE: u32 = 1;
        const MAXIMUM_TIME_VALUE: u32 = 60;
        const MINIMUM_TIME_VALUE: u32 = 1;

        if depth > MAXIMUM_DEPTH_VALUE {
            depth = MAXIMUM_DEPTH_VALUE;
        } else if depth < MINIMUM_DEPTH_VALUE {
            depth = MINIMUM_DEPTH_VALUE
        }

        if time > MAXIMUM_DEPTH_VALUE {
            time = MAXIMUM_DEPTH_VALUE;
        } else if time < MINIMUM_DEPTH_VALUE {
            time = MINIMUM_DEPTH_VALUE
        }

        Self { depth, time }
    }
}

#[cfg(test)]
mod dive_step_should {
}
