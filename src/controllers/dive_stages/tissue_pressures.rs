use crate::models::plan::{
    dive_model::DiveModel,
    dive_profile_result::{dive_profile::DiveProfile, tissue_pressure::TissuePressure},
    dive_step::DiveStep,
};

pub fn calculate_tissue_pressures(dive_model: &DiveModel, dive_step: &DiveStep) -> TissuePressure {
    let mut nitrogen_tissue_pressures = vec![];
    let mut helium_tissue_pressures = vec![];
    let mut total_tissue_pressures = vec![];

    for compartment in 0..dive_model.number_of_compartments {
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
        total_tissue_pressures.push(calculate_total_tissue_pressures_2(
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
        .nitrogen_tissue_pressures[compartment]
        + ((dive_model
            .dive_profile
            .ambient_pressure
            .get_nitrogen_at_pressure()
            - dive_model
                .dive_profile
                .tissue_pressure
                .nitrogen_tissue_pressures[compartment])
            * (1.0
                - f32::powf(
                    2.0,
                    -(dive_step.time as f32 / dive_model.nitrogen_half_times[compartment]),
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
        .helium_tissue_pressures[compartment]
        + ((dive_model
            .dive_profile
            .ambient_pressure
            .get_helium_at_pressure()
            - dive_model
                .dive_profile
                .tissue_pressure
                .helium_tissue_pressures[compartment])
            * (1.0
                - f32::powf(
                    2.0,
                    -(dive_step.time as f32 / dive_model.helium_half_times[compartment]),
                )))
}

fn calculate_total_tissue_pressures_2(
    compartment: usize,
    nitrogen_tissue_pressures: Vec<f32>,
    helium_tissue_pressures: Vec<f32>,
) -> f32 {
    nitrogen_tissue_pressures[compartment] + helium_tissue_pressures[compartment]
}

fn calculate_total_tissue_pressures(compartment: usize, dive_profile: &DiveProfile) -> f32 {
    dive_profile.tissue_pressure.helium_tissue_pressures[compartment]
        + dive_profile.tissue_pressure.nitrogen_tissue_pressures[compartment]
}

#[cfg(test)]
mod commands_tissue_pressure_should {
    use super::*;
    use crate::{
        models::plan::dive_profile_result::tissue_pressure::TissuePressure,
        test::test_fixture::{
            ambient_pressure_test_fixture, default_dive_stage_test_fixture,
            dive_profile_test_fixture, dive_stage_test_fixture, tissue_pressure_test_fixture,
        },
    };

    #[test]
    fn calculate_nitrogen_tissue_pressure_of_the_dive_profile() {
        // Given
        let mut zhl16 = DiveModel::create_zhl16_dive_model();
        zhl16.dive_profile = DiveProfile {
            number_of_compartments: 16,
            ambient_pressure: ambient_pressure_test_fixture(),
            tissue_pressure: TissuePressure::new_default(16),
            ..Default::default()
        };
        let expected_dive_stage = dive_stage_test_fixture();
        let mut nitrogen_tissue_pressures = vec![];

        // When
        for compartment in 0..16 {
            nitrogen_tissue_pressures.push(format!(
                "{:.3}",
                super::calculate_nitrogen_tissue_pressures(
                    compartment,
                    &zhl16,
                    &expected_dive_stage.dive_step
                )
            ));
        }

        // Then
        let tissue_pressure = expected_dive_stage.dive_model.dive_profile.tissue_pressure;
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.nitrogen_tissue_pressures[0]),
            nitrogen_tissue_pressures[0]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.nitrogen_tissue_pressures[1]),
            nitrogen_tissue_pressures[1]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.nitrogen_tissue_pressures[2]),
            nitrogen_tissue_pressures[2]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.nitrogen_tissue_pressures[3]),
            nitrogen_tissue_pressures[3]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.nitrogen_tissue_pressures[4]),
            nitrogen_tissue_pressures[4]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.nitrogen_tissue_pressures[5]),
            nitrogen_tissue_pressures[5]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.nitrogen_tissue_pressures[6]),
            nitrogen_tissue_pressures[6]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.nitrogen_tissue_pressures[7]),
            nitrogen_tissue_pressures[7]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.nitrogen_tissue_pressures[8]),
            nitrogen_tissue_pressures[8]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.nitrogen_tissue_pressures[9]),
            nitrogen_tissue_pressures[9]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.nitrogen_tissue_pressures[10]),
            nitrogen_tissue_pressures[10]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.nitrogen_tissue_pressures[11]),
            nitrogen_tissue_pressures[11]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.nitrogen_tissue_pressures[12]),
            nitrogen_tissue_pressures[12]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.nitrogen_tissue_pressures[13]),
            nitrogen_tissue_pressures[13]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.nitrogen_tissue_pressures[14]),
            nitrogen_tissue_pressures[14]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.nitrogen_tissue_pressures[15]),
            nitrogen_tissue_pressures[15]
        );
    }

    #[test]
    fn calculate_helium_tissue_pressures_of_the_dive_profile() {
        // Given
        let mut zhl16 = DiveModel::create_zhl16_dive_model();
        zhl16.dive_profile = DiveProfile {
            number_of_compartments: 16,
            ambient_pressure: ambient_pressure_test_fixture(),
            tissue_pressure: TissuePressure::new_default(16),
            ..Default::default()
        };
        let expected_dive_stage = dive_stage_test_fixture();
        let mut helium_tissue_pressures = vec![];

        for compartment in 0..16 {
            // When
            helium_tissue_pressures.push(format!(
                "{:.3}",
                super::calculate_helium_tissue_pressures(
                    compartment,
                    &zhl16,
                    &expected_dive_stage.dive_step
                )
            ));
        }

        // Then
        let tissue_pressure = expected_dive_stage.dive_model.dive_profile.tissue_pressure;
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.helium_tissue_pressures[0]),
            helium_tissue_pressures[0]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.helium_tissue_pressures[1]),
            helium_tissue_pressures[1]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.helium_tissue_pressures[2]),
            helium_tissue_pressures[2]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.helium_tissue_pressures[3]),
            helium_tissue_pressures[3]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.helium_tissue_pressures[4]),
            helium_tissue_pressures[4]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.helium_tissue_pressures[5]),
            helium_tissue_pressures[5]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.helium_tissue_pressures[6]),
            helium_tissue_pressures[6]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.helium_tissue_pressures[7]),
            helium_tissue_pressures[7]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.helium_tissue_pressures[8]),
            helium_tissue_pressures[8]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.helium_tissue_pressures[9]),
            helium_tissue_pressures[9]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.helium_tissue_pressures[10]),
            helium_tissue_pressures[10]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.helium_tissue_pressures[11]),
            helium_tissue_pressures[11]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.helium_tissue_pressures[12]),
            helium_tissue_pressures[12]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.helium_tissue_pressures[13]),
            helium_tissue_pressures[13]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.helium_tissue_pressures[14]),
            helium_tissue_pressures[14]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.helium_tissue_pressures[15]),
            helium_tissue_pressures[15]
        );
    }

    #[test]
    fn calculate_total_tissue_pressures_of_the_dive_profile() {
        // Given
        let dive_profile = DiveProfile {
            number_of_compartments: 16,
            ambient_pressure: ambient_pressure_test_fixture(),
            tissue_pressure: TissuePressure::new(
                tissue_pressure_test_fixture().nitrogen_tissue_pressures,
                tissue_pressure_test_fixture().helium_tissue_pressures,
                default_dive_stage_test_fixture()
                    .dive_model
                    .dive_profile
                    .tissue_pressure
                    .total_tissue_pressures,
            ),
            ..Default::default()
        };
        let expected_dive_profile_model = dive_profile_test_fixture();
        let mut total_tissue_pressures = vec![];

        // When
        for compartment in 0..16 {
            total_tissue_pressures.push(format!(
                "{:.3}",
                super::calculate_total_tissue_pressures(compartment, &dive_profile)
            ));
        }

        // Then
        let tissue_pressure = expected_dive_profile_model.tissue_pressure;
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.total_tissue_pressures[0]),
            total_tissue_pressures[0]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.total_tissue_pressures[1]),
            total_tissue_pressures[1]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.total_tissue_pressures[2]),
            total_tissue_pressures[2]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.total_tissue_pressures[3]),
            total_tissue_pressures[3]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.total_tissue_pressures[4]),
            total_tissue_pressures[4]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.total_tissue_pressures[5]),
            total_tissue_pressures[5]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.total_tissue_pressures[6]),
            total_tissue_pressures[6]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.total_tissue_pressures[7]),
            total_tissue_pressures[7]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.total_tissue_pressures[8]),
            total_tissue_pressures[8]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.total_tissue_pressures[9]),
            total_tissue_pressures[9]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.total_tissue_pressures[10]),
            total_tissue_pressures[10]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.total_tissue_pressures[11]),
            total_tissue_pressures[11]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.total_tissue_pressures[12]),
            total_tissue_pressures[12]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.total_tissue_pressures[13]),
            total_tissue_pressures[13]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.total_tissue_pressures[14]),
            total_tissue_pressures[14]
        );
        pretty_assertions::assert_eq!(
            format!("{:.3}", tissue_pressure.total_tissue_pressures[15]),
            total_tissue_pressures[15]
        );
    }
}
