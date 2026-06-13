use crate::{
    application::messages::message::Message,
    models::{application::dive_planner::DivePlanner, plan::dive_step::DiveStep},
    views::file_picker::{save_file_location, select_file_to_load},
};
use iced::Task;

impl DivePlanner {
    #[allow(dead_code)]
    pub fn update(&mut self, message: Message) -> Task<Message> {
        match message {
            Message::MenuBar => Task::none(),
            Message::TabSelectionOnSelect(tab_identifier) => {
                self.switch_tab(tab_identifier);
                Task::none()
            }
            Message::FileOnNewClicked => {
                self.file_new();
                Task::none()
            }
            Message::FileOnSaveRequested => save_file_location().map(Message::FileOnSaveCompleted),
            Message::FileOnSaveCompleted(file_path) => {
                if let Some(file_path) = file_path {
                    self.file_save_application_state(&file_path);
                }
                Task::none()
            }
            Message::FileOnLoadRequested => select_file_to_load().map(Message::FileOnLoadCompleted),
            Message::FileOnLoadCompleted(file_path) => {
                if let Some(file_path) = file_path {
                    self.file_load(&file_path);
                }
                Task::none()
            }
            Message::EditOnUndoClicked => {
                self.edit_undo();
                self.dive_stage.decompression_steps =
                    self.dive_stage.calculate_decompression_dive_steps().into();

                Task::none()
            }
            Message::EditOnRedoClicked => {
                self.edit_redo();
                self.dive_stage.decompression_steps =
                    self.dive_stage.calculate_decompression_dive_steps().into();

                Task::none()
            }
            Message::ViewOnToggleThemeClicked => {
                self.switch_theme();
                Task::none()
            }
            Message::DiveModelSelectionOnSelect(selectable_dive_model) => {
                self.dive_model_selected(selectable_dive_model);
                Task::none()
            }
            Message::DepthOnChanged(depth) => {
                self.dive_stage.dive_step.depth = DiveStep::update_depth(depth);
                Task::none()
            }
            Message::TimeOnChanged(time) => {
                self.dive_stage.dive_step.time = DiveStep::update_time(time);
                Task::none()
            }
            Message::CylinderVolumeOnChanged(cylinder_volume) => {
                self.dive_stage.cylinder = self
                    .dive_stage
                    .cylinder
                    .update_cylinder_volume(cylinder_volume);
                Task::none()
            }
            Message::CylinderPressureOnChanged(cylinder_pressure) => {
                self.dive_stage.cylinder = self
                    .dive_stage
                    .cylinder
                    .update_cylinder_pressure(cylinder_pressure);
                Task::none()
            }
            Message::SurfaceAirConsumptionOnChanged(surface_air_consumption) => {
                self.dive_stage.cylinder.gas_management = self
                    .dive_stage
                    .cylinder
                    .gas_management
                    .update_surface_air_consumption_rate(surface_air_consumption);
                Task::none()
            }
            Message::OxygenOnChanged(oxygen) => {
                self.dive_stage.cylinder.gas_mixture =
                    self.dive_stage.cylinder.gas_mixture.update_oxygen(oxygen);
                Task::none()
            }
            Message::HeliumOnChanged(helium) => {
                self.dive_stage.cylinder.gas_mixture =
                    self.dive_stage.cylinder.gas_mixture.update_helium(helium);
                Task::none()
            }
            Message::DiveProfileOnClicked => {
                self.dive_planning.is_planning = false;
                self.dive_stage = DivePlanner::update_dive_profile(&self.dive_stage);
                self.dive_results.results.push(self.dive_stage.clone());
                self.dive_stage.decompression_steps =
                    self.dive_stage.calculate_decompression_dive_steps().into();

                Task::none()
            }
            Message::DecompressionProfileOnClicked => {
                let results = self.dive_stage.decompression_update_dive_profile();

                self.dive_stage = results.last().unwrap_or(&self.dive_stage).clone();
                self.dive_results.results.extend(results);
                Task::none()
            }
        }
    }
}

