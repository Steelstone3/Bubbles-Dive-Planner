use crate::models::plan::dive_profile_result::{
    ambient_pressure::AmbientPressure, tissue_pressure::TissuePressure,
    tolerated_ambient_pressure::ToleratedAmbientPressure,
    tolerated_surface_pressure::ToleratedSurfacePressure,
};
use serde::{Deserialize, Serialize};

#[derive(PartialEq, Debug, Default, Clone, Serialize, Deserialize)]
pub struct DiveProfile {
    pub number_of_compartments: usize,
    pub ambient_pressure: AmbientPressure,
    pub tissue_pressure: TissuePressure,
    pub tolerated_ambient_pressure: ToleratedAmbientPressure,
    pub tolerated_surface_pressure: ToleratedSurfacePressure,
}

impl DiveProfile {
    pub fn new(number_of_compartments: usize) -> Self {
        Self {
            number_of_compartments,
            tissue_pressure: TissuePressure::new_default(number_of_compartments),
            ambient_pressure: AmbientPressure::default(),
            tolerated_ambient_pressure: ToleratedAmbientPressure::new_default(
                number_of_compartments,
            ),
            tolerated_surface_pressure: ToleratedSurfacePressure::new_default(
                number_of_compartments,
            ),
        }
    }
}

#[cfg(test)]
mod dive_profile_should {}
