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

    pub fn new_default(number_of_compartments: usize) -> TissuePressure {
        let nitrogen_compartments: Vec<f32> =
            std::iter::repeat_n(0.79, number_of_compartments).collect();

        let default_compartments: Vec<f32> =
            std::iter::repeat_n(0.0, number_of_compartments).collect();

        TissuePressure::new(
            nitrogen_compartments.clone(),
            default_compartments,
            nitrogen_compartments.clone(),
        )
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

#[cfg(test)]
mod tissue_pressure_should {
    use crate::models::plan::dive_profile_result::tissue_pressure::TissuePressure;

    #[test]
    fn test_get_nitrogen_tissue_pressures() {
        // given
        let expected_tissue_pressures = vec![12.0, 10.2];
        let tissue_pressure =
            TissuePressure::new(expected_tissue_pressures.clone(), vec![], vec![]);

        // when
        let nitrogen_tissue_pressure = tissue_pressure.get_nitrogen_tissue_pressures();

        // then
        pretty_assertions::assert_eq!(expected_tissue_pressures.clone(), nitrogen_tissue_pressure);
    }

    #[test]
    fn test_get_helium_tissue_pressures() {
        // given
        let expected_tissue_pressures = vec![12.0, 10.2];
        let tissue_pressure =
            TissuePressure::new(vec![], expected_tissue_pressures.clone(), vec![]);

        // when
        let helium_tissue_pressure = tissue_pressure.get_helium_tissue_pressures();

        // then
        pretty_assertions::assert_eq!(expected_tissue_pressures.clone(), helium_tissue_pressure);
    }

    #[test]
    fn test_get_total_tissue_pressures() {
        // given
        let expected_tissue_pressures = vec![12.0, 10.2];
        let tissue_pressure =
            TissuePressure::new(vec![], vec![], expected_tissue_pressures.clone());

        // when
        let total_tissue_pressure = tissue_pressure.get_total_tissue_pressures();

        // then
        pretty_assertions::assert_eq!(expected_tissue_pressures.clone(), total_tissue_pressure);
    }
}
