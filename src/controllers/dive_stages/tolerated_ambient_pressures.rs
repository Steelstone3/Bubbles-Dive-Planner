use crate::models::result::dive_profile::DiveProfile;

pub fn calculate_tolerated_ambient_pressure(
    compartment: usize,
    dive_profile_model: DiveProfile,
) -> f32 {
    (dive_profile_model.total_tissue_pressures[compartment]
        - dive_profile_model.a_values[compartment])
        * dive_profile_model.b_values[compartment]
}

#[cfg(test)]
mod commands_tolerated_ambient_pressures_should {
    use super::*;

    #[test]
    fn calculate_tolerated_ambient_pressures_of_the_dive_profile() {
        // Given
        let actual_dive_profile_model =
            test_fixture_tolerated_ambient_pressures_dive_profile_model();
        let expected_dive_profile_model = dive_profile_test_fixture();

        for compartment in 0..16 {
            // When
            let tolerated_ambient_pressure = format!(
                "{:.3}",
                super::calculate_tolerated_ambient_pressure(compartment, actual_dive_profile_model)
            );

            // Then
            assert_eq!(
                format!(
                    "{:.3}",
                    expected_dive_profile_model.tolerated_ambient_pressures[compartment]
                ),
                tolerated_ambient_pressure
            );
        }
    }

    fn test_fixture_tolerated_ambient_pressures_dive_profile_model() -> DiveProfile {
        DiveProfile {
            total_tissue_pressures: [
                4.002, 2.939, 2.224, 1.671, 1.233, 0.913, 0.668, 0.483, 0.348, 0.263, 0.207, 0.162,
                0.128, 0.101, 0.079, 0.062,
            ],
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
            tolerated_ambient_pressures: [
                1.318, 1.191, 0.916, 0.653, 0.404, 0.239, 0.097, -0.018, -0.106, -0.150, -0.177,
                -0.199, -0.207, -0.227, -0.234, -0.236,
            ],
            ..Default::default()
        }
    }
}
