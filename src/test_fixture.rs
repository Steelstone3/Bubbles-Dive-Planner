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
pub fn zhl16_default_dive_stage_test_fixture() -> DiveStage {
    let dive_model = DiveModel::new_zhl16_dive_model();

    let dive_step = DiveStep::new(50, 10);
    let gas_mixture = GasMixture::new(21, 10);
    let cylinder = Cylinder::new(12, 200, gas_mixture, 12);
    DiveStage::new(dive_model, dive_step, cylinder)
}

#[cfg(test)]
pub fn zhl16_dive_stage_test_fixture() -> DiveStage {
    let dive_profile = zhl16_dive_profile_test_fixture();
    let dive_model = DiveModel::new_zhl16_dive_model_with_dive_profile(dive_profile);

    let dive_step = DiveStep::new(50, 10);

    let gas_mixture = GasMixture::new(21, 10);
    let mut cylinder = Cylinder::new(12, 200, gas_mixture, 12);

    cylinder = cylinder.update_gas_management(&dive_step);

    DiveStage::new(dive_model, dive_step, cylinder)
}

#[cfg(test)]
pub fn zhl16_dive_profile_test_fixture() -> DiveProfile {
    DiveProfile {
        number_of_compartments: 16,
        ambient_pressure: zhl16_ambient_pressure_test_fixture(),
        tissue_pressure: zhl16_tissue_pressure_test_fixture(),
        tolerated_ambient_pressure: zhl16_tolerated_ambient_pressure_test_fixture(),
        tolerated_surface_pressure: zhl16_tolerated_surface_pressure_test_fixture(),
    }
}

#[cfg(test)]
pub fn zhl16_ambient_pressure_test_fixture() -> AmbientPressure {
    AmbientPressure::new(1.26, 0.6, 4.14)
}

#[cfg(test)]
pub fn zhl16_tissue_pressure_test_fixture() -> TissuePressure {
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
pub fn zhl16_tolerated_ambient_pressure_test_fixture() -> ToleratedAmbientPressure {
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
pub fn zhl16_tolerated_surface_pressure_test_fixture() -> ToleratedSurfacePressure {
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

#[cfg(test)]
pub fn usn_revision_6_default_dive_stage_test_fixture() -> DiveStage {
    let dive_model = DiveModel::new_usn_revision_6_dive_model();

    let dive_step = DiveStep::new(50, 10);
    let gas_mixture = GasMixture::new(21, 10);
    let cylinder = Cylinder::new(12, 200, gas_mixture, 12);
    DiveStage::new(dive_model, dive_step, cylinder)
}

#[cfg(test)]
pub fn usn_revision_6_dive_stage_test_fixture() -> DiveStage {
    let dive_profile = usn_revision_6_dive_profile_test_fixture();
    let dive_model = DiveModel::new_usn_revision_6_dive_model_with_dive_profile(dive_profile);

    let dive_step = DiveStep::new(50, 10);

    let gas_mixture = GasMixture::new(21, 10);
    let mut cylinder = Cylinder::new(12, 200, gas_mixture, 12);

    cylinder = cylinder.update_gas_management(&dive_step);

    DiveStage::new(dive_model, dive_step, cylinder)
}

#[cfg(test)]
pub fn usn_revision_6_dive_profile_test_fixture() -> DiveProfile {
    DiveProfile {
        number_of_compartments: 9,
        ambient_pressure: usn_revision_6_ambient_pressure_test_fixture(),
        tissue_pressure: usn_revision_6_tissue_pressure_test_fixture(),
        tolerated_ambient_pressure: usn_revision_6_tolerated_ambient_pressure_test_fixture(),
        tolerated_surface_pressure: usn_revision_6_tolerated_surface_pressure_test_fixture(),
    }
}

#[cfg(test)]
pub fn usn_revision_6_ambient_pressure_test_fixture() -> AmbientPressure {
    AmbientPressure::new(1.26, 0.6, 4.14)
}

#[cfg(test)]
pub fn usn_revision_6_tissue_pressure_test_fixture() -> TissuePressure {
    TissuePressure::new(
        vec![
            3.3024998, 2.465, 1.7711923, 1.322997, 1.0680364, 0.97802114, 0.932029, 0.9041134,
            0.88536805,
        ],
        vec![
            0.45000002,
            0.3,
            0.17573595,
            0.09546215,
            0.049797572,
            0.03367542,
            0.025438035,
            0.020438218,
            0.017080843,
        ],
        vec![
            3.7524998, 2.7649999, 1.9469283, 1.4184592, 1.117834, 1.0116966, 0.957467, 0.9245516,
            0.9024489,
        ],
    )
}

#[cfg(test)]
pub fn usn_revision_6_tolerated_ambient_pressure_test_fixture() -> ToleratedAmbientPressure {
    ToleratedAmbientPressure::new(
        vec![
            1.3721964, 1.0852337, 0.8475809, 0.79128087, 0.5947675, 0.5261535, 0.48367286,
            0.4290998, 0.4361019,
        ],
        vec![
            1.34002, 1.0550452, 0.69180524, 0.32220894, 0.34712774, 0.38199717, 0.40371954,
            0.453537, 0.42359614,
        ],
        vec![
            0.56879085, 0.6346564, 0.6752971, 0.72180676, 0.7717175, 0.835563, 0.87345386,
            0.91101164, 0.9107224,
        ],
    )
}

#[cfg(test)]
pub fn usn_revision_6_tolerated_surface_pressure_test_fixture() -> ToleratedSurfacePressure {
    ToleratedSurfacePressure::new(
        vec![
            3.0981355, 2.630701, 2.172635, 1.7076213, 1.6429386, 1.578795, 1.5485997, 1.5512178,
            1.5216256,
        ],
        vec![
            121.12123, 105.10506, 89.61138, 83.066376, 68.0387, 64.08031, 61.827923, 59.601665,
            59.30821,
        ],
        3.7219644,
    )
}
