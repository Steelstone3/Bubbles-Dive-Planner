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

#[allow(dead_code)]
pub fn default_dive_stage_test_fixture() -> DiveStage {
    let dive_model = DiveModel::create_zhl16_dive_model();

    let dive_step = DiveStep::new(50, 10);
    let gas_mixture = GasMixture::new(32, 10);
    let cylinder = Cylinder::new(12, 200, gas_mixture, 12);
    DiveStage::new(dive_model, dive_step, cylinder)
}

#[allow(dead_code)]
pub fn dive_stage_test_fixture() -> DiveStage {
    let mut dive_model = DiveModel::create_zhl16_dive_model();
    dive_model.dive_profile = dive_profile_test_fixture();

    let dive_step = DiveStep::new(50, 10);

    let gas_mixture = GasMixture::new(21, 10);
    let mut cylinder = Cylinder::new(12, 200, gas_mixture, 12);

    cylinder = cylinder.update_gas_management(&dive_step);

    DiveStage::new(dive_model, dive_step, cylinder)
}

#[allow(dead_code)]
pub fn dive_profile_test_fixture() -> DiveProfile {
    DiveProfile {
        number_of_compartments: 16,
        ambient_pressure: ambient_pressure_test_fixture(),
        tissue_pressure: tissue_pressure_test_fixture(),
        tolerated_ambient_pressure: tolerated_ambient_pressure_test_fixture(),
        tolerated_surface_pressure: tolerated_surface_pressure_test_fixture(),
    }
}

#[allow(dead_code)]
pub fn ambient_pressure_test_fixture() -> AmbientPressure {
    AmbientPressure::new(1.26, 0.6, 4.14)
}

#[allow(dead_code)]
pub fn tissue_pressure_test_fixture() -> TissuePressure {
    TissuePressure::new(
        vec![
            3.548, 2.731, 2.216, 1.837, 1.548, 1.345, 1.191, 1.078, 0.996, 0.945, 0.912, 0.886, 0.865, 0.849, 0.836, 0.826,
        ],
        vec![
            0.594, 0.540, 0.462, 0.377, 0.296, 0.228, 0.172, 0.127, 0.093, 0.071, 0.056, 0.044,
            0.035, 0.028, 0.022, 0.017,
        ],
        vec![
            4.142, 3.271, 2.678, 2.214, 1.844, 1.573, 1.363, 1.205, 1.089, 1.016, 0.968, 0.93, 0.90, 0.877,
            0.858, 0.843,
        ],
    )
}

#[allow(dead_code)]
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

#[allow(dead_code)]
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
