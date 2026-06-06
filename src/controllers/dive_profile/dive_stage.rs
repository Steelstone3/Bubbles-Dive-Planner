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
            SelectableDiveModel::Bulhmann => {
                self.dive_planning.select_dive_model.selected_dive_model =
                    Some(SelectableDiveModel::Bulhmann);
                self.dive_stage.dive_model = DiveModel::create_zhl16_dive_model()
            }
            SelectableDiveModel::Usn => {
                self.dive_planning.select_dive_model.selected_dive_model =
                    Some(SelectableDiveModel::Usn);
                self.dive_stage.dive_model = DiveModel::create_usn_rev_6_dive_model()
            }
        }
    }

    pub fn update_dive_profile(&self) -> DiveStage {
        // calculate gas usage
        let cylinder = self
            .dive_stage
            .cylinder
            .update_gas_management(&self.dive_stage.dive_step);

        let mut dive_model = self.dive_stage.dive_model.clone();

        // calculate ambient pressure
        dive_model.dive_profile.ambient_pressure = calculate_ambient_pressures(
            &self.dive_stage.dive_step,
            &self.dive_stage.cylinder.gas_mixture,
        );

        // calculate tissue pressures
        dive_model.dive_profile.tissue_pressure =
            calculate_tissue_pressures(&dive_model, &self.dive_stage.dive_step);

        // calculate tolerated ambient pressures
        dive_model.dive_profile.tolerated_ambient_pressure =
            calculate_tolerated_ambient_pressures(&dive_model);

        // calculate tolerated surface pressures
        dive_model.dive_profile.tolerated_surface_pressure =
            calculate_tolerated_surface_pressures(&dive_model.dive_profile);

        DiveStage::new(dive_model, self.dive_stage.dive_step.clone(), cylinder)
    }
}

#[cfg(test)]
mod dive_stage_should {
    use crate::{
        models::application::dive_planner::DivePlanner,
        test::test_fixture::{default_dive_stage_test_fixture, dive_stage_test_fixture},
    };

    #[test]
    fn test_update_dive_profile() {
        // Given
        let expected_dive_stage = dive_stage_test_fixture();
        let mut dive_planner = DivePlanner::default();
        dive_planner.dive_stage = default_dive_stage_test_fixture();

        // When
        let dive_stage = dive_planner.update_dive_profile();

        // Then
        pretty_assertions::assert_eq!(expected_dive_stage, dive_stage);
    }
}
