use crate::models::plan::dive_profile_result::dive_profile::DiveProfile;

pub fn calculate_tolerated_ambient_pressure(
    compartment: usize,
    dive_profile_model: &DiveProfile,
) -> f32 {
    (dive_profile_model.tissue_pressure.total_tissue_pressures[compartment]
        - dive_profile_model.tolerated_ambient_pressure.a_values[compartment])
        * dive_profile_model.tolerated_ambient_pressure.b_values[compartment]
}

#[cfg(test)]
mod commands_tolerated_ambient_pressures_should {
    use crate::{
        models::plan::dive_profile_result::{
            dive_profile::DiveProfile, tolerated_ambient_pressure::ToleratedAmbientPressure,
        },
        test::test_fixture::{
            ambient_pressure_test_fixture, dive_profile_test_fixture, tissue_pressure_test_fixture,
            tolerated_ambient_pressure_test_fixture,
        },
    };

    #[test]
    fn calculate_tolerated_ambient_pressures_of_the_dive_profile() {
        // Given
        let dive_profile_model = DiveProfile {
            ambient_pressure: ambient_pressure_test_fixture(),
            tissue_pressure: tissue_pressure_test_fixture(),
            tolerated_ambient_pressure: ToleratedAmbientPressure::new(
                Default::default(),
                tolerated_ambient_pressure_test_fixture().a_values,
                tolerated_ambient_pressure_test_fixture().b_values,
            ),
            ..Default::default()
        };
        let expected_dive_profile_model = dive_profile_test_fixture();
        let mut tolerated_ambient_pressures = vec![];

        // When
        for compartment in 0..16 {
            tolerated_ambient_pressures.push(format!(
                "{:.3}",
                super::calculate_tolerated_ambient_pressure(compartment, &dive_profile_model)
            ));
        }

        // Then
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .tolerated_ambient_pressures[0]
            ),
            tolerated_ambient_pressures[0]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .tolerated_ambient_pressures[1]
            ),
            tolerated_ambient_pressures[1]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .tolerated_ambient_pressures[2]
            ),
            tolerated_ambient_pressures[2]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .tolerated_ambient_pressures[3]
            ),
            tolerated_ambient_pressures[3]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .tolerated_ambient_pressures[4]
            ),
            tolerated_ambient_pressures[4]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .tolerated_ambient_pressures[5]
            ),
            tolerated_ambient_pressures[5]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .tolerated_ambient_pressures[6]
            ),
            tolerated_ambient_pressures[6]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .tolerated_ambient_pressures[7]
            ),
            tolerated_ambient_pressures[7]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .tolerated_ambient_pressures[8]
            ),
            tolerated_ambient_pressures[8]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .tolerated_ambient_pressures[9]
            ),
            tolerated_ambient_pressures[9]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .tolerated_ambient_pressures[10]
            ),
            tolerated_ambient_pressures[10]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .tolerated_ambient_pressures[11]
            ),
            tolerated_ambient_pressures[11]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .tolerated_ambient_pressures[12]
            ),
            tolerated_ambient_pressures[12]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .tolerated_ambient_pressures[13]
            ),
            tolerated_ambient_pressures[13]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .tolerated_ambient_pressures[14]
            ),
            tolerated_ambient_pressures[14]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile_model
                    .tolerated_ambient_pressure
                    .tolerated_ambient_pressures[15]
            ),
            tolerated_ambient_pressures[15]
        );
    }
}
