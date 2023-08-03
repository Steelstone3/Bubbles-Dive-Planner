use serde::{Deserialize, Serialize};
use std::fmt::Display;

#[derive(PartialEq, Debug, Default, Copy, Clone, Serialize, Deserialize)]
pub struct DiveProfile {
    pub maximum_surface_pressures: [f32; 16],
    pub compartment_loads: [f32; 16],
    pub tissue_pressures_nitrogen: [f32; 16],
    pub tissue_pressures_helium: [f32; 16],
    pub tissue_pressures_total: [f32; 16],
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
            tissue_pressures_nitrogen: Default::default(),
            tissue_pressures_helium: Default::default(),
            tissue_pressures_total: Default::default(),
            tolerated_ambient_pressures: Default::default(),
            a_values: Default::default(),
            b_values: Default::default(),
            oxygen_at_pressure: Default::default(),
            helium_at_pressure: Default::default(),
            nitrogen_at_pressure: Default::default(),
        }
    }

    fn display_results(self) -> String {
        println!();
        let mut dive_results = "".to_string();

        for (_, compartment) in (0..self.compartment_loads.len()).enumerate() {
            let dive_result = format!(
                "\nC: {} | TPt: {} | TAP: {} | MSP: {} | CLp: {}",
                compartment + 1,
                self.tissue_pressures_total[compartment],
                self.tolerated_ambient_pressures[compartment],
                self.maximum_surface_pressures[compartment],
                self.compartment_loads[compartment]
            );

            dive_results.push_str(&dive_result);
        }

        dive_results
    }
}

impl Display for DiveProfile {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        write!(f, "{}", self.display_results())
    }
}

#[cfg(test)]
mod gas_management_should {
    use super::*;

    #[test]
    fn has_default() {
        let dive_profile = DiveProfile::default();
        let maximum_surface_pressures: [f32; 16] = default_compartment_value_test_fixture();
        let compartment_loads: [f32; 16] = default_compartment_value_test_fixture();
        let tissue_pressures_nitrogen: [f32; 16] = default_compartment_value_test_fixture();
        let tissue_pressures_helium: [f32; 16] = default_compartment_value_test_fixture();
        let tissue_pressures_total: [f32; 16] = default_compartment_value_test_fixture();
        let tolerated_ambient_pressures: [f32; 16] = default_compartment_value_test_fixture();
        let a_values: [f32; 16] = default_compartment_value_test_fixture();
        let b_values: [f32; 16] = default_compartment_value_test_fixture();
        let oxygen_at_pressure: f32 = 0.0;
        let helium_at_pressure: f32 = 0.0;
        let nitrogen_at_pressure: f32 = 0.0;

        assert_eq!(
            maximum_surface_pressures,
            dive_profile.maximum_surface_pressures
        );
        assert_eq!(compartment_loads, dive_profile.compartment_loads);
        assert_eq!(
            tissue_pressures_nitrogen,
            dive_profile.tissue_pressures_nitrogen
        );
        assert_eq!(
            tissue_pressures_helium,
            dive_profile.tissue_pressures_helium
        );
        assert_eq!(tissue_pressures_total, dive_profile.tissue_pressures_total);
        assert_eq!(
            tolerated_ambient_pressures,
            dive_profile.tolerated_ambient_pressures
        );
        assert_eq!(a_values, dive_profile.a_values);
        assert_eq!(b_values, dive_profile.b_values);
        assert_eq!(oxygen_at_pressure, dive_profile.oxygen_at_pressure);
        assert_eq!(helium_at_pressure, dive_profile.helium_at_pressure);
        assert_eq!(nitrogen_at_pressure, dive_profile.nitrogen_at_pressure);
    }

    #[test]
    fn has_maximum_surface_pressures() {
        let mut dive_profile = DiveProfile::default();

        dive_profile.maximum_surface_pressures = compartment_value_test_fixture();

        assert_eq!(
            compartment_value_test_fixture(),
            dive_profile.maximum_surface_pressures
        )
    }

    #[test]
    fn has_compartment_loads() {
        let mut dive_profile = DiveProfile::default();

        dive_profile.compartment_loads = compartment_value_test_fixture();

        assert_eq!(
            compartment_value_test_fixture(),
            dive_profile.compartment_loads
        )
    }

    #[test]
    fn has_tissue_pressures_nitrogen() {
        let mut dive_profile = DiveProfile::default();

        dive_profile.tissue_pressures_nitrogen = compartment_value_test_fixture();

        assert_eq!(
            compartment_value_test_fixture(),
            dive_profile.tissue_pressures_nitrogen
        )
    }

    #[test]
    fn has_tissue_pressures_helium() {
        let mut dive_profile = DiveProfile::default();

        dive_profile.tissue_pressures_helium = compartment_value_test_fixture();

        assert_eq!(
            compartment_value_test_fixture(),
            dive_profile.tissue_pressures_helium
        )
    }

    #[test]
    fn has_tissue_pressures_total() {
        let mut dive_profile = DiveProfile::default();

        dive_profile.tissue_pressures_total = compartment_value_test_fixture();

        assert_eq!(
            compartment_value_test_fixture(),
            dive_profile.tissue_pressures_total
        )
    }

    #[test]
    fn has_tolerated_ambient_pressures() {
        let mut dive_profile = DiveProfile::default();

        dive_profile.tolerated_ambient_pressures = compartment_value_test_fixture();

        assert_eq!(
            compartment_value_test_fixture(),
            dive_profile.tolerated_ambient_pressures
        )
    }

    #[test]
    fn has_a_values() {
        let mut dive_profile = DiveProfile::default();

        dive_profile.a_values = compartment_value_test_fixture();

        assert_eq!(compartment_value_test_fixture(), dive_profile.a_values)
    }

    #[test]
    fn has_b_values() {
        let mut dive_profile = DiveProfile::default();

        dive_profile.b_values = compartment_value_test_fixture();

        assert_eq!(compartment_value_test_fixture(), dive_profile.b_values)
    }

    #[test]
    fn has_oxygen_at_pressure() {
        let mut dive_profile = DiveProfile::default();
        let value: f32 = 4.0;

        dive_profile.oxygen_at_pressure = value;

        assert_eq!(value, dive_profile.oxygen_at_pressure)
    }

    #[test]
    fn has_helium_at_pressure() {
        let mut dive_profile = DiveProfile::default();
        let value: f32 = 4.0;

        dive_profile.helium_at_pressure = value;

        assert_eq!(value, dive_profile.helium_at_pressure)
    }

    #[test]
    fn have_nitrogen_at_pressure() {
        let mut dive_profile = DiveProfile::default();
        let value: f32 = 4.0;

        dive_profile.nitrogen_at_pressure = value;

        assert_eq!(value, dive_profile.nitrogen_at_pressure)
    }

    fn default_compartment_value_test_fixture() -> [f32; 16] {
        [
            0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0,
        ]
    }

    fn compartment_value_test_fixture() -> [f32; 16] {
        [
            4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0,
        ]
    }
}
