use serde::{Deserialize, Serialize};

#[derive(PartialEq, Debug, Copy, Clone, Serialize, Deserialize, Default)]
pub struct DiveStep {
    pub depth: u32,
    pub time: u32,
}

#[cfg(test)]
mod dive_step_should {
    #[test]
    #[ignore = "not implemented"]
    fn update() {}
}
