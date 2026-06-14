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
    use crate::{
        controllers::dive_stages::ambient_pressures::calculate_ambient_pressures,
        test_fixture::{
            usn_revision_6_ambient_pressure_test_fixture, usn_revision_6_dive_stage_test_fixture,
            zhl16_ambient_pressure_test_fixture, zhl16_dive_stage_test_fixture,
        },
    };

    #[test]
    fn test_zhl16_calculate_ambient_pressures() {
        // given
        let dive_step = zhl16_dive_stage_test_fixture().dive_step;
        let gas_mixture = zhl16_dive_stage_test_fixture().cylinder.gas_mixture;
        let expected_ambient_pressure = zhl16_ambient_pressure_test_fixture();

        // when
        let ambient_pressure = calculate_ambient_pressures(&dive_step, &gas_mixture);

        // then
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

    #[test]
    fn test_usn_revision_6_calculate_ambient_pressures() {
        // given
        let dive_step = usn_revision_6_dive_stage_test_fixture().dive_step;
        let gas_mixture = usn_revision_6_dive_stage_test_fixture()
            .cylinder
            .gas_mixture;
        let expected_ambient_pressure = usn_revision_6_ambient_pressure_test_fixture();

        // when
        let ambient_pressure = calculate_ambient_pressures(&dive_step, &gas_mixture);

        // then
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
