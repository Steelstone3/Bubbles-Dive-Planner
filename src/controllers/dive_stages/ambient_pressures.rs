use crate::models::plan::{
    cylinders::gas_mixture::GasMixture,
    dive_profile_result::{ambient_pressure::AmbientPressure, dive_profile::DiveProfile},
    dive_step::DiveStep,
};

pub fn calculate_ambient_pressures(
    dive_profile_model: &DiveProfile,
    dive_step: &DiveStep,
    gas_mixture: &GasMixture,
) -> DiveProfile {
    let ambient_pressure = 1.0 + dive_step.depth as f32 / 10.0;
    let nitrogen_at_pressure = gas_mixture.get_nitrogen() as f32 / 100.0 * ambient_pressure;
    let oxygen_at_pressure = gas_mixture.oxygen as f32 / 100.0 * ambient_pressure;
    let helium_at_pressure = gas_mixture.helium as f32 / 100.0 * ambient_pressure;

    DiveProfile {
        number_of_compartments: dive_profile_model.number_of_compartments,
        ambient_pressure: AmbientPressure::new(
            oxygen_at_pressure,
            helium_at_pressure,
            nitrogen_at_pressure,
        ),

        tissue_pressure: dive_profile_model.tissue_pressure.clone(),

        tolerated_ambient_pressures: dive_profile_model.tolerated_ambient_pressures.clone(),
        a_values: dive_profile_model.a_values.clone(),
        b_values: dive_profile_model.b_values.clone(),

        maximum_surface_pressures: dive_profile_model.maximum_surface_pressures.clone(),
        compartment_loads: dive_profile_model.compartment_loads.clone(),
        dive_ceiling: dive_profile_model.dive_ceiling.clone(),
    }
}

#[cfg(test)]
mod commands_ambient_pressures_should {
    use super::*;
    use crate::test::test_fixture::{ambient_pressure_test_fixture, dive_stage_test_fixture};

    #[test]
    fn calculate_ambient_pressures_of_the_dive_profile() {
        // Given
        let dive_step = dive_stage_test_fixture().dive_step;
        let gas_mixture = dive_stage_test_fixture().cylinder.gas_mixture;
        let ambient_pressure = ambient_pressure_test_fixture();

        // When
        let actual_dive_profile_model =
            super::calculate_ambient_pressures(&DiveProfile::default(), &dive_step, &gas_mixture);

        // Then
        assert_eq!(
            format!("{:.3}", ambient_pressure.get_oxygen_at_pressure()),
            format!(
                "{:.3}",
                actual_dive_profile_model
                    .ambient_pressure
                    .get_oxygen_at_pressure()
            )
        );
        assert_eq!(
            format!("{:.3}", ambient_pressure.get_nitrogen_at_pressure()),
            format!(
                "{:.3}",
                actual_dive_profile_model
                    .ambient_pressure
                    .get_nitrogen_at_pressure()
            )
        );
        assert_eq!(
            format!("{:.3}", ambient_pressure.get_helium_at_pressure()),
            format!(
                "{:.3}",
                actual_dive_profile_model
                    .ambient_pressure
                    .get_helium_at_pressure()
            )
        );
    }
}
