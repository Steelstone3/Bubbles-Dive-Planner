use serde::{Deserialize, Serialize};

#[derive(PartialEq, Debug, Default, Clone, Serialize, Deserialize)]
pub struct ToleratedSurfacePressure {
    pub maximum_surface_pressures: Vec<f32>,
    pub compartment_loads: Vec<f32>,
    dive_ceiling: f32,
}

impl ToleratedSurfacePressure {
    pub fn new(
        maximum_surface_pressures: Vec<f32>,
        compartment_loads: Vec<f32>,
        dive_ceiling: f32,
    ) -> Self {
        Self {
            maximum_surface_pressures,
            compartment_loads,
            dive_ceiling,
        }
    }

    pub fn new_default(number_of_compartments: usize) -> ToleratedSurfacePressure {
        let default_compartments: Vec<f32> = std::iter::repeat(0.0)
            .take(number_of_compartments)
            .collect();

        ToleratedSurfacePressure::new(
            default_compartments.clone(),
            default_compartments.clone(),
            0.0,
        )
    }

    pub fn get_dive_ceiling(&self) -> f32 {
        self.dive_ceiling
    }
}
