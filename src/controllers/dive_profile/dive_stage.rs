use crate::{
    commands::selectable_dive_model::SelectableDiveModel,
    models::{application::dive_planner::DivePlanner, result::dive_profile::DiveProfile},
};

impl DivePlanner {
    pub fn dive_model_selected(&mut self, selectable_dive_model: SelectableDiveModel) {
        self.select_dive_model
            .select_dive_model(selectable_dive_model, &mut self.dive_stage.dive_model);
    }

    pub fn update_dive_profile(&mut self) {
        self.select_cylinder
            .assign_cylinder(self.dive_stage.cylinder);

        self.update_dive_stage();

        self.add_result();

        self.select_cylinder
            .assign_cylinder(self.dive_stage.cylinder);

        self.decompression_steps
            .assign_decompression_steps(self.dive_stage.calculate_decompression_dive_steps());

        self.application_state.is_planning = false;
    }

    fn update_dive_stage(&mut self) {
        let dive_stage = DiveProfile::update_dive_profile(self.dive_stage);
        self.dive_stage = dive_stage;
    }

    fn add_result(&mut self) {
        self.dive_results.add_dive_result(self.dive_stage);
        self.application_state.redo_buffer = Default::default();
    }
}

#[cfg(test)]
mod dive_stage_should {
    use crate::{
        commands::{
            selectable_cylinder::SelectableCylinder, selectable_dive_model::SelectableDiveModel,
        },
        models::{
            application::{dive_planner::DivePlanner, select_cylinder::SelectCylinder},
            information::decompression_steps::DecompressionSteps,
            plan::{dive_model::DiveModel, dive_step::DiveStep},
        },
        test_fixture::dive_stage_test_fixture,
    };
    use rstest::rstest;

    #[rstest]
    #[case(
        DiveModel::create_zhl16_dive_model(),
        SelectableDiveModel::Bulhmann,
        DiveModel::create_usn_rev_6_dive_model()
    )]
    #[case(
        DiveModel::create_usn_rev_6_dive_model(),
        SelectableDiveModel::Usn,
        DiveModel::create_zhl16_dive_model()
    )]
    fn select_dive_model(
        #[case] expected_dive_model: DiveModel,
        #[case] selectable_dive_model: SelectableDiveModel,
        #[case] dive_model: DiveModel,
    ) {
        // Given

        use crate::models::{
            application::{dive_planner::DivePlanner, select_dive_model::SelectDiveModel},
            plan::dive_stage::DiveStage,
        };
        let select_dive_model = SelectDiveModel {
            dive_model_list: Default::default(),
            selected_dive_model: Some(selectable_dive_model),
        };
        let mut dive_planner = DivePlanner {
            dive_stage: DiveStage {
                dive_model,
                ..Default::default()
            },
            select_dive_model,
            ..Default::default()
        };

        // When
        dive_planner.dive_model_selected(select_dive_model.selected_dive_model.unwrap());

        // Then
        assert_eq!(expected_dive_model, dive_planner.dive_stage.dive_model);
    }

    #[test]
    fn update_dive_profile() {
        // Given
        let mut expected_cylinder = dive_stage_test_fixture().cylinder;
        expected_cylinder.gas_management.remaining = 960;
        let cylinder = dive_stage_test_fixture().cylinder;
        let selectable_cylinder = SelectableCylinder::Bottom;
        let expected_dive_planner = DivePlanner {
            select_cylinder: SelectCylinder {
                selected_cylinder: Some(selectable_cylinder),
                cylinders: [cylinder, Default::default(), Default::default()],
                ..Default::default()
            },
            dive_stage: dive_stage_test_fixture(),
            decompression_steps: DecompressionSteps {
                dive_steps: vec![
                    DiveStep { depth: 9, time: 2 },
                    DiveStep { depth: 6, time: 3 },
                    DiveStep { depth: 3, time: 7 },
                ],
            },
            ..Default::default()
        };
        let mut dive_planner = DivePlanner {
            select_cylinder: SelectCylinder {
                selected_cylinder: Some(selectable_cylinder),
                cylinders: [Default::default(), Default::default(), Default::default()],
                ..Default::default()
            },
            dive_stage: dive_stage_test_fixture(),
            ..Default::default()
        };

        // When
        dive_planner.update_dive_profile();

        // Then
        assert!(!dive_planner.dive_results.results.is_empty());
        assert_eq!(expected_cylinder, dive_planner.select_cylinder.cylinders[0]);
        assert_eq!(
            expected_dive_planner.decompression_steps,
            dive_planner.decompression_steps
        );
    }
}
