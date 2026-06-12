use crate::models::plan::{
    dive_model::DiveModel, dive_profile_result::tissue_pressure::TissuePressure,
    dive_step::DiveStep,
};

pub fn calculate_tissue_pressures(dive_model: &DiveModel, dive_step: &DiveStep) -> TissuePressure {
    let mut nitrogen_tissue_pressures = vec![];
    let mut helium_tissue_pressures = vec![];
    let mut total_tissue_pressures = vec![];

    for compartment in 0..dive_model.get_number_of_compartments() {
        nitrogen_tissue_pressures.push(calculate_nitrogen_tissue_pressures(
            compartment,
            dive_model,
            dive_step,
        ));
        helium_tissue_pressures.push(calculate_helium_tissue_pressures(
            compartment,
            dive_model,
            dive_step,
        ));
        total_tissue_pressures.push(calculate_total_tissue_pressures(
            compartment,
            nitrogen_tissue_pressures.clone(),
            helium_tissue_pressures.clone(),
        ));
    }

    TissuePressure::new(
        nitrogen_tissue_pressures.clone(),
        helium_tissue_pressures.clone(),
        total_tissue_pressures,
    )
}

fn calculate_nitrogen_tissue_pressures(
    compartment: usize,
    dive_model: &DiveModel,
    dive_step: &DiveStep,
) -> f32 {
    dive_model
        .dive_profile
        .tissue_pressure
        .get_nitrogen_tissue_pressures()[compartment]
        + ((dive_model
            .dive_profile
            .ambient_pressure
            .get_nitrogen_at_pressure()
            - dive_model
                .dive_profile
                .tissue_pressure
                .get_nitrogen_tissue_pressures()[compartment])
            * (1.0
                - f32::powf(
                    2.0,
                    -(dive_step.time as f32 / dive_model.get_nitrogen_half_times()[compartment]),
                )))
}

fn calculate_helium_tissue_pressures(
    compartment: usize,
    dive_model: &DiveModel,
    dive_step: &DiveStep,
) -> f32 {
    dive_model
        .dive_profile
        .tissue_pressure
        .get_helium_tissue_pressures()[compartment]
        + ((dive_model
            .dive_profile
            .ambient_pressure
            .get_helium_at_pressure()
            - dive_model
                .dive_profile
                .tissue_pressure
                .get_helium_tissue_pressures()[compartment])
            * (1.0
                - f32::powf(
                    2.0,
                    -(dive_step.time as f32 / dive_model.get_helium_half_times()[compartment]),
                )))
}

fn calculate_total_tissue_pressures(
    compartment: usize,
    nitrogen_tissue_pressures: Vec<f32>,
    helium_tissue_pressures: Vec<f32>,
) -> f32 {
    nitrogen_tissue_pressures[compartment] + helium_tissue_pressures[compartment]
}

#[cfg(test)]
mod commands_tissue_pressure_should {
    use super::*;
    use crate::{
        models::plan::dive_profile_result::{
            dive_profile::DiveProfile, tissue_pressure::TissuePressure,
        },
        test_fixture::{ambient_pressure_test_fixture, dive_stage_test_fixture_zhl16},
    };

    #[test]
    fn calculate_tissue_pressure_of_the_dive_profile() {
        // Given
        let dive_profile = DiveProfile {
            number_of_compartments: 16,
            ambient_pressure: ambient_pressure_test_fixture(),
            tissue_pressure: TissuePressure::new_default(16),
            ..Default::default()
        };
        let zhl16 = DiveModel::create_zhl16_dive_model_with_dive_profile(dive_profile);
        let expected_dive_stage = dive_stage_test_fixture_zhl16();

        // When

        let tissue_pressure =
            super::calculate_tissue_pressures(&zhl16, &expected_dive_stage.dive_step);

        // Then
        pretty_assertions::assert_eq!(
            expected_dive_stage.dive_model.dive_profile.tissue_pressure,
            tissue_pressure,
        );
    }
}
