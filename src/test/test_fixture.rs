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
            3.548, 2.731, 2.216, 1.837, 1.548, 1.345, 1.191, 1.078, 0.996, 0.945, 0.912, 0.886,
            0.865, 0.849, 0.836, 0.826,
        ],
        vec![
            0.594, 0.540, 0.462, 0.377, 0.296, 0.228, 0.172, 0.127, 0.093, 0.071, 0.056, 0.044,
            0.035, 0.028, 0.022, 0.017,
        ],
        vec![
            4.142, 3.271, 2.678, 2.214, 1.844, 1.573, 1.363, 1.205, 1.089, 1.016, 0.968, 0.93,
            0.90, 0.877, 0.858, 0.843,
        ],
    )
}

#[allow(dead_code)]
pub fn tolerated_ambient_pressure_test_fixture() -> ToleratedAmbientPressure {
    ToleratedAmbientPressure::new(
        vec![
            1.388, 1.387, 1.249, 1.087, 0.913, 0.815, 0.725, 0.651, 0.593, 0.574, 0.565, 0.562,
            0.573, 0.568, 0.574, 0.584,
        ],
        vec![
            1.326, 1.1, 0.919, 0.806, 0.708, 0.598, 0.524, 0.471, 0.434, 0.392, 0.360, 0.332,
            0.294, 0.281, 0.259, 0.238,
        ],
        vec![
            0.493, 0.639, 0.710, 0.772, 0.804, 0.836, 0.864, 0.887, 0.906, 0.920, 0.93, 0.939,
            0.946, 0.953, 0.959, 0.965,
        ],
    )
}

#[allow(dead_code)]
pub fn tolerated_surface_pressure_test_fixture() -> ToleratedSurfacePressure {
    ToleratedSurfacePressure::new(
        vec![
            3.354, 2.665, 2.327, 2.101, 1.952, 1.794, 1.681, 1.598, 1.538, 1.479, 1.435, 1.397,
            1.351, 1.33, 1.302, 1.274,
        ],
        vec![
            123.494, 122.739, 115.084, 105.378, 94.467, 87.681, 81.083, 75.407, 70.806, 68.695,
            67.456, 66.571, 66.617, 65.940, 65.899, 66.170,
        ],
        4.1,
    )
}
