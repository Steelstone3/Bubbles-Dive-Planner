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
use std::fmt::Display;

#[derive(PartialEq, Debug, Default, Copy, Clone)]
pub struct DiveProfile {
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
    pub fn update_dive_profile(mut dive_stage: DiveStage) -> DiveStage {
        dive_stage.dive_model.dive_profile = calculate_ambient_pressures(
            dive_stage.dive_model.dive_profile,
            dive_stage.dive_step,
            dive_stage.cylinder.gas_mixture,
        );

        for compartment in 0..dive_stage.dive_model.number_of_compartments {
            dive_stage = DiveProfile::run_dive_stages(compartment, dive_stage);
        }

        dive_stage
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

    fn display_results(self) -> String {
        let mut dive_results = "".to_string();

        for (_, compartment) in (0..self.compartment_loads.len()).enumerate() {
            let dive_result = format!(
                "C: {} | TPt: {} | TAP: {} | MSP: {} | CLp: {}\n",
                compartment + 1,
                format!("{:.3}", self.total_tissue_pressures[compartment]),
                format!("{:.3}",self.tolerated_ambient_pressures[compartment]),
                format!("{:.3}",self.maximum_surface_pressures[compartment]),
                format!("{:.3}",self.compartment_loads[compartment])
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
        cylinder::Cylinder, dive_model::DiveModel, dive_step::DiveStep, gas_mixture::GasMixture,
    };

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
            },
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
            maximum_surface_pressures: [
                3.356234234,
                2.640234234234,
                2.342234234234,
                2.122234234324,
                1.978234234,
                1.828234234,
                1.719234234,
                1.637234234,
                1.577234234,
                1.521234234234,
                1.482234234,
                1.450234234324,
                1.415234324,
                1.40023423423,
                1.380234234,
                1.356234324324,
            ],
            compartment_loads: [
                119.249234234,
                111.326234234,
                94.962234324,
                78.746234234324,
                62.336234234342,
                49.945234324324,
                38.860234324324,
                29.505234324324,
                22.067234234,
                17.291234324234,
                13.968234324234,
                11.172234234,
                9.046234234,
                7.214234234,
                5.725234234,
                4.572234234,
            ],
            nitrogen_tissue_pressures: [
                3.408234234,
                2.399234234,
                1.762234234,
                1.294234234,
                0.937234234,
                0.685234234,
                0.496234234,
                0.356234234234,
                0.255234234234,
                0.192234234,
                0.151234234,
                0.118234234234,
                0.093234234234234,
                0.073234234234,
                0.057234234,
                0.045234234234,
            ],
            helium_tissue_pressures: [
                0.594234234234,
                0.540234234324234,
                0.462234234324234,
                0.377234234,
                0.296234234234,
                0.228234234234234,
                0.172234234324,
                0.127234234324,
                0.093234234234,
                0.071234234324,
                0.056234234,
                0.044234234324,
                0.035234234234,
                0.028234234324,
                0.022234234234,
                0.017234234324,
            ],
            total_tissue_pressures: [
                4.002234234234,
                2.939234234324,
                2.224234234234,
                1.671234234324,
                1.233234234234,
                0.91323423423423,
                0.668234234234,
                0.483234234234,
                0.348234234234,
                0.263234324324,
                0.207234234234,
                0.162234234234,
                0.128234234234,
                0.101234324,
                0.079234234234,
                0.062234234324324,
            ],
            tolerated_ambient_pressures: [
                1.318234234234,
                1.191234234324,
                0.916234234234,
                0.653234234234,
                0.404234234234,
                0.239234234234,
                0.097234234234,
                -0.018234324324,
                -0.106234234234,
                -0.150234234324,
                -0.177234234234324,
                -0.199234234234,
                -0.207234234234,
                -0.227234234234,
                -0.234234234234,
                -0.236234234234,
            ],
            a_values: [
                1.328234234234,
                1.070234234324,
                0.930234234234,
                0.822234234234,
                0.728234234324,
                0.625234234234234,
                0.555234234234,
                0.503234234234,
                0.466234234234234,
                0.427234234234234,
                0.399234234234,
                0.376234234234234,
                0.349234234234,
                0.341234234234234,
                0.326234234234234,
                0.309234234324234324,
            ],
            b_values: [
                0.493234234234,
                0.637234234234,
                0.708234234234,
                0.769234324324324,
                0.80023423423434,
                0.831234234324,
                0.859234324234234,
                0.882234234324324,
                0.900234324234234,
                0.914234234234234,
                0.923234234234,
                0.931234234324,
                0.938234234234,
                0.944234324324,
                0.949234234234,
                0.955234234234,
            ],
            oxygen_at_pressure: 1.26234234234,
            helium_at_pressure: 0.600234324324,
            nitrogen_at_pressure: 4.14234324234,
        }
    }
}
