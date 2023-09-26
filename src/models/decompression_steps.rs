use crate::models::dive_step::DiveStep;
use serde::{Deserialize, Serialize};

// TODO encapsulate
#[derive(Debug, Default, Clone, PartialEq, Serialize, Deserialize)]
pub struct DecompressionSteps {
    pub is_visible: bool,
    pub dive_steps: Vec<DiveStep>,
}

impl DecompressionSteps {
    pub fn update_visibility(&mut self) {
        self.is_visible = !self.dive_steps.is_empty();
    }

    // TODO test
    pub fn assign_decompression_steps(&mut self, decompression_dive_steps: Vec<DiveStep>) {
        self.dive_steps = decompression_dive_steps
    }
}

#[cfg(test)]
mod decompression_steps_should {
    use super::*;

    #[test]
    fn update_visibility_is_visible() {
        // Given
        let mut decompression_steps = DecompressionSteps {
            is_visible: false,
            dive_steps: vec![DiveStep {
                depth: 50,
                time: 10,
            }],
        };

        // When
        decompression_steps.update_visibility();

        // Then
        assert!(decompression_steps.is_visible);
    }

    #[test]
    fn update_visibility_is_not_visible() {
        // Given
        let mut decompression_steps = DecompressionSteps {
            is_visible: true,
            dive_steps: vec![],
        };

        // When
        decompression_steps.update_visibility();

        // Then
        assert!(!decompression_steps.is_visible);
    }
}
