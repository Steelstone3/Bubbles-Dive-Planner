use crate::models::plan::{
    cylinders::{cylinder::Cylinder, gas_mixture::GasMixture},
    dive_model::DiveModel,
    dive_profile_result::{
        ambient_pressure::AmbientPressure, dive_profile::DiveProfile,
        tissue_pressure::TissuePressure, tolerated_ambient_pressure::ToleratedAmbientPressure,
        tolerated_surface_pressure::ToleratedSurfacePressure,
    },
    dive_stage::DiveStage,
    dive_step::DiveStep,
};

pub fn default_dive_stage_test_fixture() -> DiveStage {
    let dive_model = DiveModel::create_zhl16_dive_model();

    let dive_step = DiveStep::new(50, 10);
    let gas_mixture = GasMixture::new(21, 10);
    let cylinder = Cylinder::new(12, 200, gas_mixture, 12);
    DiveStage::new(dive_model, dive_step, cylinder)
}

pub fn dive_stage_test_fixture() -> DiveStage {
    let mut dive_model = DiveModel::create_zhl16_dive_model();
    dive_model.dive_profile = dive_profile_test_fixture();

    let dive_step = DiveStep::new(50, 10);

    let gas_mixture = GasMixture::new(21, 10);
    let mut cylinder = Cylinder::new(12, 200, gas_mixture, 12);

    cylinder = cylinder.update_gas_management(&dive_step);

    DiveStage::new(dive_model, dive_step, cylinder)
}

pub fn dive_profile_test_fixture() -> DiveProfile {
    DiveProfile {
        number_of_compartments: 16,
        ambient_pressure: ambient_pressure_test_fixture(),
        tissue_pressure: tissue_pressure_test_fixture(),
        tolerated_ambient_pressure: tolerated_ambient_pressure_test_fixture(),
        tolerated_surface_pressure: tolerated_surface_pressure_test_fixture(),
    }
}

pub fn ambient_pressure_test_fixture() -> AmbientPressure {
    AmbientPressure::new(1.26, 0.6, 4.14)
}

pub fn tissue_pressure_test_fixture() -> TissuePressure {
    TissuePressure::new(
        vec![
            3.500, 2.700, 2.200, 1.8, 1.5, 1.3, 1.2, 1.1, 1.0, 0.9, 0.9, 0.9, 0.9, 0.8, 0.8, 0.8,
        ],
        vec![
            0.594, 0.540, 0.462, 0.377, 0.296, 0.228, 0.172, 0.127, 0.093, 0.071, 0.056, 0.044,
            0.035, 0.028, 0.022, 0.017,
        ],
        vec![
            4.140, 3.270, 2.68, 2.21, 1.84, 1.57, 1.36, 1.21, 1.09, 1.02, 0.97, 0.93, 0.90, 0.88,
            0.86, 0.84,
        ],
    )
}

pub fn tolerated_ambient_pressure_test_fixture() -> ToleratedAmbientPressure {
    ToleratedAmbientPressure::new(
        vec![
            1.390, 1.410, 1.25, 1.09, 0.91, 0.82, 0.72, 0.65, 0.59, 0.57, 0.57, 0.56, 0.57, 0.57,
            0.57, 0.58,
        ],
        vec![
            1.3, 1.1, 0.9, 0.8, 0.7, 0.6, 0.5, 0.5, 0.4, 0.4, 0.4, 0.3, 0.3, 0.3, 0.3, 0.2,
        ],
        vec![
            0.493, 0.637, 0.708, 0.769, 0.800, 0.84, 0.859, 0.89, 0.910, 0.920, 0.93, 0.94, 0.95,
            0.95, 0.96, 0.96,
        ],
    )
}

pub fn tolerated_surface_pressure_test_fixture() -> ToleratedSurfacePressure {
    ToleratedSurfacePressure::new(
        vec![
            3.350, 2.630, 2.33, 2.10, 1.95, 1.79, 1.68, 1.60, 1.54, 1.48, 1.44, 1.400, 1.35, 1.33,
            1.300, 1.28,
        ],
        vec![
            124.0, 124.0, 115.0, 105.0, 94.0, 88.0, 81.0, 75.0, 71.0, 69.0, 67.0, 67.0, 67.0, 66.0,
            66.0, 66.0,
        ],
        4.1,
    )
}
