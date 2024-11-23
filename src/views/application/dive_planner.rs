use crate::{
    commands::{messages::Message, tab_identifier::TabIdentifier},
    models::application::dive_planner::DivePlanner,
};
use iced::{Element, Theme};

impl DivePlanner {
    pub fn update(&mut self, message: Message) {
        match message {
            Message::MenuBar => {}
            Message::SelectedTabChanged(tab_identifier) => match tab_identifier {
                TabIdentifier::Plan => {
                    self.application_state.tab_identifier = TabIdentifier::Plan;
                }
                TabIdentifier::Information => {
                    self.application_state.tab_identifier = TabIdentifier::Information;
                }
                TabIdentifier::Results => {
                    self.application_state.tab_identifier = TabIdentifier::Results;
                }
            },
            Message::FileNew => self.file_new(),
            Message::FileSave => self.file_save(),
            Message::FileLoad => self.file_load(),
            Message::EditUndo => self.edit_undo(),
            Message::EditRedo => self.edit_redo(),
            Message::ViewToggleTheme => match self.application_state.is_light_theme {
                true => self.application_state.is_light_theme = false,
                false => self.application_state.is_light_theme = true,
            },
            Message::ViewToggleSelectedCylinderVisibility => {
                self.view_toggle_select_cylinder_visibility();
            }
            Message::SelectedDiveModelChanged(selectable_dive_model) => {
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
            Message::SelectedCylinderChanged(selectable_cylinder) => {
                self.cylinder_selected(selectable_cylinder);
            }
            Message::UpdateSelectedCylinder(selectable_cylinder) => {
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

    pub fn theme(&self) -> Theme {
        // Theme::GruvboxDark
        match self.application_state.is_light_theme {
            true => Theme::Light,
            false => Theme::Dark,
        }
    }
}
