use crate::{
    commands::{messages::Message, selectable_cylinder::SelectableCylinder},
    view_models::dive_planner::DivePlanner,
};
use iced::{
    widget::{button, column, pick_list, text, Column},
    Length,
};

impl DivePlanner {
    pub fn select_cylinder_view<'a>(&self) -> Column<'a, Message> {
        if self.is_planning {
            return column![
                button("Update Cylinder").width(Length::Fill).on_press(
                    Message::UpdateCylinderSelected(
                        self.select_cylinder.selected_cylinder.unwrap()
                    ),
                ),
                text("Select Cylinder"),
                pick_list(
                    &SelectableCylinder::ALL[..],
                    self.select_cylinder.selected_cylinder,
                    Message::CylinderSelected,
                )
                .width(Length::Fill)
                .placeholder("Select Cylinder"),
            ]
            .spacing(10.0);
        } else if !self.is_planning {
            return column![
                text("Select Cylinder"),
                pick_list(
                    &SelectableCylinder::ALL[..],
                    self.select_cylinder.selected_cylinder,
                    Message::CylinderSelected,
                )
                .width(Length::Fill)
                .placeholder("Select Cylinder"),
            ]
            .spacing(10.0);
        } else {
            return column![];
        }
    }
}
