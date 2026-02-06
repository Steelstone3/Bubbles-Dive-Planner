use crate::{
    application::messages::message::Message,
    controllers::files::file_picker::{save_file_location, select_file_to_load},
    models::application::dive_planner::DivePlanner,
};

impl DivePlanner {
    pub fn update(&mut self, message: Message) {
        match message {
            Message::MenuBar => {}
            Message::TabSelectionOnSelect(tab_identifier) => self.switch_tab(tab_identifier),
            Message::FileOnNewClicked => self.file_new(),
            Message::FileOnSaveClicked => self.file_save_application_state(&save_file_location()),
            Message::FileOnSaveResultsClicked => self.file_save_results(&save_file_location()),
            Message::FileOnLoadClicked => self.file_load(&select_file_to_load()),
            Message::EditOnUndoClicked => self.edit_undo(),
            Message::EditOnRedoClicked => self.edit_redo(),
            Message::ViewOnToggleThemeClicked => self.switch_theme(),
            Message::DiveModelSelectionOnSelect(selectable_dive_model) => {
                self.dive_model_selected(selectable_dive_model)
            }
            Message::DepthOnChanged(depth) => self.dive_stage.dive_step.update_depth(depth),
            Message::TimeOnChanged(time) => self.dive_stage.dive_step.update_time(time),
            Message::CylinderVolumeOnChanged(cylinder_volume) => {
                self.dive_stage
                    .cylinder
                    .update_cylinder_volume(cylinder_volume);
            }
            Message::CylinderPressureOnChanged(cylinder_pressure) => {
                self.dive_stage
                    .cylinder
                    .update_cylinder_pressure(cylinder_pressure);
            }
            Message::SurfaceAirConsumptionOnChanged(surface_air_consumption) => self
                .dive_stage
                .cylinder
                .gas_management
                .update_surface_air_consumption_rate(surface_air_consumption),
            Message::OxygenOnChanged(oxygen) => {
                self.dive_stage.cylinder.gas_mixture.update_oxygen(oxygen)
            }
            Message::HeliumOnChanged(helium) => {
                self.dive_stage.cylinder.gas_mixture.update_helium(helium)
            }
            Message::DiveProfileOnClicked => {
                self.update_dive_profile();
            }
            Message::DecompressionProfileOnClicked => {
                self.decompression_update_dive_profile();
            }
        }
    }
}
