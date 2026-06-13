use crate::{
    application::states::selectable_dive_model::SelectableDiveModel,
    controllers::dive_stages::{
        ambient_pressures::calculate_ambient_pressures,
        tissue_pressures::calculate_tissue_pressures,
        tolerated_ambient_pressures::calculate_tolerated_ambient_pressures,
        tolerated_surface_pressures::calculate_tolerated_surface_pressures,
    },
    models::{
        application::dive_planner::DivePlanner,
        plan::{dive_model::DiveModel, dive_stage::DiveStage},
    },
};

impl DivePlanner {
    pub fn dive_model_selected(&mut self, selectable_dive_model: SelectableDiveModel) {
        match selectable_dive_model {
            SelectableDiveModel::BulhmannZhl16 => {
                self.dive_planning.select_dive_model.selected_dive_model =
                    Some(SelectableDiveModel::BulhmannZhl16);
                self.dive_stage.dive_model = DiveModel::new_zhl16_dive_model()
            }
            SelectableDiveModel::UsnRevision6 => {
                self.dive_planning.select_dive_model.selected_dive_model =
                    Some(SelectableDiveModel::UsnRevision6);
                self.dive_stage.dive_model = DiveModel::new_usn_revision_6_dive_model()
            }
        }
    }

    pub fn update_dive_profile(dive_stage: &DiveStage) -> DiveStage {
        if !dive_stage.is_valid() {
            return dive_stage.clone();
        }

        // calculate gas usage
        let cylinder = dive_stage
            .cylinder
            .update_gas_management(&dive_stage.dive_step);

        let mut dive_model = dive_stage.dive_model.clone();

        // calculate ambient pressure
        dive_model.dive_profile.ambient_pressure =
            calculate_ambient_pressures(&dive_stage.dive_step, &dive_stage.cylinder.gas_mixture);

        // calculate tissue pressures
        dive_model.dive_profile.tissue_pressure =
            calculate_tissue_pressures(&dive_model, &dive_stage.dive_step);

        // calculate tolerated ambient pressures
        dive_model.dive_profile.tolerated_ambient_pressure =
            calculate_tolerated_ambient_pressures(&dive_model);

        // calculate tolerated surface pressures
        dive_model.dive_profile.tolerated_surface_pressure =
            calculate_tolerated_surface_pressures(&dive_model.dive_profile);

        DiveStage::new(dive_model, dive_stage.dive_step.clone(), cylinder)
    }
}

#[cfg(test)]
mod dive_stage_should {
    use crate::{
        application::states::selectable_dive_model::SelectableDiveModel,
        models::{
            application::dive_planner::DivePlanner,
            plan::{
                cylinders::{cylinder::Cylinder, gas_mixture::GasMixture},
                dive_model::DiveModel,
                dive_stage::DiveStage,
                dive_step::DiveStep,
            },
        },
        test_fixture::{default_dive_stage_test_fixture_zhl16, dive_stage_test_fixture_zhl16},
    };

    #[test]
    fn test_dive_model_selected_zhl16() {
        // Given
        let expected_dive_model = DiveModel::new_zhl16_dive_model();
        let mut dive_planner = DivePlanner::default();

        // When
        dive_planner.dive_model_selected(SelectableDiveModel::BulhmannZhl16);

        // Then
        pretty_assertions::assert_eq!(expected_dive_model, dive_planner.dive_stage.dive_model);
    }

    #[test]
    fn test_dive_model_selected_usn_revision_6() {
        // Given
        let expected_dive_model = DiveModel::new_usn_revision_6_dive_model();
        let mut dive_planner = DivePlanner::default();

        // When
        dive_planner.dive_model_selected(SelectableDiveModel::UsnRevision6);

        // Then
        pretty_assertions::assert_eq!(expected_dive_model, dive_planner.dive_stage.dive_model);
    }

    #[test]
    fn test_update_dive_profile_invalid() {
        // Given
        let expected_dive_stage = DiveStage::new(
            DiveModel::new_zhl16_dive_model(),
            DiveStep::new(0, 0),
            Cylinder::new(0, 0, GasMixture::new(100, 100), 12),
        );

        // When
        let dive_stage = DivePlanner::update_dive_profile(&expected_dive_stage);

        // Then
        pretty_assertions::assert_eq!(expected_dive_stage, dive_stage);
    }

    #[test]
    fn test_update_dive_profile() {
        // Given
        let expected_dive_stage = dive_stage_test_fixture_zhl16();

        // When
        let dive_stage = DivePlanner::update_dive_profile(&default_dive_stage_test_fixture_zhl16());

        // Then
        pretty_assertions::assert_eq!(expected_dive_stage, dive_stage);
    }
}
