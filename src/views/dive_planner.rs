use super::dive_results::results::ResultsView;
use super::information::dive_information::DiveInformationView;
use super::menu_bar::MenuBarView;
use super::parameters::cylinder_parameters::cylinder::CylinderView;
use super::parameters::cylinder_parameters::gas_management::GasManagementView;
use super::parameters::cylinder_parameters::gas_mixture::GasMixtureView;
use super::parameters::dive_stage::DiveStageView;
use super::parameters::dive_step::DiveStepView;
use crate::commands::messages::Message;
use crate::commands::selectable_dive_model::SelectableDiveModel;
use crate::controllers::file::{read_dive_stage, upsert_dive_stage};
use crate::models::dive_model::DiveModel;
use crate::models::dive_profile::DiveProfile;
use crate::{models::dive_stage::DiveStage, view_models::dive_planner::DivePlanner};
use iced::widget::{column, row, scrollable};
use iced::{Element, Sandbox};
use iced_aw::Grid;

impl Sandbox for DivePlanner {
    type Message = Message;

    fn new() -> Self {
        Self {
            select_dive_model: Default::default(),
            select_cylinder: Default::default(),
            dive_stage: DiveStage::default(),
            results: Default::default(),
            redo_buffer: Default::default(),
            cns_toxicity: Default::default(),
        }
    }

    fn title(&self) -> String {
        String::from("Bubbles Dive Planner")
    }

    fn update(&mut self, message: Message) {
        match message {
            Message::MenuBar => {}
            Message::FileNew => self.reset(),
            Message::FileSave => upsert_dive_stage("dive_plan.json", self),
            Message::FileLoad => *self = read_dive_stage("dive_plan.json"),
            Message::EditUndo => self.undo(),
            Message::EditRedo => self.redo(),
            Message::ViewToggleCentralNervousSystemToxicityVisibility => {
                self.cns_toxicity.is_visible = self.cns_toxicity.toggle_visibility();
            }
            Message::DiveModelSelected(selectable_dive_model) => {
                self.select_dive_model.selected_dive_model = Some(selectable_dive_model);

                match selectable_dive_model {
                    SelectableDiveModel::Bulhmann => {
                        self.dive_stage.dive_model = DiveModel::create_zhl16_dive_model()
                    }
                    SelectableDiveModel::Usn => {
                        self.dive_stage.dive_model = DiveModel::create_usn_rev_6_model()
                    }
                }
            }
            Message::DepthChanged(depth) => {
                self.dive_stage.dive_step.depth = DiveStepView::update_depth(depth);
            }
            Message::TimeChanged(time) => {
                self.dive_stage.dive_step.time = DiveStepView::update_time(time);
            }
            Message::CylinderVolumeChanged(cylinder_volume) => {
                self.dive_stage.cylinder =
                    CylinderView::update_cylinder_volume(cylinder_volume, self.dive_stage.cylinder);
            }
            Message::CylinderPressureChanged(cylinder_pressure) => {
                self.dive_stage.cylinder = CylinderView::update_cylinder_pressure(
                    cylinder_pressure,
                    self.dive_stage.cylinder,
                );
            }
            Message::SurfaceAirConsumptionChanged(surface_air_consumption) => {
                self.dive_stage
                    .cylinder
                    .gas_management
                    .surface_air_consumption_rate =
                    GasManagementView::update_surface_air_consumption_rate(surface_air_consumption);
            }
            Message::OxygenChanged(oxygen) => {
                self.dive_stage.cylinder.gas_mixture = GasMixtureView::update_oxygen(
                    oxygen,
                    self.dive_stage.cylinder.gas_mixture.helium,
                );
            }
            Message::HeliumChanged(helium) => {
                self.dive_stage.cylinder.gas_mixture = GasMixtureView::update_helium(
                    helium,
                    self.dive_stage.cylinder.gas_mixture.oxygen,
                );
            }
            Message::AddCylinder => self.select_cylinder.add_cylinder(self.dive_stage.cylinder),
            Message::UpdateDiveProfile => {
                self.dive_stage = DiveProfile::update_dive_profile(self.dive_stage);
                self.add_result();
            }
            // Message::SelectCylinder(index, cylinder) => {
            //     self.dive_stage.cylinder = cylinder;
            // } 
        }
    }

    fn view(&self) -> Element<Message> {
        let menu_bar = MenuBarView::new(self);
        let dive_stage = DiveStageView::new(self);
        let dive_information = DiveInformationView::new(self);
        let results = ResultsView::new(&self.results);

        let dive_stage_view = DiveStageView::determine_view(
            self,
            dive_stage.select_dive_model,
            dive_stage.dive_step,
            dive_stage.cylinder,
            dive_stage.cylinder_read_only,
        );

        column![]
            .push(
                Grid::with_columns(1).push(
                    row!(menu_bar.file, menu_bar.edit, menu_bar.view)
                        .spacing(10)
                        .padding(10),
                ),
            )
            .push(
                Grid::with_columns(2)
                    .push(scrollable(
                        column![dive_stage_view.spacing(10),]
                            .width(300)
                            .spacing(10)
                            .padding(10),
                    ))
                    .push(scrollable(
                        column![
                            results.result_title_text,
                            dive_information.dive_information_text,
                            results.results_text.spacing(10)
                        ]
                        .spacing(10)
                        .padding(10),
                    )),
            )
            .into()
    }
}
