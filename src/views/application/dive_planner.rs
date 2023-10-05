use crate::commands::messages::Message;
use crate::view_models::dive_planner::DivePlanner;
use crate::views::dive_results::results::ResultsView;
use crate::views::information::dive_information::DiveInformationView;
use crate::views::parameters::dive_stage::DiveStageView;
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
            Message::DepthChanged(depth) => self.dive_stage.dive_step.update_depth(depth),
            Message::TimeChanged(time) => self.dive_stage.dive_step.update_time(time),
            Message::CylinderVolumeChanged(cylinder_volume) => {
                self.dive_stage
                    .cylinder
                    .update_cylinder_volume(cylinder_volume);
            }
            Message::CylinderPressureChanged(cylinder_pressure) => {
                self.dive_stage
                    .cylinder
                    .update_cylinder_pressure(cylinder_pressure);
            }
            Message::SurfaceAirConsumptionChanged(surface_air_consumption) => self
                .dive_stage
                .cylinder
                .gas_management
                .update_surface_air_consumption_rate(surface_air_consumption),
            Message::OxygenChanged(oxygen) => {
                self.dive_stage.cylinder.gas_mixture.update_oxygen(oxygen)
            }
            Message::HeliumChanged(helium) => {
                self.dive_stage.cylinder.gas_mixture.update_helium(helium)
            }
            Message::CylinderSelected(selectable_cylinder) => {
                self.cylinder_selected(selectable_cylinder)
            }
            Message::UpdateCylinderSelected(selectable_cylinder) => {
                self.update_cylinder_selected(selectable_cylinder)
            }
            Message::UpdateDiveProfile => {
                self.update_dive_profile();
            }
            Message::RefreshDecompression => {
                self.refresh_decompression();
            }
            Message::DecompressionUpdateDiveProfile => {
                self.decompression_update_dive_profile();
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
