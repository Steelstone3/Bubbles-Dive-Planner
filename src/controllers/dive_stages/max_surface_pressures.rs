use crate::models::plan::dive_profile_result::dive_profile::DiveProfile;

pub fn calculate_max_surface_pressures(
    compartment: usize,
    dive_profile_model: &DiveProfile,
) -> f32 {
    (1.0 / dive_profile_model.tolerated_ambient_pressure.b_values[compartment])
        + dive_profile_model.tolerated_ambient_pressure.a_values[compartment]
}

#[cfg(test)]
mod commands_max_surface_pressures_should {
    use crate::test::test_fixture::{
        dive_profile_test_fixture, tolerated_ambient_pressure_test_fixture,
    };

    use super::*;

    #[test]
    fn calculate_max_surface_pressures_of_the_dive_profile() {
        // Given
        let dive_profile = DiveProfile {
            tolerated_ambient_pressure: tolerated_ambient_pressure_test_fixture(),
            ..Default::default()
        };
        let expected_dive_profile = dive_profile_test_fixture();
        let mut max_surface_pressures = vec![];

        // When
        for compartment in 0..16 {
            max_surface_pressures.push(format!(
                "{:.3}",
                super::calculate_max_surface_pressures(compartment, &dive_profile)
            ));
        }

        // Then
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .maximum_surface_pressures[0]
            ),
            max_surface_pressures[0]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .maximum_surface_pressures[1]
            ),
            max_surface_pressures[1]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .maximum_surface_pressures[2]
            ),
            max_surface_pressures[2]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .maximum_surface_pressures[3]
            ),
            max_surface_pressures[3]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .maximum_surface_pressures[4]
            ),
            max_surface_pressures[4]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .maximum_surface_pressures[5]
            ),
            max_surface_pressures[5]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .maximum_surface_pressures[6]
            ),
            max_surface_pressures[6]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .maximum_surface_pressures[7]
            ),
            max_surface_pressures[7]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .maximum_surface_pressures[8]
            ),
            max_surface_pressures[8]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .maximum_surface_pressures[9]
            ),
            max_surface_pressures[9]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .maximum_surface_pressures[10]
            ),
            max_surface_pressures[10]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .maximum_surface_pressures[11]
            ),
            max_surface_pressures[11]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .maximum_surface_pressures[12]
            ),
            max_surface_pressures[12]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .maximum_surface_pressures[13]
            ),
            max_surface_pressures[13]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .maximum_surface_pressures[14]
            ),
            max_surface_pressures[14]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .maximum_surface_pressures[15]
            ),
            max_surface_pressures[15]
        );
    }
}
