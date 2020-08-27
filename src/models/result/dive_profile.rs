use crate::{
    controllers::dive_stages::{
        a_b_values::{calculate_a_values, calculate_b_values},
        ambient_pressures::calculate_ambient_pressures,
        compartment_loads::calculate_compartment_loads,
        max_surface_pressures::calculate_max_surface_pressures,
        tissue_pressures::{
            calculate_helium_tissue_pressures, calculate_nitrogen_tissue_pressures,
            calculate_total_tissue_pressure,
        },
        tolerated_ambient_pressures::calculate_tolerated_ambient_pressure,
    },
    models::plan::dive_stage::DiveStage,
};
use serde::{Deserialize, Serialize};

#[derive(PartialEq, Debug, Default, Copy, Clone, Serialize, Deserialize)]
pub struct DiveProfile {
    pub number_of_compartments: usize,
    pub maximum_surface_pressures: [f32; 16],
    pub compartment_loads: [f32; 16],
    pub nitrogen_tissue_pressures: [f32; 16],
    pub helium_tissue_pressures: [f32; 16],
    pub total_tissue_pressures: [f32; 16],
    pub tolerated_ambient_pressures: [f32; 16],
    pub a_values: [f32; 16],
    pub b_values: [f32; 16],
    pub oxygen_at_pressure: f32,
    pub helium_at_pressure: f32,
    pub nitrogen_at_pressure: f32,
    pub dive_ceiling: f32,
}

impl DiveProfile {
    pub fn new(number_of_compartments: usize) -> Self {
        Self {
            number_of_compartments,
            nitrogen_tissue_pressures: [
                0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79,
                0.79, 0.79,
            ],
            total_tissue_pressures: [
                0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79,
                0.79, 0.79,
            ],
            ..Default::default()
        }
    }

    pub fn update_dive_profile(mut dive_stage: DiveStage) -> DiveStage {
        if !dive_stage.validate() {
            return dive_stage;
        }

        dive_stage
            .cylinder
            .gas_management
            .update_gas_management(dive_stage.dive_step);

        dive_stage.dive_model.dive_profile = calculate_ambient_pressures(
            dive_stage.dive_model.dive_profile,
            dive_stage.dive_step,
            dive_stage.cylinder.gas_mixture,
        );

        for compartment in 0..dive_stage.dive_model.number_of_compartments {
            dive_stage = DiveProfile::run_dive_stages(compartment, dive_stage);
        }

        dive_stage.dive_model.dive_profile.calculate_dive_ceiling();

        dive_stage
    }

    pub fn display_dive_ceiling(&self) -> String {
        format!("Dive Ceiling: {:.2} (m)", self.dive_ceiling)
    }

    fn run_dive_stages(compartment: usize, mut dive_stage: DiveStage) -> DiveStage {
        dive_stage.dive_model.dive_profile.nitrogen_tissue_pressures[compartment] =
            calculate_nitrogen_tissue_pressures(
                compartment,
                dive_stage.dive_model,
                dive_stage.dive_step,
            );
        dive_stage.dive_model.dive_profile.helium_tissue_pressures[compartment] =
            calculate_helium_tissue_pressures(
                compartment,
                dive_stage.dive_model,
                dive_stage.dive_step,
            );
        dive_stage.dive_model.dive_profile.total_tissue_pressures[compartment] =
            calculate_total_tissue_pressure(compartment, dive_stage.dive_model.dive_profile);
        dive_stage.dive_model.dive_profile.a_values[compartment] =
            calculate_a_values(compartment, dive_stage.dive_model);
        dive_stage.dive_model.dive_profile.b_values[compartment] =
            calculate_b_values(compartment, dive_stage.dive_model);
        dive_stage
            .dive_model
            .dive_profile
            .tolerated_ambient_pressures[compartment] =
            calculate_tolerated_ambient_pressure(compartment, dive_stage.dive_model.dive_profile);
        dive_stage.dive_model.dive_profile.maximum_surface_pressures[compartment] =
            calculate_max_surface_pressures(compartment, dive_stage.dive_model.dive_profile);
        dive_stage.dive_model.dive_profile.compartment_loads[compartment] =
            calculate_compartment_loads(compartment, dive_stage.dive_model.dive_profile);

        dive_stage
    }

    pub fn calculate_dive_ceiling(&mut self) {
        self.dive_ceiling = (self
            .tolerated_ambient_pressures
            .iter()
            .cloned()
            .fold(f32::NEG_INFINITY, f32::max)
            - 1.0)
            * 10.0
    }
}

