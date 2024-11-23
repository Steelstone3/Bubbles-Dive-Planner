use crate::models::{
    cylinder::Cylinder, dive_model::DiveModel, dive_profile::DiveProfile, dive_stage::DiveStage,
    dive_step::DiveStep, gas_management::GasManagement, gas_mixture::GasMixture,
};

#[allow(dead_code)]
pub fn dive_stage_test_fixture() -> DiveStage {
    let mut dive_model = DiveModel::create_zhl16_dive_model();
    dive_model.dive_profile = dive_profile_test_fixture();

    DiveStage {
        dive_model,
        dive_step: DiveStep {
            depth: 50,
            time: 10,
        },
        cylinder: Cylinder {
            is_read_only: true,
            volume: 12,
            pressure: 200,
            initial_pressurised_cylinder_volume: 2400,
            gas_mixture: GasMixture {
                oxygen: 32,
                helium: 10,
                nitrogen: 58,
                maximum_operating_depth: 0.0,
            },
            gas_management: GasManagement {
                remaining: 1680,
                used: 720,
                surface_air_consumption_rate: 12,
            },
        },
    }
}

#[allow(dead_code)]
pub fn dive_stage_test_fixture_default(default_array: [f32; 16]) -> DiveStage {
    DiveStage {
        dive_model: dive_model_test_fixture_default(default_array),
        dive_step: DiveStep {
            depth: 50,
            time: 10,
        },
        cylinder: Cylinder {
            is_read_only: true,
            volume: 12,
            pressure: 200,
            initial_pressurised_cylinder_volume: 2400,
            gas_mixture: GasMixture {
                oxygen: 32,
                helium: 10,
                nitrogen: 58,
                maximum_operating_depth: 0.0,
            },
            gas_management: GasManagement {
                remaining: 1680,
                used: 720,
                surface_air_consumption_rate: 12,
            },
        },
    }
}

fn dive_model_test_fixture_default(default_array: [f32; 16]) -> DiveModel {
    DiveModel {
        number_of_compartments: 2,
        nitrogen_half_times: default_array,
        helium_half_times: default_array,
        a_values_nitrogen: default_array,
        b_values_nitrogen: default_array,
        a_values_helium: default_array,
        b_values_helium: default_array,
        dive_profile: DiveProfile {
            number_of_compartments: 16,
            maximum_surface_pressures: default_array,
            compartment_loads: default_array,
            nitrogen_tissue_pressures: default_array,
            helium_tissue_pressures: default_array,
            total_tissue_pressures: default_array,
            tolerated_ambient_pressures: default_array,
            a_values: default_array,
            b_values: default_array,
            oxygen_at_pressure: 2.34,
            helium_at_pressure: 2.56,
            nitrogen_at_pressure: 2.12,
            dive_ceiling: 0.0,
        },
        is_read_only: Default::default(),
    }
}

fn dive_profile_test_fixture() -> DiveProfile {
    DiveProfile {
        number_of_compartments: 16,
        maximum_surface_pressures: [
            3.350, 2.630, 2.33, 2.10, 1.95, 1.79, 1.68, 1.60, 1.54, 1.48, 1.44, 1.400, 1.35, 1.33,
            1.300, 1.28,
        ],
        compartment_loads: [
            124.0, 124.0, 115.0, 105.0, 94.0, 88.0, 81.0, 75.0, 71.0, 69.0, 67.0, 67.0, 67.0, 66.0,
            66.0, 66.0,
        ],
        nitrogen_tissue_pressures: [
            3.500, 2.700, 2.200, 1.8, 1.5, 1.3, 1.2, 1.1, 1.0, 0.9, 0.9, 0.9, 0.9, 0.8, 0.8, 0.8,
        ],
        helium_tissue_pressures: [
            0.594, 0.540, 0.462, 0.377, 0.296, 0.228, 0.172, 0.127, 0.093, 0.071, 0.056, 0.044,
            0.035, 0.028, 0.022, 0.017,
        ],
        total_tissue_pressures: [
            4.140, 3.270, 2.68, 2.21, 1.84, 1.57, 1.36, 1.21, 1.09, 1.02, 0.97, 0.93, 0.90, 0.88,
            0.86, 0.84,
        ],
        tolerated_ambient_pressures: [
            1.390, 1.410, 1.25, 1.09, 0.91, 0.82, 0.72, 0.65, 0.59, 0.57, 0.57, 0.56, 0.57, 0.57,
            0.57, 0.58,
        ],
        a_values: [
            1.3, 1.1, 0.9, 0.8, 0.7, 0.6, 0.5, 0.5, 0.4, 0.4, 0.4, 0.3, 0.3, 0.3, 0.3, 0.2,
        ],
        b_values: [
            0.493, 0.637, 0.708, 0.769, 0.800, 0.84, 0.859, 0.89, 0.910, 0.920, 0.93, 0.94, 0.95,
            0.95, 0.96, 0.96,
        ],
        oxygen_at_pressure: 1.26,
        helium_at_pressure: 0.600,
        nitrogen_at_pressure: 4.14,
        dive_ceiling: 4.1,
    }
}
