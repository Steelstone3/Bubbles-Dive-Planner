use crate::presenters::presenter::{parse_numeric_value, text_prompt};
use serde::{Deserialize, Serialize};

#[derive(PartialEq, Debug, Copy, Clone, Serialize, Deserialize, Default)]
pub struct DiveStep {
    pub depth: u32,
    pub time: u32,
}

impl DiveStep {
    pub fn new() -> Self {
        Self {
            depth: parse_numeric_value(text_prompt(
                "Enter depth (M):",
                "Enter a value 1 - 100",
                "1",
            )),
            time: parse_numeric_value(text_prompt(
                "Enter time (min):",
                "Enter a value 1 - 60",
                "1",
            )),
        }
    }
}
