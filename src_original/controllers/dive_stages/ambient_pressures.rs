use crate::models::{
    plan::{cylinders::gas_mixture::GasMixture, dive_step::DiveStep},
    result::dive_profile::DiveProfile,
};

pub fn calculate_ambient_pressures(
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
    fn calculate_ambient_pressures_of_the_dive_profile() {
        // Given
        let dive_step = dive_step_test_fixture();
        let gas_mixture = gas_mixture_test_fixture();
        let expected_dive_profile_model = dive_profile_test_fixture();

        // When
        let actual_dive_profile_model =
            super::calculate_ambient_pressures(expected_dive_profile_model, dive_step, gas_mixture);

        // Then
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
            maximum_operating_depth: 0.0,
        }
    }

    fn dive_profile_test_fixture() -> DiveProfile {
        DiveProfile {
            oxygen_at_pressure: 1.26,
            helium_at_pressure: 0.600,
            nitrogen_at_pressure: 4.14,
            ..Default::default()
        }
    }
}
