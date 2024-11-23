use crate::{
    commands::selectable_cylinder::SelectableCylinder,
    models::application::dive_planner::DivePlanner,
};

impl DivePlanner {
    pub fn view_toggle_select_cylinder_visibility(&mut self) {
        self.select_cylinder.toggle_visibility();
    }

    pub fn cylinder_selected(&mut self, selectable_cylinder: SelectableCylinder) {
        self.select_cylinder
            .on_cylinder_selected(selectable_cylinder, &mut self.dive_stage.cylinder);

        // Refresh decompression steps
        self.decompression_steps
            .assign_decompression_steps(self.dive_stage.calculate_decompression_dive_steps());
    }

    pub fn update_cylinder_selected(&mut self, selectable_cylinder: SelectableCylinder) {
        self.select_cylinder
            .update_cylinder_selected(selectable_cylinder, self.dive_stage.cylinder);
    }
}

#[cfg(test)]
mod cylinder_should {
    use crate::{
        commands::selectable_cylinder::SelectableCylinder,
        models::{
            application::{dive_planner::DivePlanner, select_cylinder::SelectCylinder},
            information::{decompression_steps::DecompressionSteps, gas_management::GasManagement},
            plan::{
                cylinder::Cylinder, dive_stage::DiveStage, dive_step::DiveStep,
                gas_mixture::GasMixture,
            },
        },
        test_fixture::dive_stage_test_fixture,
    };
    use rstest::rstest;

    #[rstest]
    #[case(false, true)]
    #[case(true, false)]
    fn toggle_select_cylinder_visibility(
        #[case] is_multiple_cylinder: bool,
        #[case] expected_is_visible: bool,
    ) {
        // Given
        let select_cylinder = SelectCylinder {
            is_multiple_cylinder,
            ..Default::default()
        };
        let mut dive_planner = DivePlanner {
            select_cylinder,
            ..Default::default()
        };

        // When
        dive_planner.view_toggle_select_cylinder_visibility();

        // Then
        assert_eq!(
            expected_is_visible,
            dive_planner.select_cylinder.is_multiple_cylinder
        );
    }

    #[rstest]
    #[case(SelectableCylinder::Bottom, 0)]
    #[case(SelectableCylinder::Decompression, 1)]
    #[case(SelectableCylinder::Descend, 2)]
    fn update_the_selected_cylinder(
        #[case] selectable_cylinder: SelectableCylinder,
        #[case] index: usize,
    ) {
        // Given
        let select_cylinder = SelectCylinder {
            cylinders: Default::default(),
            selected_cylinder: Some(selectable_cylinder),
            is_multiple_cylinder: true,
        };
        let cylinder = Cylinder {
            volume: 12,
            pressure: 200,
            initial_pressurised_cylinder_volume: 2400,
            gas_mixture: GasMixture {
                oxygen: 21,
                helium: 10,
                nitrogen: 69,
                maximum_operating_depth: 56.67,
            },
            gas_management: GasManagement {
                remaining: 1680,
                used: 720,
                surface_air_consumption_rate: 12,
            },
        };
        let mut dive_planner = DivePlanner {
            select_cylinder,
            dive_stage: DiveStage {
                cylinder,
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        dive_planner.update_cylinder_selected(selectable_cylinder);

        // Then
        assert_eq!(cylinder, dive_planner.select_cylinder.cylinders[index]);
    }

    #[rstest]
    #[case(SelectableCylinder::Bottom)]
    #[case(SelectableCylinder::Decompression)]
    #[case(SelectableCylinder::Descend)]
    fn select_a_cylinder(#[case] selectable_cylinder: SelectableCylinder) {
        // Given
        let expected_decompression_steps = DecompressionSteps {
            dive_steps: vec![
                DiveStep { depth: 6, time: 1 },
                DiveStep { depth: 3, time: 3 },
            ],
        };
        let expected_cylinder = Cylinder {
            volume: 12,
            pressure: 200,
            initial_pressurised_cylinder_volume: 2400,
            gas_mixture: GasMixture {
                oxygen: 21,
                helium: 10,
                nitrogen: 69,
                maximum_operating_depth: 56.67,
            },
            gas_management: GasManagement {
                remaining: 1680,
                used: 720,
                surface_air_consumption_rate: 12,
            },
        };
        let select_cylinder = SelectCylinder {
            cylinders: [expected_cylinder, expected_cylinder, expected_cylinder],
            selected_cylinder: Some(selectable_cylinder),
            is_multiple_cylinder: true,
        };
        let dive_stage = dive_stage_test_fixture();
        let mut dive_planner = DivePlanner {
            select_cylinder,
            dive_stage,
            ..Default::default()
        };

        // When
        dive_planner.cylinder_selected(selectable_cylinder);

        // Then
        assert_eq!(expected_cylinder, dive_planner.dive_stage.cylinder);
        assert_eq!(
            expected_decompression_steps,
            dive_planner.decompression_steps
        );
    }
}
