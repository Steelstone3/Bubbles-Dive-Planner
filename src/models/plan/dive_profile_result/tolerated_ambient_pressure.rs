use serde::{Deserialize, Serialize};

#[derive(PartialEq, Debug, Default, Clone, Serialize, Deserialize)]
pub struct ToleratedAmbientPressure {
    pub tolerated_ambient_pressures: Vec<f32>,
    pub a_values: Vec<f32>,
    pub b_values: Vec<f32>,
}

impl ToleratedAmbientPressure {
    pub fn new(
        tolerated_ambient_pressures: Vec<f32>,
        a_values: Vec<f32>,
        b_values: Vec<f32>,
    ) -> Self {
        Self {
            tolerated_ambient_pressures,
            a_values,
            b_values,
        }
    }

    pub fn new_default(number_of_compartments: usize) -> Self {
        let default_compartments: Vec<f32> = std::iter::repeat(0.0)
            .take(number_of_compartments)
            .collect();

        ToleratedAmbientPressure::new(
            default_compartments.clone(),
            default_compartments.clone(),
            default_compartments.clone(),
        )
    }
}
