use serde::{Deserialize, Serialize};

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
    #[allow(dead_code)]
    fn default() -> Self {
        Self {
            maximum_surface_pressures: Default::default(),
            compartment_loads: Default::default(),
            nitrogen_tissue_pressures: Default::default(),
            helium_tissue_pressures: Default::default(),
            total_tissue_pressures: Default::default(),
            tolerated_ambient_pressures: Default::default(),
            a_values: Default::default(),
            b_values: Default::default(),
            oxygen_at_pressure: Default::default(),
            helium_at_pressure: Default::default(),
            nitrogen_at_pressure: Default::default(),
        }
    }
}
