use crate::commands::{messages::Message, tab_identifier::TabIdentifier};
use crate::models::dive_planner::DivePlanner;
use iced::{
    widget::{column, Scrollable},
    Element,
};
use iced_aw::{TabBar, TabLabel};

impl DivePlanner {
    pub fn update(&mut self, message: Message) {
        match message {
            Message::MenuBar => {}
            Message::TabSelected(tab_identifier) => match tab_identifier {
                TabIdentifier::Plan => todo!(),
                TabIdentifier::Information => todo!(),
                TabIdentifier::Results => todo!(),
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
        // TODO create a view for the tab bar
        let tab_bar = TabBar::new(Message::TabSelected)
            .push(
                TabIdentifier::Plan,
                TabLabel::IconText('p', "Plan".to_string()),
            )
            .push(
                TabIdentifier::Information,
                TabLabel::IconText('i', "Information".to_string()),
            )
            .push(
                TabIdentifier::Results,
                TabLabel::IconText('r', "Results".to_string()),
            )
            .set_active_tab(&TabIdentifier::Plan);

        let contents = Scrollable::new(
            column!()
                .push(self.plan_view())
                .push(self.information_view())
                .push(self.results_view()),
        );

        let view = column!(self.menu_view(), tab_bar, contents);

        view.into()
    }
}
