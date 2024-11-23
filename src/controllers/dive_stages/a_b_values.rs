use crate::models::plan::dive_model::DiveModel;

pub fn calculate_a_values(compartment: usize, dive_model: DiveModel) -> f32 {
    (dive_model.a_values_nitrogen[compartment]
        * dive_model.dive_profile.nitrogen_tissue_pressures[compartment]
        + dive_model.a_values_helium[compartment]
            * dive_model.dive_profile.helium_tissue_pressures[compartment])
        / dive_model.dive_profile.total_tissue_pressures[compartment]
}

pub fn calculate_b_values(compartment: usize, dive_model: DiveModel) -> f32 {
    (dive_model.b_values_nitrogen[compartment]
        * dive_model.dive_profile.nitrogen_tissue_pressures[compartment]
        + dive_model.b_values_helium[compartment]
            * dive_model.dive_profile.helium_tissue_pressures[compartment])
        / dive_model.dive_profile.total_tissue_pressures[compartment]
}

#[cfg(test)]
mod commands_a_b_values_should {
    use super::*;
    use crate::models::result::dive_profile::DiveProfile;

    #[test]
    fn calculate_a_values_of_the_dive_profile() {
        // Given
        let mut zhl16 = DiveModel::create_zhl16_dive_model();
        let expected_dive_profile_model = dive_profile_test_fixture();
        zhl16.dive_profile = a_b_values_dive_profile_test_fixture();

        for compartment in 0..16 {
            // When
            let a_value = format!("{:.3}", super::calculate_a_values(compartment, zhl16));

            // Then
            assert_eq!(
                format!("{:.3}", expected_dive_profile_model.a_values[compartment]),
                a_value
            );
        }
    }

    #[test]
    fn calculate_b_values_of_the_dive_profile() {
        // Given
        let mut zhl16 = DiveModel::create_zhl16_dive_model();
        let expected_dive_profile_model = dive_profile_test_fixture();
        zhl16.dive_profile = a_b_values_dive_profile_test_fixture();

        for compartment in 0..16 {
            // When
            let b_value = format!("{:.3}", super::calculate_b_values(compartment, zhl16));

            // Then
            assert_eq!(
                format!("{:.3}", expected_dive_profile_model.b_values[compartment]),
                b_value
            );
        }
    }

    fn a_b_values_dive_profile_test_fixture() -> DiveProfile {
        DiveProfile {
            nitrogen_tissue_pressures: [
                3.408, 2.399, 1.762, 1.294, 0.937, 0.685, 0.496, 0.356, 0.255, 0.192, 0.151, 0.118,
                0.093, 0.073, 0.057, 0.045,
            ],
            helium_tissue_pressures: [
                0.594, 0.540, 0.462, 0.377, 0.296, 0.228, 0.172, 0.127, 0.093, 0.071, 0.056, 0.044,
                0.035, 0.028, 0.022, 0.017,
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
}
