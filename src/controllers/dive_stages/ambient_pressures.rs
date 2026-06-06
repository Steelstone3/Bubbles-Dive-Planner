use crate::models::plan::{
    cylinders::gas_mixture::GasMixture, dive_profile_result::ambient_pressure::AmbientPressure,
    dive_step::DiveStep,
};

pub fn calculate_ambient_pressures(
    dive_step: &DiveStep,
    gas_mixture: &GasMixture,
) -> AmbientPressure {
    let ambient_pressure = 1.0 + dive_step.depth as f32 / 10.0;
    let nitrogen_at_pressure = gas_mixture.get_nitrogen() as f32 / 100.0 * ambient_pressure;
    let oxygen_at_pressure = gas_mixture.oxygen as f32 / 100.0 * ambient_pressure;
    let helium_at_pressure = gas_mixture.helium as f32 / 100.0 * ambient_pressure;

    AmbientPressure::new(oxygen_at_pressure, helium_at_pressure, nitrogen_at_pressure)
}

#[cfg(test)]
mod commands_ambient_pressures_should {
    use crate::test::test_fixture::{ambient_pressure_test_fixture, dive_stage_test_fixture};

    #[test]
    fn calculate_ambient_pressures_of_the_dive_profile() {
        // Given
        let dive_step = dive_stage_test_fixture().dive_step;
        let gas_mixture = dive_stage_test_fixture().cylinder.gas_mixture;
        let expected_ambient_pressure = ambient_pressure_test_fixture();

        // When
        let ambient_pressure = super::calculate_ambient_pressures(&dive_step, &gas_mixture);

        // Then
        pretty_assertions::assert_eq!(
            expected_ambient_pressure.get_oxygen_at_pressure(),
            ambient_pressure.get_oxygen_at_pressure()
        );
        pretty_assertions::assert_eq!(
            expected_ambient_pressure.get_nitrogen_at_pressure(),
            ambient_pressure.get_nitrogen_at_pressure()
        );
        pretty_assertions::assert_eq!(
            expected_ambient_pressure.get_helium_at_pressure(),
            ambient_pressure.get_helium_at_pressure()
        );
    }
}
