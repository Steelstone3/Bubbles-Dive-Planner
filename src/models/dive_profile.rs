use super::dive_stage::DiveStage;
use crate::controllers::dive_stages::{
    a_b_values::{calculate_a_values, calculate_b_values},
    ambient_pressures::calculate_ambient_pressures,
    compartment_loads::calculate_compartment_loads,
    max_surface_pressures::calculate_max_surface_pressures,
    tissue_pressures::{
        calculate_helium_tissue_pressures, calculate_nitrogen_tissue_pressures,
        calculate_total_tissue_pressure,
    },
    tolerated_ambient_pressures::calculate_tolerated_ambient_pressure,
};
use serde::{Deserialize, Serialize};
use std::fmt::Display;

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
}

impl DiveProfile {
    pub fn new(number_of_compartments: usize) -> Self {
        Self {
            number_of_compartments,
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

        dive_stage.dive_model.dive_model.dive_profile = calculate_ambient_pressures(
            dive_stage.dive_model.dive_model.dive_profile,
            dive_stage.dive_step,
            dive_stage.cylinder.gas_mixture,
        );

        for compartment in 0..dive_stage.dive_model.dive_model.number_of_compartments {
            dive_stage = DiveProfile::run_dive_stages(compartment, dive_stage);
        }

        dive_stage.cylinder.is_read_only = true;

        dive_stage
    }

    fn run_dive_stages(compartment: usize, mut dive_stage: DiveStage) -> DiveStage {
        dive_stage
            .dive_model
            .dive_model
            .dive_profile
            .nitrogen_tissue_pressures[compartment] = calculate_nitrogen_tissue_pressures(
            compartment,
            dive_stage.dive_model.dive_model,
            dive_stage.dive_step,
        );
        dive_stage
            .dive_model
            .dive_model
            .dive_profile
            .helium_tissue_pressures[compartment] = calculate_helium_tissue_pressures(
            compartment,
            dive_stage.dive_model.dive_model,
            dive_stage.dive_step,
        );
        dive_stage
            .dive_model
            .dive_model
            .dive_profile
            .total_tissue_pressures[compartment] = calculate_total_tissue_pressure(
            compartment,
            dive_stage.dive_model.dive_model.dive_profile,
        );
        dive_stage.dive_model.dive_model.dive_profile.a_values[compartment] =
            calculate_a_values(compartment, dive_stage.dive_model.dive_model);
        dive_stage.dive_model.dive_model.dive_profile.b_values[compartment] =
            calculate_b_values(compartment, dive_stage.dive_model.dive_model);
        dive_stage
            .dive_model
            .dive_model
            .dive_profile
            .tolerated_ambient_pressures[compartment] = calculate_tolerated_ambient_pressure(
            compartment,
            dive_stage.dive_model.dive_model.dive_profile,
        );
        dive_stage
            .dive_model
            .dive_model
            .dive_profile
            .maximum_surface_pressures[compartment] = calculate_max_surface_pressures(
            compartment,
            dive_stage.dive_model.dive_model.dive_profile,
        );
        dive_stage
            .dive_model
            .dive_model
            .dive_profile
            .compartment_loads[compartment] =
            calculate_compartment_loads(compartment, dive_stage.dive_model.dive_model.dive_profile);

        dive_stage
    }

    fn display_results(self) -> String {
        let mut dive_results = "".to_string();

        for (_, compartment) in (0..self.number_of_compartments).enumerate() {
            let total_tissue_pressures = format!("{:.3}", self.total_tissue_pressures[compartment]);
            let tolerated_ambient_pressures =
                format!("{:.3}", self.tolerated_ambient_pressures[compartment]);
            let maximum_surface_pressures =
                format!("{:.3}", self.maximum_surface_pressures[compartment]);
            let compartment_loads = format!("{:.3}", self.compartment_loads[compartment]);

            let dive_result = format!(
                "C: {} | TPt: {} | TAP: {} | MSP: {} | CLp: {}\n",
                compartment + 1,
                total_tissue_pressures,
                tolerated_ambient_pressures,
                maximum_surface_pressures,
                compartment_loads
            );

            dive_results.push_str(&dive_result);
        }

        dive_results
    }
}

impl Display for DiveProfile {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        write!(f, "{}", self.display_results())
    }
}

#[cfg(test)]
mod dive_profile_should {
    use super::*;
    use crate::models::{
        cylinder::Cylinder, dive_model::DiveModel, dive_step::DiveStep,
        gas_management::GasManagement, gas_mixture::GasMixture, select_dive_model::SelectDiveModel,
    };

