use serde::{Deserialize, Serialize};

#[derive(PartialEq, Debug, Copy, Clone, Serialize, Deserialize, Default)]
pub struct DiveStep {
    pub depth: u32,
    pub time: u32,
}