#[cfg(test)]
mod dive_profile_should {
    use super::*;
    use crate::models::plan::{
        cylinders::{cylinder::Cylinder, gas_management::GasManagement, gas_mixture::GasMixture},
        dive_model::DiveModel,
        dive_step::DiveStep,
    };

    #[test]
    fn display_read_only_dive_ceiling() {
        // Given
        let dive_profile = DiveProfile {
            dive_ceiling: 3.765,
            ..Default::default()
        };
        let expected_display = "Dive Ceiling: 3.77 (m)";

        // When
        let display = dive_profile.display_dive_ceiling();

        // Then
        assert_eq!(expected_display, display);
    }

    #[test]
    fn create_a_new_dive_profile_with_a_set_number_of_compartments() {
        // Given
        let dive_profile = DiveProfile::new(9);

        // Then
        assert_eq!(9, dive_profile.number_of_compartments);
    }

    #[test]
    fn return_error_message_if_parameters_are_invalid() {
        // Given
        let dive_stage = DiveStage::default();

        // When
        let actual_dive_profile_result = DiveProfile::update_dive_profile(dive_stage);

        // Then
        assert_eq!(dive_stage, actual_dive_profile_result);
    }

    #[test]
    fn update_gas_management_stage() {
        // Given
        let dive_stage = DiveStage {
            dive_model: DiveModel::create_zhl16_dive_model(),

            dive_step: dive_step_test_fixture(),
            cylinder: cylinder_test_fixture(),
        };
        let expected_dive_stage = DiveStage {
            cylinder: used_cylinder_test_fixture(),
            ..Default::default()
        };

        // When
        let actual_dive_stage = DiveProfile::update_dive_profile(dive_stage);

        // Then
        assert_eq!(expected_dive_stage.cylinder, actual_dive_stage.cylinder);
    }

    #[test]
    fn update_dive_profile_by_running_each_dive_stages() {
        // Given
        let dive_stage = DiveStage {
            dive_model: DiveModel::create_zhl16_dive_model(),

            dive_step: dive_step_test_fixture(),
            cylinder: cylinder_test_fixture(),
        };
        let expected_dive_profile = dive_profile_test_fixture();

        // When
        let actual_dive_stage = DiveProfile::update_dive_profile(dive_stage);

        // Then
        assert_eq!(
            format!("{:.2}", expected_dive_profile.oxygen_at_pressure),
            format!(
                "{:.2}",
                actual_dive_stage.dive_model.dive_profile.oxygen_at_pressure
            )
        );
        assert_eq!(
            format!("{:.2}", expected_dive_profile.helium_at_pressure),
            format!(
                "{:.2}",
                actual_dive_stage.dive_model.dive_profile.helium_at_pressure
            )
        );
        assert_eq!(
            format!("{:.2}", expected_dive_profile.nitrogen_at_pressure),
            format!(
                "{:.2}",
                actual_dive_stage
                    .dive_model
                    .dive_profile
                    .nitrogen_at_pressure
            )
        );
        assert_eq!(
            format!("{:.2}", expected_dive_profile.dive_ceiling),
            format!(
                "{:.2}",
                actual_dive_stage.dive_model.dive_profile.dive_ceiling
            )
        );

        for compartment in 0..16 {
            assert_eq!(
                format!(
                    "{:.1}",
                    expected_dive_profile.nitrogen_tissue_pressures[compartment]
                ),
                format!(
                    "{:.1}",
                    actual_dive_stage
                        .dive_model
                        .dive_profile
                        .nitrogen_tissue_pressures[compartment]
                )
            );
            assert_eq!(
                format!(
                    "{:.3}",
                    expected_dive_profile.helium_tissue_pressures[compartment]
                ),
                format!(
                    "{:.3}",
                    actual_dive_stage
                        .dive_model
                        .dive_profile
                        .helium_tissue_pressures[compartment]
                )
            );
            assert_eq!(
                format!(
                    "{:.2}",
                    expected_dive_profile.total_tissue_pressures[compartment]
                ),
                format!(
                    "{:.2}",
                    actual_dive_stage
                        .dive_model
                        .dive_profile
                        .total_tissue_pressures[compartment]
                )
            );
            assert_eq!(
                format!("{:.1}", expected_dive_profile.a_values[compartment]),
                format!(
                    "{:.1}",
                    actual_dive_stage.dive_model.dive_profile.a_values[compartment]
                )
            );
            assert_eq!(
                format!("{:.2}", expected_dive_profile.b_values[compartment]),
                format!(
                    "{:.2}",
                    actual_dive_stage.dive_model.dive_profile.b_values[compartment]
                )
            );
            assert_eq!(
                format!(
                    "{:.2}",
                    expected_dive_profile.tolerated_ambient_pressures[compartment]
                ),
                format!(
                    "{:.2}",
                    actual_dive_stage
                        .dive_model
                        .dive_profile
                        .tolerated_ambient_pressures[compartment]
                )
            );
            assert_eq!(
                format!(
                    "{:.2}",
                    expected_dive_profile.maximum_surface_pressures[compartment]
                ),
                format!(
                    "{:.2}",
                    actual_dive_stage
                        .dive_model
                        .dive_profile
                        .maximum_surface_pressures[compartment]
                )
            );
            assert_eq!(
                format!(
                    "{:.0}",
                    expected_dive_profile.compartment_loads[compartment]
                ),
                format!(
                    "{:.0}",
                    actual_dive_stage.dive_model.dive_profile.compartment_loads[compartment]
                )
            );
        }
    }