#[cfg(test)]
mod dive_planner_messages_should {
    use std::fs;

    use crate::{
        application::{
            messages::message::Message,
            states::{selectable_dive_model::SelectableDiveModel, tab_identifier::TabIdentifier},
        },
        controllers::files::test_file_guard::file_guard::TestFileGuard,
        models::{
            application::{application_state::ApplicationState, dive_planner::DivePlanner},
            plan::{
                cylinders::{
                    cylinder::Cylinder, gas_management::GasManagement, gas_mixture::GasMixture,
                },
                dive_model::DiveModel,
                dive_planning::dive_pre_planning::DivePrePlanning,
                dive_profile_result::{
                    ambient_pressure::AmbientPressure, dive_profile::DiveProfile,
                    tissue_pressure::TissuePressure,
                    tolerated_ambient_pressure::ToleratedAmbientPressure,
                    tolerated_surface_pressure::ToleratedSurfacePressure,
                },
                dive_stage::DiveStage,
                dive_step::DiveStep,
            },
            result::results::DiveResults,
        },
        test_fixture::{default_dive_stage_test_fixture_zhl16, dive_stage_test_fixture_zhl16},
    };

    #[test]
    fn test_menu_bar() {
        // given
        let mut dive_planner = DivePlanner::default();

        // when
        let tasks = dive_planner.update(Message::MenuBar);

        // then
        pretty_assertions::assert_eq!(0, tasks.units());
    }

    #[test]
    fn test_tab_selection_on_select() {
        // given
        let mut dive_planner = DivePlanner::default();

        // when
        let tasks = dive_planner.update(Message::TabSelectionOnSelect(TabIdentifier::Information));

        // then
        pretty_assertions::assert_eq!(0, tasks.units());
        pretty_assertions::assert_eq!(
            TabIdentifier::Information,
            dive_planner.application_state.tab_identifier
        )
    }

    #[test]
    fn test_file_on_new_clicked() {
        // given
        let mut dive_planner = DivePlanner::default();
        dive_planner.dive_stage = dive_stage_test_fixture_zhl16();
        let expected_dive_stage = DiveStage::default();

        // when
        let tasks = dive_planner.update(Message::FileOnNewClicked);

        // then
        pretty_assertions::assert_eq!(0, tasks.units());
        pretty_assertions::assert_eq!(expected_dive_stage, dive_planner.dive_stage)
    }

    #[test]
    #[ignore]
    fn acceptance_test_file_save_and_load_fails() {
        // given
        // const DIVE_PLANNER_STATE_FILE_NAME: &str = "dive_planner_state_2.toml";
        // let expected_dive_planner = DivePlanner {
        //     dive_planning: DivePrePlanning {
        //         is_planning: false,
        //         ..Default::default()
        //     },
        //     dive_stage: dive_stage_test_fixture_zhl16(),
        //     dive_results: DiveResults {
        //         results: vec![dive_stage_test_fixture_zhl16()],
        //     },
        //     ..Default::default()
        // };
        // let mut dive_planner = DivePlanner {
        //     dive_stage: dive_stage_test_fixture_zhl16(),
        //     dive_results: DiveResults {
        //         results: vec![dive_stage_test_fixture_zhl16()],
        //     },
        //     ..Default::default()
        // };

        let mut dive_planner = DivePlanner::default();

        // when
        // let _guard = TestFileGuard::new(DIVE_PLANNER_STATE_FILE_NAME);

        let tasks_1 = dive_planner.update(Message::FileOnSaveRequested);
        let tasks_2 = dive_planner.update(Message::FileOnSaveCompleted(None));
        let tasks_3 = dive_planner.update(Message::FileOnLoadRequested);
        let tasks_4 = dive_planner.update(Message::FileOnLoadCompleted(None));

        // then
        pretty_assertions::assert_eq!(1, tasks_1.units());
        pretty_assertions::assert_eq!(0, tasks_2.units());
        pretty_assertions::assert_eq!(1, tasks_3.units());
        pretty_assertions::assert_eq!(0, tasks_4.units());
        pretty_assertions::assert_eq!(DivePlanner::default(), dive_planner);
    }

