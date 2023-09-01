use std::fmt::Display;

use serde::{Deserialize, Serialize};

const COLUMNS: usize = 11;

#[derive(Debug, Clone, PartialEq, Serialize, Deserialize)]
pub struct CentralNervousSystemToxicity {
    pub oxygen_partial_pressure: [f32; COLUMNS],
    pub maximum_single_dive_duration: [u32; COLUMNS],
    pub maximum_total_dive_duration: [u32; COLUMNS],
}

impl Default for CentralNervousSystemToxicity {
    fn default() -> Self {
        Self {
            oxygen_partial_pressure: [1.6, 1.5, 1.4, 1.3, 1.2, 1.1, 1.0, 0.9, 0.8, 0.7, 0.6],
            maximum_single_dive_duration: [45, 120, 150, 180, 210, 240, 300, 360, 450, 570, 720],
            maximum_total_dive_duration: [150, 180, 180, 210, 240, 270, 300, 360, 450, 570, 720],
        }
    }
}

impl CentralNervousSystemToxicity {
    fn display_cns_toxicity(&self) -> String {
        let mut central_nervous_system = "".to_string();

        for (_, compartment) in (0..COLUMNS).enumerate() {
            let dive_result = format!(
                "O2 PP (%): {} | Per Dive (min): {} | Per Day (min): {}\n",
                self.oxygen_partial_pressure[compartment],
                self.maximum_single_dive_duration[compartment],
                self.maximum_total_dive_duration[compartment]
            );

            central_nervous_system.push_str(&dive_result);
        }

        central_nervous_system
    }
}

impl Display for CentralNervousSystemToxicity {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        write!(f, "{}", self.display_cns_toxicity())
    }
}

#[cfg(test)]
mod central_nervous_system_toxicity_should {
    use super::*;

    #[test]
    fn display_central_nervous_system_toxicity() {
        // Given
        let expected_display = "O2 PP (%): 2 | Per Dive (min): 2 | Per Day (min): 2\nO2 PP (%): 2 | Per Dive (min): 2 | Per Day (min): 2\nO2 PP (%): 2 | Per Dive (min): 2 | Per Day (min): 2\nO2 PP (%): 2 | Per Dive (min): 2 | Per Day (min): 2\nO2 PP (%): 2 | Per Dive (min): 2 | Per Day (min): 2\nO2 PP (%): 2 | Per Dive (min): 2 | Per Day (min): 2\nO2 PP (%): 2 | Per Dive (min): 2 | Per Day (min): 2\nO2 PP (%): 2 | Per Dive (min): 2 | Per Day (min): 2\nO2 PP (%): 2 | Per Dive (min): 2 | Per Day (min): 2\nO2 PP (%): 2 | Per Dive (min): 2 | Per Day (min): 2\nO2 PP (%): 2 | Per Dive (min): 2 | Per Day (min): 2\n";
        let cns_toxicity = central_nervous_system_toxicity_test_fixture();

        // When
        let display = cns_toxicity.display_cns_toxicity();

        // Then
        assert_eq!(expected_display, display);
    }

    fn central_nervous_system_toxicity_test_fixture() -> CentralNervousSystemToxicity {
        let default_array = [2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0];
        let default_array_2 = [2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2];

        CentralNervousSystemToxicity {
            oxygen_partial_pressure: default_array,
            maximum_single_dive_duration: default_array_2,
            maximum_total_dive_duration: default_array_2,
        }
    }
}
