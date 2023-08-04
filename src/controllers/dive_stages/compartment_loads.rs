use crate::models::dive_profile::DiveProfile;

pub fn calculate_compartment_loads(compartment: usize, dive_profile_model: DiveProfile) -> f32 {
    dive_profile_model.total_tissue_pressures[compartment]
        / dive_profile_model.maximum_surface_pressures[compartment]
        * 100.0
}

#[cfg(test)]
mod commands_compartment_loads_should {
    use super::*;

    #[test]
    fn calculate_compartment_loads_of_the_dive_profile() {
        // Given
        let actual_dive_profile = compartment_loads_dive_profile_test_fixture();
        let expected_dive_profile = dive_profile_test_fixture();

        for compartment in 0..16 {
            // When
            let compartment_load = format!(
                "{:.3}",
                super::calculate_compartment_loads(compartment, actual_dive_profile)
            );
            // Then
            assert_eq!(
                format!(
                    "{:.3}",
                    expected_dive_profile.compartment_loads[compartment]
                ),
                compartment_load
            );
        }
    }

    fn compartment_loads_dive_profile_test_fixture() -> DiveProfile {
        DiveProfile {
            maximum_surface_pressures: [
                3.356, 2.640, 2.342, 2.122, 1.978, 1.828, 1.719, 1.637, 1.577, 1.521, 1.482, 1.450,
                1.415, 1.400, 1.380, 1.356,
            ],
            total_tissue_pressures: [
                4.002, 2.939, 2.224, 1.671, 1.233, 0.913, 0.668, 0.483, 0.348, 0.263, 0.207, 0.162,
                0.128, 0.101, 0.079, 0.062,
            ],
            ..Default::default()
        }
    }

    fn dive_profile_test_fixture() -> DiveProfile {
        DiveProfile {
            compartment_loads: [
                119.249, 111.326, 94.962, 78.746, 62.336, 49.945, 38.860, 29.505, 22.067, 17.291,
                13.968, 11.172, 9.046, 7.214, 5.725, 4.572,
            ],
            ..Default::default()
        }
    }
}
