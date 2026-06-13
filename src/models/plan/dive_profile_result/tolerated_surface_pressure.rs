use serde::{Deserialize, Serialize};

#[derive(PartialEq, Debug, Default, Clone, Serialize, Deserialize)]
pub struct ToleratedSurfacePressure {
    maximum_surface_pressures: Vec<f32>,
    compartment_loads: Vec<f32>,
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
        let default_compartments: Vec<f32> =
            std::iter::repeat_n(0.0, number_of_compartments).collect();

        ToleratedSurfacePressure::new(
            default_compartments.clone(),
            default_compartments.clone(),
            0.0,
        )
    }

    pub fn get_maximum_surface_pressures(&self) -> Vec<f32> {
        self.maximum_surface_pressures.clone()
    }

    pub fn get_compartment_loads(&self) -> Vec<f32> {
        self.compartment_loads.clone()
    }

    pub fn get_dive_ceiling(&self) -> f32 {
        self.dive_ceiling
    }
}

#[cfg(test)]
mod tolerated_surface_pressure_should {
    use crate::models::plan::dive_profile_result::tolerated_surface_pressure::ToleratedSurfacePressure;

    #[test]
    fn test_get_maximum_surface_pressures() {
        // Given
        let expected_maximum_surface_pressures = vec![23.0, 24.0, 25.0];
        let tolerated_surface_pressure = ToleratedSurfacePressure::new(
            expected_maximum_surface_pressures.clone(),
            Default::default(),
            Default::default(),
        );

        // When
        let maximum_surface_pressures = tolerated_surface_pressure.get_maximum_surface_pressures();

        // Then
        pretty_assertions::assert_eq!(
            expected_maximum_surface_pressures.clone(),
            maximum_surface_pressures
        );
    }

    #[test]
    fn test_get_compartment_loads() {
        // Given
        let expected_compartment_loads = vec![23.0, 24.0, 25.0];
        let tolerated_surface_pressure = ToleratedSurfacePressure::new(
            Default::default(),
            expected_compartment_loads.clone(),
            Default::default(),
        );

        // When
        let comparment_loads = tolerated_surface_pressure.get_compartment_loads();

        // Then
        pretty_assertions::assert_eq!(expected_compartment_loads.clone(), comparment_loads);
    }

    #[test]
    fn test_get_dive_ceiling() {
        // Given
        let expected_dive_ceiling = 23.0;
        let tolerated_surface_pressure = ToleratedSurfacePressure::new(
            Default::default(),
            Default::default(),
            expected_dive_ceiling,
        );

        // When
        let dive_ceiling = tolerated_surface_pressure.get_dive_ceiling();

        // Then
        pretty_assertions::assert_eq!(expected_dive_ceiling.clone(), dive_ceiling);
    }
}
