use serde::{Deserialize, Serialize};

#[derive(PartialEq, Debug, Default, Clone, Serialize, Deserialize)]
pub struct TissuePressure {
    nitrogen_tissue_pressures: Vec<f32>,
    helium_tissue_pressures: Vec<f32>,
    total_tissue_pressures: Vec<f32>,
}

impl TissuePressure {
    pub fn new(
        nitrogen_tissue_pressures: Vec<f32>,
        helium_tissue_pressures: Vec<f32>,
        total_tissue_pressures: Vec<f32>,
    ) -> Self {
        Self {
            nitrogen_tissue_pressures,
            helium_tissue_pressures,
            total_tissue_pressures,
        }
    }

    pub fn get_nitrogen_tissue_pressures(&self) -> Vec<f32> {
        self.nitrogen_tissue_pressures.clone()
    }

    pub fn get_helium_tissue_pressures(&self) -> Vec<f32> {
        self.helium_tissue_pressures.clone()
    }

    pub fn get_total_tissue_pressures(&self) -> Vec<f32> {
        self.total_tissue_pressures.clone()
    }
}
