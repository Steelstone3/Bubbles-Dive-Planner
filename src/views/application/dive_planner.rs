use crate::commands::{messages::Message, tab_identifier::TabIdentifier};
use crate::models::dive_planner::DivePlanner;
use iced::Element;

impl DivePlanner {
    pub fn update(&mut self, message: Message) {
        match message {
            Message::MenuBar => {}
            Message::TabSelected(tab_identifier) => match tab_identifier {
                TabIdentifier::Plan => {
                    self.tab_identifier = TabIdentifier::Plan;
                }
                TabIdentifier::Information => {
                    self.tab_identifier = TabIdentifier::Information;
                }
                TabIdentifier::Results => {
                    self.tab_identifier = TabIdentifier::Results;
                }
            },
            Message::FileNew => self.file_new(),
            Message::FileSave => self.file_save(),
            Message::FileLoad => self.file_load(),
            Message::EditUndo => self.edit_undo(),
            Message::EditRedo => self.edit_redo(),
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
                self.cylinder_selected(selectable_cylinder);
            }
            Message::UpdateCylinderSelected(selectable_cylinder) => {
                self.update_cylinder_selected(selectable_cylinder)
            }
            Message::UpdateDiveProfile => {
                self.update_dive_profile();
            }
            Message::DecompressionUpdateDiveProfile => {
                self.decompression_update_dive_profile();
            }
        }
    }

    pub fn view(&self) -> Element<Message> {
        self.tab_bar_view().into()
    }
}
