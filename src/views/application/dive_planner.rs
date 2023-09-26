use crate::commands::messages::Message;
use crate::models::dive_profile::DiveProfile;
use crate::view_models::dive_planner::DivePlanner;
use crate::views::dive_results::results::ResultsView;
use crate::views::information::dive_information::DiveInformationView;
use crate::views::parameters::cylinder_parameters::cylinder::CylinderView;
use crate::views::parameters::cylinder_parameters::gas_management::GasManagementView;
use crate::views::parameters::cylinder_parameters::gas_mixture::GasMixtureView;
use crate::views::parameters::dive_stage::DiveStageView;
use crate::views::parameters::dive_step::DiveStepView;
use iced::widget::{column, scrollable};
use iced::{Element, Sandbox};
use iced_aw::Grid;

use super::menu_bar::MenuBarView;

impl Sandbox for DivePlanner {
    type Message = Message;

    fn new() -> Self {
        Self {
            select_dive_model: Default::default(),
            select_cylinder: Default::default(),
            dive_stage: Default::default(),
            dive_results: Default::default(),
            decompression_steps: Default::default(),
            cns_toxicity: Default::default(),
            redo_buffer: Default::default(),
        }
    }

    fn title(&self) -> String {
        String::from("Bubbles Dive Planner")
    }

    fn update(&mut self, message: Message) {
        match message {
            Message::MenuBar => {}
            Message::FileNew => self.file_new(),
            Message::FileSave => self.file_save(),
            Message::FileLoad => self.file_load(),
            Message::EditUndo => self.edit_undo(),
            Message::EditRedo => self.edit_redo(),
            Message::ViewToggleCentralNervousSystemToxicityVisibility => {
                self.view_toggle_central_nervous_system_toxicity_visibility();
            }
            Message::ViewToggleSelectCylinderVisibility => {
                self.view_toggle_select_cylinder_visibility();
            }
            Message::DiveModelSelected(selectable_dive_model) => {
                self.dive_model_selected(selectable_dive_model)
            }
            Message::DepthChanged(depth) => {
                self.dive_stage.dive_step.depth = DiveStepView::update_depth(depth)
            }
            Message::TimeChanged(time) => {
                self.dive_stage.dive_step.time = DiveStepView::update_time(time)
            }
            Message::CylinderVolumeChanged(cylinder_volume) => {
                self.dive_stage.cylinder =
                    CylinderView::update_cylinder_volume(cylinder_volume, self.dive_stage.cylinder)
            }
            Message::CylinderPressureChanged(cylinder_pressure) => {
                self.dive_stage.cylinder = CylinderView::update_cylinder_pressure(
                    cylinder_pressure,
                    self.dive_stage.cylinder,
                )
            }
            Message::SurfaceAirConsumptionChanged(surface_air_consumption) => {
                self.dive_stage
                    .cylinder
                    .gas_management
                    .surface_air_consumption_rate =
                    GasManagementView::update_surface_air_consumption_rate(surface_air_consumption)
            }
            Message::OxygenChanged(oxygen) => {
                self.dive_stage.cylinder.gas_mixture = GasMixtureView::update_oxygen(
                    oxygen,
                    self.dive_stage.cylinder.gas_mixture.helium,
                )
            }
            Message::HeliumChanged(helium) => {
                self.dive_stage.cylinder.gas_mixture = GasMixtureView::update_helium(
                    helium,
                    self.dive_stage.cylinder.gas_mixture.oxygen,
                )
            }
            Message::CylinderSelected(selectable_cylinder) => self
                .select_cylinder
                .on_cylinder_selected(selectable_cylinder, &mut self.dive_stage.cylinder),
            Message::UpdateCylinderSelected(selectable_cylinder) => self
                .select_cylinder
                .update_cylinder_selected(selectable_cylinder, self.dive_stage.cylinder),
            Message::UpdateDiveProfile => {
                self.update_dive_profile();
            }
            Message::RefreshDecompression => {
                self.refresh_decompression();
            }
            Message::DecompressionUpdateDiveProfile => {
                // TODO this is a repeat of the above method
                self.refresh_decompression();

                // TODO Refactor this into dive_planner
                for dive_step in &self.decompression_steps.dive_steps {
                    self.dive_stage.dive_step = *dive_step;

                    self.dive_stage = DiveProfile::update_dive_profile(self.dive_stage);

                    // TODO Refactor to using dive_planner.update_results()
                    self.dive_results.results.push(self.dive_stage);
                    self.redo_buffer = Default::default();
                }

                self.assign_decompression_steps();

                self.decompression_steps.update_visibility();
            }
        }
    }

    fn view(&self) -> Element<Message> {
        let menu_bar = MenuBarView::build_view(self);
        let dive_stage = DiveStageView::build_view(self);
        let dive_information = DiveInformationView::build_view(self);
        let results = ResultsView::build_view(self);

        column![]
            .push(Grid::with_columns(1).push(menu_bar.spacing(10).padding(10)))
            .push(
                Grid::with_columns(2)
                    .push(scrollable(dive_stage.width(300.0).spacing(10).padding(10)))
                    .push(scrollable(
                        column![dive_information.spacing(10), results.spacing(10.0)]
                            .spacing(10)
                            .padding(10),
                    )),
            )
            .into()
    }
}
