use serde::{Deserialize, Serialize};

#[derive(PartialEq, Debug, Default, Clone, Serialize, Deserialize)]
pub struct ToleratedAmbientPressure {
    tolerated_ambient_pressures: Vec<f32>,
    a_values: Vec<f32>,
    b_values: Vec<f32>,
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
        let default_compartments: Vec<f32> =
            std::iter::repeat_n(0.0, number_of_compartments).collect();

        ToleratedAmbientPressure::new(
            default_compartments.clone(),
            default_compartments.clone(),
            default_compartments.clone(),
        )
    }

    pub fn get_a_values(&self) -> Vec<f32> {
        self.a_values.clone()
    }

    pub fn get_b_values(&self) -> Vec<f32> {
        self.b_values.clone()
    }

    pub fn get_tolerated_ambient_pressures(&self) -> Vec<f32> {
        self.tolerated_ambient_pressures.clone()
    }
}

#[cfg(test)]
mod tolerated_ambient_pressures_should {
    use crate::models::plan::dive_profile_result::tolerated_ambient_pressure::ToleratedAmbientPressure;

    #[test]
    fn test_get_tolerated_ambient_pressures() {
        // Given
        let expected_tolerated_ambient_pressures = vec![23.0, 22.0, 21.0];
        let tolerated_ambient_pressure = ToleratedAmbientPressure::new(
            expected_tolerated_ambient_pressures.clone(),
            vec![],
            vec![],
        );

        // When
        let tolerated_ambient_pressures =
            tolerated_ambient_pressure.get_tolerated_ambient_pressures();

        // Then
        pretty_assertions::assert_eq!(
            expected_tolerated_ambient_pressures.clone(),
            tolerated_ambient_pressures
        );
    }

    #[test]
    fn test_get_a_values() {
        // Given
        let expected_a_values = vec![23.0, 22.0, 21.0];
        let tolerated_ambient_pressure =
            ToleratedAmbientPressure::new(vec![], expected_a_values.clone(), vec![]);

        // When
        let a_values = tolerated_ambient_pressure.get_a_values();

        // Then
        pretty_assertions::assert_eq!(expected_a_values.clone(), a_values);
    }

    #[test]
    fn test_get_b_values() {
        // Given
        let expected_b_values = vec![23.0, 22.0, 21.0];
        let tolerated_ambient_pressure =
            ToleratedAmbientPressure::new(vec![], vec![], expected_b_values.clone());

        // When
        let b_values = tolerated_ambient_pressure.get_b_values();

        // Then
        pretty_assertions::assert_eq!(expected_b_values.clone(), b_values);
    }
}
