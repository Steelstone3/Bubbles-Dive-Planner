use serde::{Deserialize, Serialize};

#[derive(PartialEq, Debug, Copy, Clone, Serialize, Deserialize, Default)]
pub struct DiveStep {
    pub depth: u32,
    pub time: u32,
}

impl DiveStep {
    pub fn validate(input: u32, maximum: u32) -> u32 {
        if input > maximum {
            return maximum;
        }

        input
    }
}

#[cfg(test)]
mod dive_step_should {
    use super::*;

    #[test]
    fn validate_assignment_of_input() {
        // When
        let input = DiveStep::validate(101, 100);

        // Then
        assert_eq!(100, input);
    }
}
