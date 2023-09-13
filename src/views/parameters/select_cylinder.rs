use crate::{
    commands::{messages::Message, selectable_cylinder::SelectableCylinder},
    view_models::dive_planner::DivePlanner,
};
use iced::{
    widget::{pick_list, text, PickList, Text},
    Length,
};

pub struct SelectCylinderView<'a> {
    pub cylinder_read_only_text_title: Text<'a>,
    pub selectable_cylinder: PickList<'a, SelectableCylinder, Message>,
}

impl SelectCylinderView<'_> {
    pub fn new(dive_planner: &DivePlanner) -> Self {
        Self {
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
