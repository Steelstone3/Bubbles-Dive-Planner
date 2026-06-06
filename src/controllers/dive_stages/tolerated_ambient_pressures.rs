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

    for compartment in 0..dive_model.number_of_compartments {
        a_values.push(calculate_a_values(compartment, &dive_model));
        b_values.push(calculate_b_values(compartment, &dive_model));
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
    (dive_model.a_values_nitrogen[compartment]
        * dive_model
            .dive_profile
            .tissue_pressure
            .get_nitrogen_tissue_pressures()[compartment]
        + dive_model.a_values_helium[compartment]
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
    (dive_model.b_values_nitrogen[compartment]
        * dive_model
            .dive_profile
            .tissue_pressure
            .get_nitrogen_tissue_pressures()[compartment]
        + dive_model.b_values_helium[compartment]
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
        test::test_fixture::{
            ambient_pressure_test_fixture, dive_stage_test_fixture, tissue_pressure_test_fixture,
        },
    };

    #[test]
    fn test_calculate_tolerated_ambient_pressures() {
        // Given
        let expected_dive_model = dive_stage_test_fixture().dive_model;
        let mut dive_model = DiveModel::create_zhl16_dive_model();
        dive_model.dive_profile = DiveProfile {
            ambient_pressure: ambient_pressure_test_fixture(),
            tissue_pressure: tissue_pressure_test_fixture(),
            ..Default::default()
        };

        // When
        let tolerated_ambient_pressure = calculate_tolerated_ambient_pressures(&dive_model);

        // Then
        pretty_assertions::assert_eq!(
            expected_dive_model.dive_profile.tolerated_ambient_pressure,
            tolerated_ambient_pressure
        );
    }

    // #[test]
    // fn calculate_tolerated_ambient_pressures_of_the_dive_profile() {
    //     // Given
    //     let dive_profile_model = DiveProfile {
    //         ambient_pressure: ambient_pressure_test_fixture(),
    //         tissue_pressure: tissue_pressure_test_fixture(),
    //         tolerated_ambient_pressure: ToleratedAmbientPressure::new(
    //             Default::default(),
    //             tolerated_ambient_pressure_test_fixture().get_a_values(),
    //             tolerated_ambient_pressure_test_fixture().get_b_values(),
    //         ),
    //         ..Default::default()
    //     };
    //     let expected_dive_profile_model = dive_profile_test_fixture();
    //     let mut tolerated_ambient_pressures = vec![];

    //     // When
    //     for compartment in 0..16 {
    //         tolerated_ambient_pressures.push(format!(
    //             "{:.3}",
    //             super::calculate_tolerated_ambient_pressure(compartment, &dive_profile_model)
    //         ));
    //     }

    //     // Then
    //     assert_eq!(
    //         format!(
    //             "{:.3}",
    //             expected_dive_profile_model
    //                 .tolerated_ambient_pressure
    //                 .get_tolerated_ambient_pressures()[0]
    //         ),
    //         tolerated_ambient_pressures[0]
    //     );
    //     assert_eq!(
    //         format!(
    //             "{:.3}",
    //             expected_dive_profile_model
    //                 .tolerated_ambient_pressure
    //                 .get_tolerated_ambient_pressures()[1]
    //         ),
    //         tolerated_ambient_pressures[1]
    //     );
    //     assert_eq!(
    //         format!(
    //             "{:.3}",
    //             expected_dive_profile_model
    //                 .tolerated_ambient_pressure
    //                 .get_tolerated_ambient_pressures()[2]
    //         ),
    //         tolerated_ambient_pressures[2]
    //     );
    //     assert_eq!(
    //         format!(
    //             "{:.3}",
    //             expected_dive_profile_model
    //                 .tolerated_ambient_pressure
    //                 .get_tolerated_ambient_pressures()[3]
    //         ),
    //         tolerated_ambient_pressures[3]
    //     );
    //     assert_eq!(
    //         format!(
    //             "{:.3}",
    //             expected_dive_profile_model
    //                 .tolerated_ambient_pressure
    //                 .get_tolerated_ambient_pressures()[4]
    //         ),
    //         tolerated_ambient_pressures[4]
    //     );
    //     assert_eq!(
    //         format!(
    //             "{:.3}",
    //             expected_dive_profile_model
    //                 .tolerated_ambient_pressure
    //                 .get_tolerated_ambient_pressures()[5]
    //         ),
    //         tolerated_ambient_pressures[5]
    //     );
    //     assert_eq!(
    //         format!(
    //             "{:.3}",
    //             expected_dive_profile_model
    //                 .tolerated_ambient_pressure
    //                 .get_tolerated_ambient_pressures()[6]
    //         ),
    //         tolerated_ambient_pressures[6]
    //     );
    //     assert_eq!(
    //         format!(
    //             "{:.3}",
    //             expected_dive_profile_model
    //                 .tolerated_ambient_pressure
    //                 .get_tolerated_ambient_pressures()[7]
    //         ),
    //         tolerated_ambient_pressures[7]
    //     );
    //     assert_eq!(
    //         format!(
    //             "{:.3}",
    //             expected_dive_profile_model
    //                 .tolerated_ambient_pressure
    //                 .get_tolerated_ambient_pressures()[8]
    //         ),
    //         tolerated_ambient_pressures[8]
    //     );
    //     assert_eq!(
    //         format!(
    //             "{:.3}",
    //             expected_dive_profile_model
    //                 .tolerated_ambient_pressure
    //                 .get_tolerated_ambient_pressures()[9]
    //         ),
    //         tolerated_ambient_pressures[9]
    //     );
    //     assert_eq!(
    //         format!(
    //             "{:.3}",
    //             expected_dive_profile_model
    //                 .tolerated_ambient_pressure
    //                 .get_tolerated_ambient_pressures()[10]
    //         ),
    //         tolerated_ambient_pressures[10]
    //     );
    //     assert_eq!(
    //         format!(
    //             "{:.3}",
    //             expected_dive_profile_model
    //                 .tolerated_ambient_pressure
    //                 .get_tolerated_ambient_pressures()[11]
    //         ),
    //         tolerated_ambient_pressures[11]
    //     );
    //     assert_eq!(
    //         format!(
    //             "{:.3}",
    //             expected_dive_profile_model
    //                 .tolerated_ambient_pressure
    //                 .get_tolerated_ambient_pressures()[12]
    //         ),
    //         tolerated_ambient_pressures[12]
    //     );
    //     assert_eq!(
    //         format!(
    //             "{:.3}",
    //             expected_dive_profile_model
    //                 .tolerated_ambient_pressure
    //                 .get_tolerated_ambient_pressures()[13]
    //         ),
    //         tolerated_ambient_pressures[13]
    //     );
    //     assert_eq!(
    //         format!(
    //             "{:.3}",
    //             expected_dive_profile_model
    //                 .tolerated_ambient_pressure
    //                 .get_tolerated_ambient_pressures()[14]
    //         ),
    //         tolerated_ambient_pressures[14]
    //     );
    //     assert_eq!(
    //         format!(
    //             "{:.3}",
    //             expected_dive_profile_model
    //                 .tolerated_ambient_pressure
    //                 .get_tolerated_ambient_pressures()[15]
    //         ),
    //         tolerated_ambient_pressures[15]
    //     );
    // }
}
