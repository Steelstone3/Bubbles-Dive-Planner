use iced::executor::Default;
use serde::{Deserialize, Serialize};
use std::fmt::Display;

#[derive(PartialEq, Debug, Default, Copy, Clone, Serialize, Deserialize)]
pub struct DiveProfile {
    pub maximum_surface_pressures: [f32; 16],
    pub compartment_loads: [f32; 16],
    pub nitrogen_tissue_pressures: [f32; 16],
    pub helium_tissue_pressures: [f32; 16],
    pub total_tissue_pressures: [f32; 16],
    pub tolerated_ambient_pressures: [f32; 16],
    pub a_values: [f32; 16],
    pub b_values: [f32; 16],
    pub oxygen_at_pressure: f32,
    pub helium_at_pressure: f32,
    pub nitrogen_at_pressure: f32,
}

impl DiveProfile {
    fn display_results(self) -> String {
        println!();
        let mut dive_results = "".to_string();

        for (_, compartment) in (0..self.compartment_loads.len()).enumerate() {
            let dive_result = format!(
                "\nC: {} | TPt: {} | TAP: {} | MSP: {} | CLp: {}",
                compartment + 1,
                self.total_tissue_pressures[compartment],
                self.tolerated_ambient_pressures[compartment],
                self.maximum_surface_pressures[compartment],
                self.compartment_loads[compartment]
            );

            dive_results.push_str(&dive_result);
        }

        dive_results
    }
}

impl Display for DiveProfile {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        write!(f, "{}", self.display_results())
    }
}