    #[test]
    fn create_a_new_dive_profile_with_a_set_number_of_compartments() {
        // Given
        let dive_profile = DiveProfile::new(9);

        // Then
        assert_eq!(9, dive_profile.number_of_compartments);
    }

    #[test]
    fn display_a_set_of_dive_profile_results() {
        // Given
        let dive_profile = dive_profile_result_test_fixture();
        let expected_dive_profile_result = "C: 1 | TPt: 4.002 | TAP: 1.318 | MSP: 3.356 | CLp: 119.249\nC: 2 | TPt: 2.939 | TAP: 1.191 | MSP: 2.640 | CLp: 111.326\nC: 3 | TPt: 2.224 | TAP: 0.916 | MSP: 2.342 | CLp: 94.962\nC: 4 | TPt: 1.671 | TAP: 0.653 | MSP: 2.122 | CLp: 78.746\nC: 5 | TPt: 1.233 | TAP: 0.404 | MSP: 1.978 | CLp: 62.336\nC: 6 | TPt: 0.913 | TAP: 0.239 | MSP: 1.828 | CLp: 49.945\nC: 7 | TPt: 0.668 | TAP: 0.097 | MSP: 1.719 | CLp: 38.860\nC: 8 | TPt: 0.483 | TAP: -0.018 | MSP: 1.637 | CLp: 29.505\nC: 9 | TPt: 0.348 | TAP: -0.106 | MSP: 1.577 | CLp: 22.067\nC: 10 | TPt: 0.263 | TAP: -0.150 | MSP: 1.521 | CLp: 17.291\nC: 11 | TPt: 0.207 | TAP: -0.177 | MSP: 1.482 | CLp: 13.968\nC: 12 | TPt: 0.162 | TAP: -0.199 | MSP: 1.450 | CLp: 11.172\nC: 13 | TPt: 0.128 | TAP: -0.207 | MSP: 1.415 | CLp: 9.046\nC: 14 | TPt: 0.101 | TAP: -0.227 | MSP: 1.400 | CLp: 7.214\nC: 15 | TPt: 0.079 | TAP: -0.234 | MSP: 1.380 | CLp: 5.725\nC: 16 | TPt: 0.062 | TAP: -0.236 | MSP: 1.356 | CLp: 4.572\n";

        // When
        let actual_dive_profile_result = DiveProfile::display_results(dive_profile);

        // Then
        assert_eq!(expected_dive_profile_result, actual_dive_profile_result);
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
            dive_model: SelectDiveModel {
                dive_model: DiveModel::create_zhl16_dive_model(),
                ..Default::default()
            },
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
            dive_model: SelectDiveModel {
                dive_model: DiveModel::create_zhl16_dive_model(),
                ..Default::default()
            },
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
                actual_dive_stage
                    .dive_model
                    .dive_model
                    .dive_profile
                    .oxygen_at_pressure
            )
        );
        assert_eq!(
            format!("{:.2}", expected_dive_profile.helium_at_pressure),
            format!(
                "{:.2}",
                actual_dive_stage
                    .dive_model
                    .dive_model
                    .dive_profile
                    .helium_at_pressure
            )
        );
        assert_eq!(
            format!("{:.2}", expected_dive_profile.nitrogen_at_pressure),
            format!(
                "{:.2}",
                actual_dive_stage
                    .dive_model
                    .dive_model
                    .dive_profile
                    .nitrogen_at_pressure
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
                        .dive_model
                        .dive_profile
                        .total_tissue_pressures[compartment]
                )
            );
            assert_eq!(
                format!("{:.1}", expected_dive_profile.a_values[compartment]),
                format!(
                    "{:.1}",
                    actual_dive_stage
                        .dive_model
                        .dive_model
                        .dive_profile
                        .a_values[compartment]
                )
            );
            assert_eq!(
                format!("{:.2}", expected_dive_profile.b_values[compartment]),
                format!(
                    "{:.2}",
                    actual_dive_stage
                        .dive_model
                        .dive_model
                        .dive_profile
                        .b_values[compartment]
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
                    actual_dive_stage
                        .dive_model
                        .dive_model
                        .dive_profile
                        .compartment_loads[compartment]
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
            },
            initial_pressurised_cylinder_volume: 2400,
            volume: 12,
            pressure: 200,
            gas_management: GasManagement {
                remaining: 1680,
                used: 720,
                surface_air_consumption_rate: 12,
            },
            is_read_only: true,
        }
    }

    fn dive_profile_test_fixture() -> DiveProfile {
        DiveProfile {
            number_of_compartments: 16,
            maximum_surface_pressures: [
                3.356, 2.640, 2.342, 2.122, 1.978, 1.828, 1.719, 1.637, 1.577, 1.521, 1.482, 1.450,
                1.415, 1.400, 1.380, 1.356,
            ],
            compartment_loads: [
                119.249, 111.326, 94.962, 78.746, 62.336, 49.945, 38.860, 29.505, 22.067, 17.291,
                13.968, 11.172, 9.046, 7.214, 5.725, 4.572,
            ],
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

    fn dive_profile_result_test_fixture() -> DiveProfile {
        DiveProfile {
            number_of_compartments: 16,
            maximum_surface_pressures: [
                3.356_234_3,
                2.640_234_2,
                2.342_234_1,
                2.122_234_3,
                1.978_234_3,
                1.828_234_2,
                1.719_234_2,
                1.637_234_2,
                1.577_234_3,
                1.521_234_3,
                1.482_234_2,
                1.450_234_2,
                1.415_234_3,
                1.400_234_2,
                1.380_234_2,
                1.356_234_3,
            ],
            compartment_loads: [
                119.249_24,
                111.326_23,
                94.962_234,
                78.746_23,
                62.336_235,
                49.945_236,
                38.860_233,
                29.505_234,
                22.067_234,
                17.291_235,
                13.968_234,
                11.172_235,
                9.046_234,
                7.214_234_4,
                5.725_234,
                4.572_234,
            ],
            nitrogen_tissue_pressures: [
                3.408_234_1,
                2.399_234_3,
                1.762_234_2,
                1.294_234_3,
                0.937_234_2,
                0.685_234_25,
                0.496_234_24,
                0.356_234_22,
                0.255_234_24,
                0.192_234_23,
                0.151_234_24,
                0.118_234_23,
                0.093_234_23,
                0.073_234_24,
                0.057234234,
                0.045_234_233,
            ],
            helium_tissue_pressures: [
                0.594_234_2,
                0.540_234_2,
                0.462_234_23,
                0.377_234_22,
                0.296_234_22,
                0.228_234_23,
                0.172_234_24,
                0.127_234_24,
                0.093_234_23,
                0.071_234_23,
                0.056234234,
                0.044_234_235,
                0.035_234_235,
                0.028_234_234,
                0.022_234_235,
                0.017_234_234,
            ],
            total_tissue_pressures: [
                4.002_234_5,
                2.939_234_3,
                2.224_234_3,
                1.671_234_3,
                1.233_234_3,
                0.913_234_23,
                0.668_234_2,
                0.483_234_23,
                0.348_234_24,
                0.263_234_32,
                0.207_234_23,
                0.162_234_23,
                0.128_234_24,
                0.101234324,
                0.079_234_235,
                0.062_234_234,
            ],
            tolerated_ambient_pressures: [
                1.318_234_2,
                1.191_234_2,
                0.916_234_25,
                0.653_234_24,
                0.404_234_23,
                0.239_234_24,
                0.097_234_234,
                -0.018_234_324,
                -0.106_234_24,
                -0.150_234_24,
                -0.177_234_23,
                -0.199_234_23,
                -0.207_234_23,
                -0.227_234_23,
                -0.234_234_23,
                -0.236_234_23,
            ],
            a_values: [
                1.328_234_2,
                1.070_234_2,
                0.930_234_25,
                0.822_234_2,
                0.728_234_23,
                0.625_234_25,
                0.555_234_25,
                0.503_234_2,
                0.466_234_24,
                0.427_234_23,
                0.399_234_24,
                0.376_234_23,
                0.349_234_22,
                0.341_234_24,
                0.326_234_22,
                0.309_234_23,
            ],
            b_values: [
                0.493_234_25,
                0.637_234_2,
                0.708_234_25,
                0.769_234_3,
                0.800_234_26,
                0.831_234_2,
                0.859_234_33,
                0.882_234_2,
                0.900_234_34,
                0.914_234_2,
                0.923_234_2,
                0.931_234_24,
                0.938_234_2,
                0.944_234_3,
                0.949_234_25,
                0.955_234_2,
            ],
            oxygen_at_pressure: 1.262_342_3,
            helium_at_pressure: 0.600_234_3,
            nitrogen_at_pressure: 4.142_343,
        }
    }
}
