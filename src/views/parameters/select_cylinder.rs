use crate::{
    commands::{messages::Message, selectable_cylinder::SelectableCylinder},
    view_models::dive_planner::DivePlanner,
};
use iced::{
    widget::{button, pick_list, text, Button, PickList, Text},
    Length,
};

pub struct SelectCylinderView<'a> {
    pub update_cylinder: Button<'a, Message>,
    pub cylinder_read_only_text_title: Text<'a>,
    pub selectable_cylinder: PickList<'a, SelectableCylinder, Message>,
}

impl SelectCylinderView<'_> {
    // TODO add a build view
    pub fn new(dive_planner: &DivePlanner) -> Self {
        Self {
            update_cylinder: button("Update Cylinder").width(Length::Fill).on_press(
                Message::UpdateCylinderSelected(
                    dive_planner.select_cylinder.selected_cylinder.unwrap(),
                ),
            ),
            cylinder_read_only_text_title: text("Select Cylinder"),
            selectable_cylinder: pick_list(
                &SelectableCylinder::ALL[..],
                dive_planner.select_cylinder.selected_cylinder,
                Message::CylinderSelected,
            )
            .width(Length::Fill)
            .placeholder("Select Cylinder"),
        }
    }
}
