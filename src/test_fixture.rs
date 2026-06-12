#[cfg(test)]
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

#[cfg(test)]
pub fn default_dive_stage_test_fixture_zhl16() -> DiveStage {
    let dive_model = DiveModel::create_zhl16_dive_model();

    let dive_step = DiveStep::new(50, 10);
    let gas_mixture = GasMixture::new(21, 10);
    let cylinder = Cylinder::new(12, 200, gas_mixture, 12);
    DiveStage::new(dive_model, dive_step, cylinder)
}

#[cfg(test)]
#[allow(dead_code)]
pub fn default_dive_stage_test_fixture_usn_rev_6() -> DiveStage {
    let dive_model = DiveModel::create_usn_rev_6_dive_model();

    let dive_step = DiveStep::new(50, 10);
    let gas_mixture = GasMixture::new(21, 10);
    let cylinder = Cylinder::new(12, 200, gas_mixture, 12);
    DiveStage::new(dive_model, dive_step, cylinder)
}

#[cfg(test)]
pub fn dive_stage_test_fixture_zhl16() -> DiveStage {
    let dive_profile = dive_profile_test_fixture();
    let dive_model = DiveModel::create_zhl16_dive_model_with_dive_profile(dive_profile);

    let dive_step = DiveStep::new(50, 10);

    let gas_mixture = GasMixture::new(21, 10);
    let mut cylinder = Cylinder::new(12, 200, gas_mixture, 12);

    cylinder = cylinder.update_gas_management(&dive_step);

    DiveStage::new(dive_model, dive_step, cylinder)
}

#[cfg(test)]
#[allow(dead_code)]
pub fn dive_stage_test_fixture_usn_rev_6() -> DiveStage {
    let dive_profile = dive_profile_test_fixture();
    let dive_model = DiveModel::create_usn_rev_6_dive_model_with_dive_profile(dive_profile);

    let dive_step = DiveStep::new(50, 10);

    let gas_mixture = GasMixture::new(21, 10);
    let mut cylinder = Cylinder::new(12, 200, gas_mixture, 12);

    cylinder = cylinder.update_gas_management(&dive_step);

    DiveStage::new(dive_model, dive_step, cylinder)
}

#[cfg(test)]
pub fn dive_profile_test_fixture() -> DiveProfile {
    DiveProfile {
        number_of_compartments: 16,
        ambient_pressure: ambient_pressure_test_fixture(),
        tissue_pressure: tissue_pressure_test_fixture(),
        tolerated_ambient_pressure: tolerated_ambient_pressure_test_fixture(),
        tolerated_surface_pressure: tolerated_surface_pressure_test_fixture(),
    }
}

#[cfg(test)]
pub fn ambient_pressure_test_fixture() -> AmbientPressure {
    AmbientPressure::new(1.26, 0.6, 4.14)
}

#[cfg(test)]
pub fn tissue_pressure_test_fixture() -> TissuePressure {
    TissuePressure::new(
        vec![
            3.547798, 2.7314985, 2.2159302, 1.8368304, 1.548494, 1.3445811, 1.1914635, 1.078389,
            0.9963993, 0.94532764, 0.91190034, 0.8857612, 0.8652739, 0.8490135, 0.83630437,
            0.82636875,
        ],
        vec![
            0.59391063,
            0.5395546,
            0.4618421,
            0.37741643,
            0.29569238,
            0.22824433,
            0.17192295,
            0.12713185,
            0.09290922,
            0.07081572,
            0.05604029,
            0.044314135,
            0.035010234,
            0.027558161,
            0.021691704,
            0.017078735,
        ],
        vec![
            4.1417084, 3.271053, 2.6777723, 2.2142467, 1.8441863, 1.5728254, 1.3633864, 1.2055209,
            1.0893085, 1.0161433, 0.9679406, 0.93007535, 0.9002841, 0.87657166, 0.85799605,
            0.8434475,
        ],
    )
}

#[cfg(test)]
pub fn tolerated_ambient_pressure_test_fixture() -> ToleratedAmbientPressure {
    ToleratedAmbientPressure::new(
        vec![
            1.3895957, 1.4102788, 1.2492926, 1.0878413, 0.91364884, 0.81558466, 0.7249213,
            0.6515928, 0.5941725, 0.5742205, 0.5650998, 0.56174237, 0.5736771, 0.5673645,
            0.5745757, 0.58362436,
        ],
        vec![
            1.325663, 1.0631752, 0.91873324, 0.8055621, 0.7076342, 0.5978031, 0.5244344, 0.4711127,
            0.43373698, 0.39197496, 0.36032978, 0.33166716, 0.2940648, 0.28136787, 0.25899717,
            0.23835345,
        ],
        vec![
            0.49345648, 0.6387485, 0.7102131, 0.772239, 0.80387765, 0.83647794, 0.86407954,
            0.8872352, 0.9063427, 0.9199769, 0.93003577, 0.9387277, 0.9463195, 0.95322734,
            0.95922667, 0.9645184,
        ],
    )
}

#[cfg(test)]
pub fn tolerated_surface_pressure_test_fixture() -> ToleratedSurfacePressure {
    ToleratedSurfacePressure::new(
        vec![
            3.3521843, 2.6287365, 2.3267612, 2.100498, 1.9516046, 1.7932919, 1.6817353, 1.5982095,
            1.5370724, 1.4789587, 1.4355572, 1.3969388, 1.3507903, 1.3304356, 1.3015037, 1.2751403,
        ],
        vec![
            123.55253, 124.434425, 115.085815, 105.41532, 94.495895, 87.70605, 81.07021, 75.42947,
            70.86904, 68.70667, 67.426125, 66.57953, 66.6487, 65.88606, 65.92345, 66.14547,
        ],
        4.102788,
    )
}
