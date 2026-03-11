use iced::Task;

use crate::{
    application::messages::message::Message,
    controllers::files::file_picker::{save_file_location, select_file_to_load},
    models::application::dive_planner::DivePlanner,
};

impl DivePlanner {
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
            Message::FileOnSaveRequested => Task::none(),
            Message::FileOnSaveCompleted => {
                self.file_save_application_state(&save_file_location());
                Task::none()
            }
            Message::FileOnSaveResultsRequested => Task::none(),
            Message::FileOnSaveResultsCompleted => {
                self.file_save_results(&save_file_location());
                Task::none()
            }
            Message::FileOnLoadRequested => Task::none(),
            Message::FileOnLoadCompleted => {
                self.file_load(&select_file_to_load());
                Task::none()
            }
            Message::EditOnUndoClicked => {
                self.edit_undo();
                Task::none()
            }
            Message::EditOnRedoClicked => {
                self.edit_redo();
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
                self.dive_stage.dive_step.update_depth(depth);
                Task::none()
            }
            Message::TimeOnChanged(time) => {
                self.dive_stage.dive_step.update_time(time);
                Task::none()
            }
            Message::CylinderVolumeOnChanged(cylinder_volume) => {
                self.dive_stage
                    .cylinder
                    .update_cylinder_volume(cylinder_volume);
                Task::none()
            }
            Message::CylinderPressureOnChanged(cylinder_pressure) => {
                self.dive_stage
                    .cylinder
                    .update_cylinder_pressure(cylinder_pressure);
                Task::none()
            }
            Message::SurfaceAirConsumptionOnChanged(surface_air_consumption) => {
                self.dive_stage
                    .cylinder
                    .gas_management
                    .update_surface_air_consumption_rate(surface_air_consumption);
                Task::none()
            }
            Message::OxygenOnChanged(oxygen) => {
                self.dive_stage.cylinder.gas_mixture.update_oxygen(oxygen);
                Task::none()
            }
            Message::HeliumOnChanged(helium) => {
                self.dive_stage.cylinder.gas_mixture.update_helium(helium);
                Task::none()
            }
            Message::DiveProfileOnClicked => {
                self.update_dive_profile();
                Task::none()
            }
            Message::DecompressionProfileOnClicked => {
                self.decompression_update_dive_profile();
                Task::none()
            }
        }
    }
}
