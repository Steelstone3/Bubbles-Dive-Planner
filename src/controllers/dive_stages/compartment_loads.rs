use crate::models::plan::dive_profile_result::dive_profile::DiveProfile;

pub fn calculate_compartment_loads(compartment: usize, dive_profile_model: &DiveProfile) -> f32 {
    dive_profile_model.tissue_pressure.total_tissue_pressures[compartment]
        / dive_profile_model
            .tolerated_surface_pressure
            .maximum_surface_pressures[compartment]
        * 100.0
}

#[cfg(test)]
mod commands_compartment_loads_should {
    use crate::{
        models::plan::dive_profile_result::tolerated_surface_pressure::ToleratedSurfacePressure,
        test::test_fixture::{
            dive_profile_test_fixture, tissue_pressure_test_fixture,
            tolerated_surface_pressure_test_fixture,
        },
    };

    use super::*;

    #[test]
    fn calculate_compartment_loads_of_the_dive_profile() {
        // Given
        let dive_profile = DiveProfile {
            tissue_pressure: tissue_pressure_test_fixture(),
            tolerated_surface_pressure: ToleratedSurfacePressure::new(
                tolerated_surface_pressure_test_fixture().maximum_surface_pressures,
                Default::default(),
                Default::default(),
            ),
            ..Default::default()
        };
        let expected_dive_profile = dive_profile_test_fixture();
        let mut compartment_loads = vec![];

        // When
        for compartment in 0..16 {
            compartment_loads.push(format!(
                "{:.3}",
                super::calculate_compartment_loads(compartment, &dive_profile)
            ));
        }

        // Then
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .compartment_loads[0]
            ),
            compartment_loads[0]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .compartment_loads[1]
            ),
            compartment_loads[1]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .compartment_loads[2]
            ),
            compartment_loads[2]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .compartment_loads[3]
            ),
            compartment_loads[3]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .compartment_loads[4]
            ),
            compartment_loads[4]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .compartment_loads[5]
            ),
            compartment_loads[5]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .compartment_loads[6]
            ),
            compartment_loads[6]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .compartment_loads[7]
            ),
            compartment_loads[7]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .compartment_loads[8]
            ),
            compartment_loads[8]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .compartment_loads[9]
            ),
            compartment_loads[9]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .compartment_loads[10]
            ),
            compartment_loads[10]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .compartment_loads[11]
            ),
            compartment_loads[11]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .compartment_loads[12]
            ),
            compartment_loads[12]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .compartment_loads[13]
            ),
            compartment_loads[13]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .compartment_loads[14]
            ),
            compartment_loads[14]
        );
        assert_eq!(
            format!(
                "{:.3}",
                expected_dive_profile
                    .tolerated_surface_pressure
                    .compartment_loads[15]
            ),
            compartment_loads[15]
        );
    }
}
