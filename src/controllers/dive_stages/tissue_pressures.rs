use crate::models::plan::{
    dive_model::DiveModel, dive_profile_result::dive_profile::DiveProfile, dive_step::DiveStep,
};

pub fn calculate_nitrogen_tissue_pressures(
    compartment: usize,
    dive_model: DiveModel,
    dive_step: DiveStep,
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
                    -(dive_step.time as f32 / dive_model.nitrogen_half_times[compartment]),
                )))
}

pub fn calculate_helium_tissue_pressures(
    compartment: usize,
    dive_model: DiveModel,
    dive_step: DiveStep,
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
                    -(dive_step.time as f32 / dive_model.helium_half_times[compartment]),
                )))
}

pub fn calculate_total_tissue_pressure(compartment: usize, dive_profile: DiveProfile) -> f32 {
    dive_profile.tissue_pressure.get_helium_tissue_pressures()[compartment]
        + dive_profile.tissue_pressure.get_nitrogen_tissue_pressures()[compartment]
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

        for compartment in 0..16 {
            // When
            let nitrogen_tissue_pressure = format!(
                "{:.3}",
                super::calculate_nitrogen_tissue_pressures(
                    compartment,
                    zhl16.clone(),
                    expected_dive_stage.dive_step.clone()
                )
            );

            // Then
            pretty_assertions::assert_eq!(
                format!(
                    "{:.3}",
                    expected_dive_stage
                        .dive_model
                        .dive_profile
                        .tissue_pressure
                        .get_nitrogen_tissue_pressures()[compartment]
                ),
                nitrogen_tissue_pressure
            );
        }
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

        for compartment in 0..16 {
            // When
            let helium_tissue_pressures = format!(
                "{:.3}",
                super::calculate_helium_tissue_pressures(
                    compartment,
                    zhl16.clone(),
                    expected_dive_stage.dive_step.clone()
                )
            );

            // Then
            pretty_assertions::assert_eq!(
                format!(
                    "{:.3}",
                    expected_dive_stage
                        .dive_model
                        .dive_profile
                        .tissue_pressure
                        .get_helium_tissue_pressures()[compartment]
                ),
                helium_tissue_pressures
            );
        }
    }

    #[test]
    fn calculate_total_tissue_pressures_of_the_dive_profile() {
        // Given
        let dive_profile = DiveProfile {
            number_of_compartments: 16,
            ambient_pressure: ambient_pressure_test_fixture(),
            tissue_pressure: TissuePressure::new(
                tissue_pressure_test_fixture().get_nitrogen_tissue_pressures(),
                tissue_pressure_test_fixture().get_helium_tissue_pressures(),
                default_dive_stage_test_fixture()
                    .dive_model
                    .dive_profile
                    .tissue_pressure
                    .get_total_tissue_pressures(),
            ),
            ..Default::default()
        };
        let expected_dive_profile_model = dive_profile_test_fixture();

        for compartment in 0..16 {
            // When
            let total_tissue_pressure = format!(
                "{:.3}",
                super::calculate_total_tissue_pressure(compartment, dive_profile.clone())
            );

            // Then
            pretty_assertions::assert_eq!(
                format!(
                    "{:.3}",
                    expected_dive_profile_model
                        .tissue_pressure
                        .get_total_tissue_pressures()[compartment]
                ),
                total_tissue_pressure
            );
        }
    }

    // fn dive_step_test_fixture() -> DiveStep {
    //     DiveStep {
    //         depth: 50,
    //         time: 10,
    //     }
    // }

    // pub fn tissue_pressures_dive_profile_test_fixture() -> DiveProfile {
    //     DiveProfile {
    //         oxygen_at_pressure: 1.26,
    //         helium_at_pressure: 0.600,
    //         nitrogen_at_pressure: 4.14,
    //         ..Default::default()
    //     }
    // }

    // fn tissue_pressures_total_dive_profile_test_fixture() -> DiveProfile {
    //     DiveProfile {
    //         nitrogen_tissue_pressures: [
    //             3.408, 2.399, 1.762, 1.294, 0.937, 0.685, 0.496, 0.356, 0.255, 0.192, 0.151, 0.118,
    //             0.093, 0.073, 0.057, 0.045,
    //         ],
    //         helium_tissue_pressures: [
    //             0.594, 0.540, 0.462, 0.377, 0.296, 0.228, 0.172, 0.127, 0.093, 0.071, 0.056, 0.044,
    //             0.035, 0.028, 0.022, 0.017,
    //         ],
    //         oxygen_at_pressure: 1.26,
    //         helium_at_pressure: 0.600,
    //         nitrogen_at_pressure: 4.14,
    //         ..Default::default()
    //     }
    // }

    // fn dive_profile_test_fixture() -> DiveProfile {
    //     DiveProfile {
    //         nitrogen_tissue_pressures: [
    //             3.408, 2.399, 1.762, 1.294, 0.937, 0.685, 0.496, 0.356, 0.255, 0.192, 0.151, 0.118,
    //             0.093, 0.073, 0.057, 0.045,
    //         ],
    //         helium_tissue_pressures: [
    //             0.594, 0.540, 0.462, 0.377, 0.296, 0.228, 0.172, 0.127, 0.093, 0.071, 0.056, 0.044,
    //             0.035, 0.028, 0.022, 0.017,
    //         ],
    //         total_tissue_pressures: [
    //             4.002, 2.939, 2.224, 1.671, 1.233, 0.913, 0.668, 0.483, 0.348, 0.263, 0.207, 0.162,
    //             0.128, 0.101, 0.079, 0.062,
    //         ],
    //         ..Default::default()
    //     }
    // }
}
