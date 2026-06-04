use serde::{Deserialize, Serialize};

use crate::models::plan::dive_stage::DiveStage;

#[derive(PartialEq, Debug, Default, Clone, Serialize, Deserialize)]
pub struct DiveProfile {
    pub number_of_compartments: usize,
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
    pub dive_ceiling: f32,
}

impl DiveProfile {
    pub fn new(number_of_compartments: usize) -> Self {
        Self {
            number_of_compartments,
            nitrogen_tissue_pressures: [
                0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79,
                0.79, 0.79,
            ],
            total_tissue_pressures: [
                0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79,
                0.79, 0.79,
            ],
            ..Default::default()
        }
    }
}

#[cfg(test)]
mod dive_profile_should {}