    #[test]
    fn acceptance_test_file_save_and_load() {
        // given
        const DIVE_PLANNER_STATE_FILE_NAME: &str = "dive_planner_state_2.toml";
        let expected_dive_planner = DivePlanner {
            dive_planning: DivePrePlanning {
                is_planning: false,
                ..Default::default()
            },
            dive_stage: dive_stage_test_fixture_zhl16(),
            dive_results: DiveResults {
                results: vec![dive_stage_test_fixture_zhl16()],
            },
            ..Default::default()
        };
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage_test_fixture_zhl16(),
            dive_results: DiveResults {
                results: vec![dive_stage_test_fixture_zhl16()],
            },
            ..Default::default()
        };
        let _guard = TestFileGuard::new(DIVE_PLANNER_STATE_FILE_NAME);

        // when

        let tasks_1 = dive_planner.update(Message::FileOnSaveRequested);
        let tasks_2 = dive_planner.update(Message::FileOnSaveCompleted(Some(
            DIVE_PLANNER_STATE_FILE_NAME.to_string(),
        )));
        let tasks_3 = dive_planner.update(Message::FileOnLoadRequested);
        let tasks_4 = dive_planner.update(Message::FileOnLoadCompleted(Some(
            DIVE_PLANNER_STATE_FILE_NAME.to_string(),
        )));

        // then
        pretty_assertions::assert_eq!(1, tasks_1.units());
        pretty_assertions::assert_eq!(0, tasks_2.units());
        pretty_assertions::assert_eq!(1, tasks_3.units());
        pretty_assertions::assert_eq!(0, tasks_4.units());
        assert!(fs::metadata(DIVE_PLANNER_STATE_FILE_NAME).is_ok());
        pretty_assertions::assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn test_edit_on_undo_clicked() {
        // given
        let mut dive_stage = dive_stage_test_fixture_zhl16();
        dive_stage.decompression_steps = vec![DiveStep::new(6, 1), DiveStep::new(3, 4)].into();
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage.clone(),
            dive_results: DiveResults {
                results: vec![dive_stage.clone(), dive_stage.clone()],
            },
            application_state: ApplicationState {
                redo_buffer: vec![],
                ..Default::default()
            },
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage.clone(),
            dive_results: DiveResults {
                results: vec![dive_stage.clone()],
            },
            application_state: ApplicationState {
                redo_buffer: vec![dive_stage.clone()],
                ..Default::default()
            },
            ..Default::default()
        };

        // when
        let tasks = dive_planner.update(Message::EditOnUndoClicked);

