use crate::models::dive_profile::DiveProfile;

pub fn calculate_max_surface_pressures(compartment: usize, dive_profile_model: DiveProfile) -> f32 {
    (1.0 / dive_profile_model.b_values[compartment]) + dive_profile_model.a_values[compartment]
}

#[cfg(test)]
mod commands_max_surface_pressures_should {
    use super::*;

    #[test]
    fn calculate_max_surface_pressures_of_the_dive_profile() {
        //Given
        let actual_dive_profile = max_surface_pressures_dive_profile_test_fixture();
        let expected_dive_profile = dive_profile_test_fixture();

        for compartment in 0..16 {
            //When
            let max_surface_pressure = format!(
                "{:.3}",
                super::calculate_max_surface_pressures(compartment, actual_dive_profile)
            );

            //Then
            assert_eq!(
                format!(
                    "{:.3}",
                    expected_dive_profile.maximum_surface_pressures[compartment]
                ),
                max_surface_pressure
            );
        }
    }

    fn max_surface_pressures_dive_profile_test_fixture() -> DiveProfile {
        DiveProfile {
            a_values: [
                1.328, 1.070, 0.930, 0.822, 0.728, 0.625, 0.555, 0.503, 0.466, 0.427, 0.399, 0.376,
                0.349, 0.341, 0.326, 0.309,
            ],
            b_values: [
                0.493, 0.637, 0.708, 0.769, 0.800, 0.831, 0.859, 0.882, 0.900, 0.914, 0.923, 0.931,
                0.938, 0.944, 0.949, 0.955,
            ],
            ..Default::default()
        }
    }

    fn dive_profile_test_fixture() -> DiveProfile {
        DiveProfile {
            maximum_surface_pressures: [
                3.356, 2.640, 2.342, 2.122, 1.978, 1.828, 1.719, 1.637, 1.577, 1.521, 1.482, 1.450,
                1.415, 1.400, 1.380, 1.356,
            ],
            ..Default::default()
        }
    }
}
