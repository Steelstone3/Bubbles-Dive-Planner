use serde::{Deserialize, Serialize};

pub const CNS_COLUMNS: usize = 11;

#[derive(Debug, Clone, PartialEq, Serialize, Deserialize)]
pub struct CentralNervousSystemToxicity {
    pub oxygen_partial_pressure: [f32; CNS_COLUMNS],
    pub maximum_single_dive_duration: [u32; CNS_COLUMNS],
    pub maximum_total_dive_duration: [u32; CNS_COLUMNS],
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