        // then
        pretty_assertions::assert_eq!(0, tasks.units());
        pretty_assertions::assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn test_edit_on_redo_clicked() {
        // given
        let mut dive_stage = dive_stage_test_fixture_zhl16();
        dive_stage.decompression_steps = vec![DiveStep::new(6, 1), DiveStep::new(3, 4)].into();
        let mut dive_planner = DivePlanner {
            dive_stage: dive_stage.clone(),
            dive_results: DiveResults { results: vec![] },
            application_state: ApplicationState {
                redo_buffer: vec![dive_stage.clone(), dive_stage.clone()],
                ..Default::default()
            },
            ..Default::default()
        };
        let expected_dive_planner = DivePlanner {
            dive_stage: dive_stage.clone(),
            dive_results: DiveResults {
                results: vec![dive_stage.clone()],
            },
            application_state: ApplicationState {
                redo_buffer: vec![dive_stage.clone()],
                ..Default::default()
            },
            ..Default::default()
        };

        // when
        let tasks = dive_planner.update(Message::EditOnRedoClicked);

        // then
        pretty_assertions::assert_eq!(0, tasks.units());
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn test_view_on_toggle_theme_clicked() {
        let mut dive_planner = DivePlanner::default();

        // when
        let tasks = dive_planner.update(Message::ViewOnToggleThemeClicked);

        // then
        pretty_assertions::assert_eq!(0, tasks.units());
        pretty_assertions::assert_eq!(true, dive_planner.application_state.is_light_theme)
    }

    #[test]
    fn test_dive_model_selection_on_select() {
        // given
        let selected_dive_model = SelectableDiveModel::BulhmannZhl16;
        let mut dive_planner = DivePlanner::default();

        // when
        let tasks = dive_planner.update(Message::DiveModelSelectionOnSelect(selected_dive_model));

        // then
        pretty_assertions::assert_eq!(0, tasks.units());
        pretty_assertions::assert_eq!(
            default_dive_stage_test_fixture_zhl16().dive_model,
            dive_planner.dive_stage.dive_model
        )
    }

    #[test]
    fn test_depth_on_changed() {
        // given
        let depth = 12;
        let mut dive_planner = DivePlanner::default();

        // when
        let tasks = dive_planner.update(Message::DepthOnChanged(depth.to_string()));

        // then
        pretty_assertions::assert_eq!(0, tasks.units());
        pretty_assertions::assert_eq!(depth, dive_planner.dive_stage.dive_step.depth)
    }

    #[test]
    fn test_time_on_changed() {
        // given
        let time = 12;
        let mut dive_planner = DivePlanner::default();

        // when
        let tasks = dive_planner.update(Message::TimeOnChanged(time.to_string()));

        // then
        pretty_assertions::assert_eq!(0, tasks.units());
        pretty_assertions::assert_eq!(time, dive_planner.dive_stage.dive_step.time)
    }

    #[test]
    fn test_cylinder_volume_on_changed() {
        // given
        let cylinder_volume = 12;
        let mut dive_planner = DivePlanner::default();

        // when
        let tasks = dive_planner.update(Message::CylinderVolumeOnChanged(
            cylinder_volume.to_string(),
        ));

        // then
        pretty_assertions::assert_eq!(0, tasks.units());
        pretty_assertions::assert_eq!(
            cylinder_volume,
            dive_planner.dive_stage.cylinder.get_volume()
        )
    }

    #[test]
    fn test_cylinder_pressure_on_changed() {
        // given
        let cylinder_pressure = 200;
        let mut dive_planner = DivePlanner::default();

        // when
        let tasks = dive_planner.update(Message::CylinderPressureOnChanged(
            cylinder_pressure.to_string(),
        ));

        // then
        pretty_assertions::assert_eq!(0, tasks.units());
        pretty_assertions::assert_eq!(
            cylinder_pressure,
            dive_planner.dive_stage.cylinder.get_pressure()
        )
    }

    #[test]
    fn test_surface_air_consumption_on_changed() {
        // given
        let surface_air_consumption = 10;
        let mut dive_planner = DivePlanner::default();

        // when
        let tasks = dive_planner.update(Message::SurfaceAirConsumptionOnChanged(
            surface_air_consumption.to_string(),
        ));

        // then
        pretty_assertions::assert_eq!(0, tasks.units());
        pretty_assertions::assert_eq!(
            surface_air_consumption,
            dive_planner
                .dive_stage
                .cylinder
                .gas_management
                .get_surface_air_consumption_rate()
        )
    }

    #[test]
    fn test_oxygen_on_changed() {
        // given
        let oxygen = 10;
        let mut dive_planner = DivePlanner::default();

        // when
        let tasks = dive_planner.update(Message::OxygenOnChanged(oxygen.to_string()));

        // then
        pretty_assertions::assert_eq!(0, tasks.units());
        pretty_assertions::assert_eq!(oxygen, dive_planner.dive_stage.cylinder.gas_mixture.oxygen)
    }

    #[test]
    fn test_helium_on_changed() {
        // given
        let helium = 10;
        let mut dive_planner = DivePlanner::default();

        // when
        let tasks = dive_planner.update(Message::HeliumOnChanged(helium.to_string()));

        // then
        pretty_assertions::assert_eq!(0, tasks.units());
        pretty_assertions::assert_eq!(helium, dive_planner.dive_stage.cylinder.gas_mixture.helium)
    }

    #[test]
    fn test_dive_profile_on_clicked() {
        // given
        let mut dive_planner = DivePlanner::default();
        dive_planner.dive_stage = default_dive_stage_test_fixture_zhl16();
        let mut expected_dive_stage = dive_stage_test_fixture_zhl16();
        expected_dive_stage.decompression_steps =
            vec![DiveStep::new(6, 1), DiveStep::new(3, 4)].into();

        // when
        let task = dive_planner.update(Message::DiveProfileOnClicked);

        // then
        pretty_assertions::assert_eq!(0, task.units());
        pretty_assertions::assert_eq!(expected_dive_stage, dive_planner.dive_stage)
    }

    #[test]
    fn test_decompression_profile_on_clicked() {
        // given
        let dive_profile_1 = DiveProfile {
            number_of_compartments: 16,
            ambient_pressure: AmbientPressure::new(0.336, 0.16000001, 1.104),
            tissue_pressure: TissuePressure::new(
                vec![
                    3.1589808, 2.5964227, 2.15595, 1.8098811, 1.5372281, 1.3402663, 1.1903541,
                    1.0786185, 0.99708134, 0.94607913, 0.91261107, 0.88639325, 0.8658158,
                    0.8494663, 0.8366767, 0.82667166,
                ],
                vec![
                    0.43418437,
                    0.461714,
                    0.42061672,
                    0.3568913,
                    0.2867861,
                    0.22505449,
                    0.17152712,
                    0.12790523,
                    0.09402851,
                    0.07192881,
                    0.05705468,
                    0.045198355,
                    0.03575944,
                    0.028179422,
                    0.022200052,
                    0.017490864,
                ],
                vec![
                    3.5931652, 3.0581367, 2.5765667, 2.1667724, 1.8240142, 1.5653208, 1.3618813,
                    1.2065238, 1.0911099, 1.018008, 0.96966577, 0.93159163, 0.90157527, 0.87764573,
                    0.85887676, 0.8441625,
                ],
            ),
            tolerated_ambient_pressure: ToleratedAmbientPressure::new(
                vec![
                    1.128468, 1.2798394, 1.180643, 1.0529338, 0.8982601, 0.8096616, 0.7236567,
                    0.6523698, 0.5956443, 0.5757614, 0.5665247, 0.5629876, 0.57471126, 0.5682179,
                    0.5752636, 0.5841761,
                ],
                vec![
                    1.3146869, 1.057825, 0.91568786, 0.8039003, 0.70684034, 0.5974535, 0.52439874,
                    0.47122344, 0.433893, 0.39214367, 0.36050293, 0.33183846, 0.2942455,
                    0.28153113, 0.25914708, 0.23848496,
                ],
                vec![
                    0.4952727, 0.6398199, 0.71085435, 0.77258444, 0.8040468, 0.8365419, 0.86408573,
                    0.88721544, 0.90631306, 0.919946, 0.9300054, 0.93869895, 0.946292, 0.9532023,
                    0.95920485, 0.9645002,
                ],
            ),
            tolerated_surface_pressure: ToleratedSurfacePressure::new(
                vec![
                    3.3337765, 2.6207647, 2.3224459, 2.0982573, 1.9505491, 1.7928509, 1.6816914,
                    1.5983454, 1.5372645, 1.4791639, 1.4357656, 1.3971428, 1.3510017, 1.3306264,
                    1.3016772, 1.2752914,
                ],
                vec![
                    107.780624, 116.68871, 110.94195, 103.265335, 93.512856, 87.30904, 80.98283,
                    75.485794, 70.97736, 68.823204, 67.53649, 66.67834, 66.73384, 65.95734,
                    65.982315, 66.193695,
                ],
                2.798394,
            ),
        };
        let dive_stage_1 = DiveStage::new_with_decompression_steps(
            DiveModel::new_zhl16_dive_model_with_dive_profile(dive_profile_1),
            DiveStep::new(6, 1),
            Cylinder::new_with_gas_management(
                12,
                200,
                GasMixture::new(21, 10),
                GasManagement::new(1668, 12, 12),
            ),
            vec![DiveStep::new(3, 4)],
        );
        let dive_profile_2 = DiveProfile {
            number_of_compartments: 16,
            ambient_pressure: AmbientPressure::new(0.27299997, 0.13, 0.89699996),
            tissue_pressure: TissuePressure::new(
                vec![
                    2.0279903, 2.0986733, 1.905507, 1.6828265, 1.474747, 1.3093116, 1.1757512,
                    1.0721952, 0.9945677, 0.9451559, 0.9123813, 0.88651556, 0.866098, 0.84980303,
                    0.83701164, 0.8269781,
                ],
                vec![
                    0.17849603,
                    0.26244897,
                    0.2915141,
                    0.28260002,
                    0.24950118,
                    0.20849015,
                    0.16628107,
                    0.12809554,
                    0.09636959,
                    0.07477407,
                    0.059860345,
                    0.047761437,
                    0.03799878,
                    0.030076506,
                    0.023776203,
                    0.018782983,
                ],
                vec![
                    2.2064865, 2.3611224, 2.197021, 1.9654266, 1.7242482, 1.5178018, 1.3420323,
                    1.2002907, 1.0909373, 1.01993, 0.97224164, 0.934277, 0.9040968, 0.87987953,
                    0.86078787, 0.84576106,
                ],
            ),
            tolerated_ambient_pressure: ToleratedAmbientPressure::new(
                vec![
                    0.4542373, 0.84766227, 0.92075545, 0.9035298, 0.8213104, 0.7715893, 0.7069918,
                    0.6466952, 0.5950952, 0.5770495, 0.5684005, 0.5649761, 0.5765242, 0.5698186,
                    0.5766051, 0.58528227,
                ],
                vec![
                    1.295256, 1.0425721, 0.90559965, 0.7978403, 0.70364225, 0.5957831, 0.5239162,
                    0.47136542, 0.43427375, 0.39260775, 0.36100414, 0.33235043, 0.2947971,
                    0.2820371, 0.2596169, 0.23890057,
                ],
                vec![
                    0.49848786, 0.6428745, 0.7129783, 0.7738441, 0.8047282, 0.8368478, 0.86417043,
                    0.88719004, 0.9062407, 0.9198613, 0.9299175, 0.938613, 0.94620794, 0.953125,
                    0.9591366, 0.9644428,
                ],
            ),
            tolerated_surface_pressure: ToleratedSurfacePressure::new(
                vec![
                    3.301323, 2.5980859, 2.308167, 2.0900903, 1.9462979, 1.7907436, 1.6810954,
                    1.5985197, 1.5377333, 1.4797282, 1.4363683, 1.3977523, 1.3516473, 1.3312174,
                    1.3022213, 1.2757686,
                ],
                vec![
                    66.83643, 90.8793, 95.18467, 94.03549, 88.59118, 84.75819, 79.830826, 75.08764,
                    70.9445, 68.92685, 67.687485, 66.841385, 66.88852, 66.095856, 66.1015,
                    66.294235,
                ],
                -0.79244554,
            ),
        };
        let dive_stage_2 = DiveStage::new_with_decompression_steps(
            DiveModel::new_zhl16_dive_model_with_dive_profile(dive_profile_2),
            DiveStep::new(3, 4),
            Cylinder::new_with_gas_management(
                12,
                200,
                GasMixture::new(21, 10),
                GasManagement::new(1620, 48, 12),
            ),
            vec![],
        );
        let expected_dive_results: DiveResults = DiveResults {
            results: vec![dive_stage_1, dive_stage_2],
        };
        let mut dive_planner = DivePlanner::default();
        dive_planner.dive_stage = dive_stage_test_fixture_zhl16();

        // when
        let tasks = dive_planner.update(Message::DecompressionProfileOnClicked);

        // then
        pretty_assertions::assert_eq!(0, tasks.units());
        pretty_assertions::assert_eq!(expected_dive_results, dive_planner.dive_results);
    }
}