    fn dive_step_test_fixture() -> DiveStep {
        DiveStep {
            depth: 50,
            time: 10,
        }
    }

    fn cylinder_test_fixture() -> Cylinder {
        Cylinder {
            gas_mixture: GasMixture {
                oxygen: 21,
                helium: 10,
                nitrogen: 69,
                maximum_operating_depth: 0.0,
            },
            initial_pressurised_cylinder_volume: 2400,
            volume: 12,
            pressure: 200,
            gas_management: GasManagement {
                remaining: 2400,
                used: 720,
                surface_air_consumption_rate: 12,
            },
            ..Default::default()
        }
    }

    fn used_cylinder_test_fixture() -> Cylinder {
        Cylinder {
            gas_mixture: GasMixture {
                oxygen: 21,
                helium: 10,
                nitrogen: 69,
                maximum_operating_depth: 0.0,
            },
            initial_pressurised_cylinder_volume: 2400,
            volume: 12,
            pressure: 200,
            gas_management: GasManagement {
                remaining: 1680,
                used: 720,
                surface_air_consumption_rate: 12,
            },
        }
    }

    fn dive_profile_test_fixture() -> DiveProfile {
        DiveProfile {
            number_of_compartments: 16,
            maximum_surface_pressures: [
                3.350, 2.630, 2.33, 2.10, 1.95, 1.79, 1.68, 1.60, 1.54, 1.48, 1.44, 1.400, 1.35,
                1.33, 1.300, 1.28,
            ],
            compartment_loads: [
                124.0, 124.0, 115.0, 105.0, 94.0, 88.0, 81.0, 75.0, 71.0, 69.0, 67.0, 67.0, 67.0,
                66.0, 66.0, 66.0,
            ],
            nitrogen_tissue_pressures: [
                3.500, 2.700, 2.200, 1.8, 1.5, 1.3, 1.2, 1.1, 1.0, 0.9, 0.9, 0.9, 0.9, 0.8, 0.8,
                0.8,
            ],
            helium_tissue_pressures: [
                0.594, 0.540, 0.462, 0.377, 0.296, 0.228, 0.172, 0.127, 0.093, 0.071, 0.056, 0.044,
                0.035, 0.028, 0.022, 0.017,
            ],
            total_tissue_pressures: [
                4.140, 3.270, 2.68, 2.21, 1.84, 1.57, 1.36, 1.21, 1.09, 1.02, 0.97, 0.93, 0.90,
                0.88, 0.86, 0.84,
            ],
            tolerated_ambient_pressures: [
                1.390, 1.410, 1.25, 1.09, 0.91, 0.82, 0.72, 0.65, 0.59, 0.57, 0.57, 0.56, 0.57,
                0.57, 0.57, 0.58,
            ],
            a_values: [
                1.3, 1.1, 0.9, 0.8, 0.7, 0.6, 0.5, 0.5, 0.4, 0.4, 0.4, 0.3, 0.3, 0.3, 0.3, 0.2,
            ],
            b_values: [
                0.493, 0.637, 0.708, 0.769, 0.800, 0.84, 0.859, 0.89, 0.910, 0.920, 0.93, 0.94,
                0.95, 0.95, 0.96, 0.96,
            ],
            oxygen_at_pressure: 1.26,
            helium_at_pressure: 0.600,
            nitrogen_at_pressure: 4.14,
            dive_ceiling: 4.1,
        }
    }
}
