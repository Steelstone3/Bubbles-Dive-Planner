use crate::models::plan::{
    dive_model::DiveModel,
    dive_profile_result::{
        dive_profile::DiveProfile, tolerated_ambient_pressure::ToleratedAmbientPressure,
    },
};

pub fn calculate_tolerated_ambient_pressures(dive_model: &DiveModel) -> ToleratedAmbientPressure {
    let mut a_values = vec![];
    let mut b_values = vec![];
    let mut tolerated_ambient_pressures = vec![];

    for compartment in 0..dive_model.get_number_of_compartments() {
        a_values.push(calculate_a_values(compartment, dive_model));
        b_values.push(calculate_b_values(compartment, dive_model));
        tolerated_ambient_pressures.push(calculate_tolerated_ambient_pressure(
            compartment,
            &dive_model.dive_profile,
            a_values.clone(),
            b_values.clone(),
        ));
    }

    ToleratedAmbientPressure::new(
        tolerated_ambient_pressures,
        a_values.clone(),
        b_values.clone(),
    )
}

fn calculate_a_values(compartment: usize, dive_model: &DiveModel) -> f32 {
    (dive_model.get_a_values_nitrogen()[compartment]
        * dive_model
            .dive_profile
            .tissue_pressure
            .get_nitrogen_tissue_pressures()[compartment]
        + dive_model.get_a_values_helium()[compartment]
            * dive_model
                .dive_profile
                .tissue_pressure
                .get_helium_tissue_pressures()[compartment])
        / dive_model
            .dive_profile
            .tissue_pressure
            .get_total_tissue_pressures()[compartment]
}

fn calculate_b_values(compartment: usize, dive_model: &DiveModel) -> f32 {
    (dive_model.get_b_values_nitrogen()[compartment]
        * dive_model
            .dive_profile
            .tissue_pressure
            .get_nitrogen_tissue_pressures()[compartment]
        + dive_model.get_b_values_helium()[compartment]
            * dive_model
                .dive_profile
                .tissue_pressure
                .get_helium_tissue_pressures()[compartment])
        / dive_model
            .dive_profile
            .tissue_pressure
            .get_total_tissue_pressures()[compartment]
}

fn calculate_tolerated_ambient_pressure(
    compartment: usize,
    dive_profile_model: &DiveProfile,
    a_values: Vec<f32>,
    b_values: Vec<f32>,
) -> f32 {
    (dive_profile_model
        .tissue_pressure
        .get_total_tissue_pressures()[compartment]
        - a_values[compartment])
        * b_values[compartment]
}

#[cfg(test)]
mod commands_tolerated_ambient_pressures_should {
    use crate::{
        controllers::dive_stages::tolerated_ambient_pressures::calculate_tolerated_ambient_pressures,
        models::plan::{dive_model::DiveModel, dive_profile_result::dive_profile::DiveProfile},
        test_fixture::{
            usn_revision_6_ambient_pressure_test_fixture, usn_revision_6_dive_stage_test_fixture,
            usn_revision_6_tissue_pressure_test_fixture, zhl16_ambient_pressure_test_fixture,
            zhl16_dive_stage_test_fixture, zhl16_tissue_pressure_test_fixture,
        },
    };

    #[test]
    fn test_zhl16_calculate_tolerated_ambient_pressures() {
        // given
        let expected_dive_model = zhl16_dive_stage_test_fixture().dive_model;
        let dive_profile = DiveProfile {
            ambient_pressure: zhl16_ambient_pressure_test_fixture(),
            tissue_pressure: zhl16_tissue_pressure_test_fixture(),
            ..Default::default()
        };
        let dive_model = DiveModel::new_zhl16_dive_model_with_dive_profile(dive_profile);

        // when
        let tolerated_ambient_pressure = calculate_tolerated_ambient_pressures(&dive_model);

        // then
        pretty_assertions::assert_eq!(
            expected_dive_model.dive_profile.tolerated_ambient_pressure,
            tolerated_ambient_pressure
        );
    }

    #[test]
    fn test_usn_revision_6_calculate_tolerated_ambient_pressures() {
        // given
        let expected_dive_model = usn_revision_6_dive_stage_test_fixture().dive_model;
        let dive_profile = DiveProfile {
            ambient_pressure: usn_revision_6_ambient_pressure_test_fixture(),
            tissue_pressure: usn_revision_6_tissue_pressure_test_fixture(),
            ..Default::default()
        };
        let dive_model = DiveModel::new_usn_revision_6_dive_model_with_dive_profile(dive_profile);

        // when
        let tolerated_ambient_pressure = calculate_tolerated_ambient_pressures(&dive_model);

        // then
        pretty_assertions::assert_eq!(
            expected_dive_model.dive_profile.tolerated_ambient_pressure,
            tolerated_ambient_pressure
        );
    }
}
