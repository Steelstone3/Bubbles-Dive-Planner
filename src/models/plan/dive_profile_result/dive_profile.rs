use serde::{Deserialize, Serialize};

use crate::models::plan::dive_profile_result::{
    ambient_pressure::AmbientPressure, tissue_pressure::TissuePressure,
};

#[derive(PartialEq, Debug, Default, Clone, Serialize, Deserialize)]
pub struct DiveProfile {
    pub number_of_compartments: usize,
    pub maximum_surface_pressures: Vec<f32>,
    pub compartment_loads: Vec<f32>,
    pub tissue_pressure: TissuePressure,
    pub tolerated_ambient_pressures: Vec<f32>,
    pub a_values: Vec<f32>,
    pub b_values: Vec<f32>,
    pub ambient_pressure: AmbientPressure,
    pub dive_ceiling: f32,
}

#[derive(PartialEq, Debug, Default, Clone, Serialize, Deserialize)]
pub struct ToleratedAmbientPressure {}

#[derive(PartialEq, Debug, Default, Clone, Serialize, Deserialize)]
pub struct ToleratedSurfacePressure {}

impl DiveProfile {
    pub fn new(number_of_compartments: usize) -> Self {
        let nitrogen_tissue_pressures: Vec<f32> = std::iter::repeat(0.79)
            .take(number_of_compartments)
            .collect();
        let helium_tissue_pressures: Vec<f32> = std::iter::repeat(0.0)
            .take(number_of_compartments)
            .collect();

        Self {
            number_of_compartments,
            tissue_pressure: TissuePressure::new(
                nitrogen_tissue_pressures.clone(),
                helium_tissue_pressures,
                nitrogen_tissue_pressures.clone(),
            ),
            ..Default::default()
        }
    }
}

#[cfg(test)]
mod dive_profile_should {}
