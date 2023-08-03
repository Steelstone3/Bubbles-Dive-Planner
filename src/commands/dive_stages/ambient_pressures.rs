use crate::models::{dive_profile::DiveProfile, dive_step::DiveStep, gas_mixture::GasMixture};

pub fn calculate_ambient_pressure(
    mut dive_profile_model: DiveProfile,
    dive_step: DiveStep,
    gas_mixture: GasMixture,
) -> DiveProfile {
    let ambient_pressure = 1.0 + dive_step.depth as f32 / 10.0;
    dive_profile_model.nitrogen_at_pressure =
        gas_mixture.nitrogen as f32 / 100.0 * ambient_pressure;
    dive_profile_model.oxygen_at_pressure = gas_mixture.oxygen as f32 / 100.0 * ambient_pressure;
    dive_profile_model.helium_at_pressure = gas_mixture.helium as f32 / 100.0 * ambient_pressure;

    dive_profile_model
}

#[cfg(test)]
mod commands_ambient_pressures_should {
    use super::*;

    #[test]
    fn calculate_ambient_pressure() {
        let dive_step = dive_step_test_fixture();
        let gas_mixture = gas_mixture_test_fixture();
        let expected_dive_profile_model = dive_profile_test_fixture();

        let actual_dive_profile_model =
            super::calculate_ambient_pressure(expected_dive_profile_model, dive_step, gas_mixture);

        assert_eq!(
            format!("{:.3}", expected_dive_profile_model.oxygen_at_pressure),
            format!("{:.3}", actual_dive_profile_model.oxygen_at_pressure)
        );
        assert_eq!(
            format!("{:.3}", expected_dive_profile_model.nitrogen_at_pressure),
            format!("{:.3}", actual_dive_profile_model.nitrogen_at_pressure)
        );
        assert_eq!(
            format!("{:.3}", expected_dive_profile_model.helium_at_pressure),
            format!("{:.3}", actual_dive_profile_model.helium_at_pressure)
        );
    }

    fn dive_step_test_fixture() -> DiveStep {
        DiveStep {
            depth: 50,
            time: 10,
        }
    }

    fn gas_mixture_test_fixture() -> GasMixture {
        GasMixture {
            oxygen: 21,
            helium: 10,
            nitrogen: 69,
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
