use serde::{Deserialize, Serialize};

#[derive(Debug, PartialEq, Copy, Clone, Default, Serialize, Deserialize)]
pub struct DiveStep {
    pub depth: u32,
    pub time: u32,
}
