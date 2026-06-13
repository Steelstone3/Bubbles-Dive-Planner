use crate::models::plan::dive_profile_result::{
    dive_profile::DiveProfile, tolerated_surface_pressure::ToleratedSurfacePressure,
};

pub fn calculate_tolerated_surface_pressures(
    dive_profile_model: &DiveProfile,
) -> ToleratedSurfacePressure {
    let mut maximum_surface_pressures = vec![];
    let mut compartment_loads = vec![];

    for compartment in 0..dive_profile_model.number_of_compartments {
        maximum_surface_pressures.push(calculate_maximum_surface_pressures(
            compartment,
            dive_profile_model,
        ));
        compartment_loads.push(calculate_compartment_loads(
            compartment,
            dive_profile_model,
            maximum_surface_pressures.clone(),
        ));
    }

    let dive_ceiling = calculate_dive_ceiling(dive_profile_model);

    ToleratedSurfacePressure::new(
        maximum_surface_pressures.clone(),
        compartment_loads,
        dive_ceiling,
    )
}

fn calculate_maximum_surface_pressures(
    compartment: usize,
    dive_profile_model: &DiveProfile,
) -> f32 {
    (1.0 / dive_profile_model.tolerated_ambient_pressure.get_b_values()[compartment])
        + dive_profile_model.tolerated_ambient_pressure.get_a_values()[compartment]
}

fn calculate_compartment_loads(
    compartment: usize,
    dive_profile_model: &DiveProfile,
    maximum_surface_pressures: Vec<f32>,
) -> f32 {
    dive_profile_model
        .tissue_pressure
        .get_total_tissue_pressures()[compartment]
        / maximum_surface_pressures[compartment]
        * 100.0
}

fn calculate_dive_ceiling(dive_profile_model: &DiveProfile) -> f32 {
    (dive_profile_model
        .tolerated_ambient_pressure
        .get_tolerated_ambient_pressures()
        .iter()
        .cloned()
        .fold(f32::NEG_INFINITY, f32::max)
        - 1.0)
        * 10.0
}

#[cfg(test)]
mod commands_max_surface_pressures_should {
    use super::*;
    use crate::test_fixture::{
        dive_profile_test_fixture, tissue_pressure_test_fixture,
        tolerated_ambient_pressure_test_fixture,
    };

    #[test]
    fn test_calculate_tolerated_surface_pressures() {
        // given
        let expected_dive_profile = dive_profile_test_fixture();
        let dive_profile = DiveProfile {
            tissue_pressure: tissue_pressure_test_fixture(),
            tolerated_ambient_pressure: tolerated_ambient_pressure_test_fixture(),
            number_of_compartments: 16,
            ..Default::default()
        };

        // when
        let tolerated_surface_pressure = calculate_tolerated_surface_pressures(&dive_profile);

        // then
        pretty_assertions::assert_eq!(
            expected_dive_profile.tolerated_surface_pressure,
            tolerated_surface_pressure
        )
    }
}
