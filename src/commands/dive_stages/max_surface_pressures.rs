use crate::models::dive_profile::DiveProfile;

pub fn calculate_max_surface_pressure(compartment: usize, dive_profile_model: DiveProfile) -> f32 {
    (1.0 / dive_profile_model.b_values[compartment]) + dive_profile_model.a_values[compartment]
}

#[cfg(test)]
mod commands_max_surface_pressures_should {
    use super::*;

    #[test]
    fn calculate_max_surface_pressure() {
        //Arrange
        let actual_dive_profile = max_surface_pressure_dive_profile_test_fixture();
        let expected_dive_profile = dive_profile_test_fixture();

        for compartment in 0..16 {
            //Act
            //Assert
            assert_eq!(
                format!(
                    "{:.3}",
                    expected_dive_profile.maximum_surface_pressures[compartment]
                ),
                format!(
                    "{:.3}",
                    super::calculate_max_surface_pressure(compartment, actual_dive_profile)
                )
            );
        }
    }

    fn max_surface_pressure_dive_profile_test_fixture() -> DiveProfile {
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
            compartment_loads: [
                119.249, 111.326, 94.962, 78.746, 62.336, 49.945, 38.860, 29.505, 22.067, 17.291,
                13.968, 11.172, 9.046, 7.214, 5.725, 4.572,
            ],
            tissue_pressures_nitrogen: [
                3.408, 2.399, 1.762, 1.294, 0.937, 0.685, 0.496, 0.356, 0.255, 0.192, 0.151, 0.118,
                0.093, 0.073, 0.057, 0.045,
            ],
            tissue_pressures_helium: [
                0.594, 0.540, 0.462, 0.377, 0.296, 0.228, 0.172, 0.127, 0.093, 0.071, 0.056, 0.044,
                0.035, 0.028, 0.022, 0.017,
            ],
            tissue_pressures_total: [
                4.002, 2.939, 2.224, 1.671, 1.233, 0.913, 0.668, 0.483, 0.348, 0.263, 0.207, 0.162,
                0.128, 0.101, 0.079, 0.062,
            ],
            tolerated_ambient_pressures: [
                1.318, 1.191, 0.916, 0.653, 0.404, 0.239, 0.097, -0.018, -0.106, -0.150, -0.177,
                -0.199, -0.207, -0.227, -0.234, -0.236,
            ],
            a_values: [
                1.328, 1.070, 0.930, 0.822, 0.728, 0.625, 0.555, 0.503, 0.466, 0.427, 0.399, 0.376,
                0.349, 0.341, 0.326, 0.309,
            ],
            b_values: [
                0.493, 0.637, 0.708, 0.769, 0.800, 0.831, 0.859, 0.882, 0.900, 0.914, 0.923, 0.931,
                0.938, 0.944, 0.949, 0.955,
            ],
            oxygen_at_pressure: 1.26,
            helium_at_pressure: 0.600,
            nitrogen_at_pressure: 4.14,
        }
    }
}
