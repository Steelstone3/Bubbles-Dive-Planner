use crate::models::plan::dive_model::DiveModel;

pub fn calculate_a_values(compartment: usize, dive_model: &DiveModel) -> f32 {
    (dive_model.a_values_nitrogen[compartment]
        * dive_model
            .dive_profile
            .tissue_pressure
            .nitrogen_tissue_pressures[compartment]
        + dive_model.a_values_helium[compartment]
            * dive_model
                .dive_profile
                .tissue_pressure
                .helium_tissue_pressures[compartment])
        / dive_model
            .dive_profile
            .tissue_pressure
            .total_tissue_pressures[compartment]
}

pub fn calculate_b_values(compartment: usize, dive_model: &DiveModel) -> f32 {
    (dive_model.b_values_nitrogen[compartment]
        * dive_model
            .dive_profile
            .tissue_pressure
            .nitrogen_tissue_pressures[compartment]
        + dive_model.b_values_helium[compartment]
            * dive_model
                .dive_profile
                .tissue_pressure
                .helium_tissue_pressures[compartment])
        / dive_model
            .dive_profile
            .tissue_pressure
            .total_tissue_pressures[compartment]
}

#[cfg(test)]
mod commands_a_b_values_should {
    use crate::{
        models::plan::dive_profile_result::dive_profile::DiveProfile,
        test::test_fixture::{dive_profile_test_fixture, tissue_pressure_test_fixture},
    };

    use super::*;

    #[test]
    fn calculate_a_values_of_the_dive_profile() {
        // Given
        let mut zhl16 = DiveModel::create_zhl16_dive_model();
        let expected_dive_profile_model = dive_profile_test_fixture();
        zhl16.dive_profile = DiveProfile {
            tissue_pressure: tissue_pressure_test_fixture(),
            ..Default::default()
        };
        let mut a_values = vec![];

        // When
        for compartment in 0..16 {
            a_values.push(format!(
                "{:.3}",
                super::calculate_a_values(compartment, &zhl16)
            ));
        }

        // Then
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .a_values[0]
            ),
            a_values[0]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .a_values[2]
            ),
            a_values[2]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .a_values[3]
            ),
            a_values[3]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .a_values[4]
            ),
            a_values[4]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .a_values[5]
            ),
            a_values[5]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .a_values[6]
            ),
            a_values[6]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .a_values[7]
            ),
            a_values[7]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .a_values[8]
            ),
            a_values[8]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .a_values[9]
            ),
            a_values[9]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .a_values[10]
            ),
            a_values[10]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .a_values[11]
            ),
            a_values[11]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .a_values[12]
            ),
            a_values[12]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .a_values[13]
            ),
            a_values[13]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .a_values[14]
            ),
            a_values[14]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .a_values[15]
            ),
            a_values[15]
        );
    }

    #[test]
    fn calculate_b_values_of_the_dive_profile() {
        // Given
        let mut zhl16 = DiveModel::create_zhl16_dive_model();
        let expected_dive_profile_model = dive_profile_test_fixture();
        zhl16.dive_profile = DiveProfile {
            tissue_pressure: tissue_pressure_test_fixture(),
            ..Default::default()
        };
        let mut b_values = vec![];

        // When
        for compartment in 0..16 {
            b_values.push(format!(
                "{:.3}",
                super::calculate_b_values(compartment, &zhl16)
            ));
        }

        // Then
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .b_values[0]
            ),
            b_values[0]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .b_values[1]
            ),
            b_values[1]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .b_values[2]
            ),
            b_values[2]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .b_values[3]
            ),
            b_values[3]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .b_values[4]
            ),
            b_values[4]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .b_values[5]
            ),
            b_values[5]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .b_values[6]
            ),
            b_values[6]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .b_values[7]
            ),
            b_values[7]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .b_values[8]
            ),
            b_values[8]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .b_values[9]
            ),
            b_values[9]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .b_values[10]
            ),
            b_values[10]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .b_values[11]
            ),
            b_values[11]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .b_values[12]
            ),
            b_values[12]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .b_values[13]
            ),
            b_values[13]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .b_values[14]
            ),
            b_values[14]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .b_values[15]
            ),
            b_values[15]
        );
    }
}
