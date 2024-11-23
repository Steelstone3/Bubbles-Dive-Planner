use crate::models::dive_step::DiveStep;
use serde::{Deserialize, Serialize};

#[derive(Debug, Default, Clone, PartialEq, Serialize, Deserialize)]
pub struct DecompressionSteps {
    pub dive_steps: Vec<DiveStep>,
}

impl DecompressionSteps {
    pub fn assign_decompression_steps(&mut self, decompression_dive_steps: Vec<DiveStep>) {
        self.dive_steps = decompression_dive_steps
    }
}

#[cfg(test)]
mod decompression_steps_should {
    use super::*;

    #[test]
    fn assign_decompression_steps() {
        // Given
        let mut decompression_steps = DecompressionSteps { dive_steps: vec![] };

        // When
        decompression_steps.assign_decompression_steps(vec![DiveStep {
            depth: 50,
            time: 10,
        }]);

        // Then
        assert_eq!(
            vec![DiveStep {
                depth: 50,
                time: 10,
            }],
            decompression_steps.dive_steps
        );
    }
}
