use crate::{
    commands::{messages::Message, selectable_cylinder::SelectableCylinder},
    models::application::dive_planner::DivePlanner,
};
use iced::{
    Length,
    widget::{Column, button, column, pick_list},
};
use iced_aw::Card;

impl DivePlanner {
    pub fn select_cylinder_view<'a>(&self) -> Column<'a, Message> {
        if self.application_state.is_planning && self.select_cylinder.is_multiple_cylinder {
            let contents = column![
                pick_list(
                    &SelectableCylinder::ALL[..],
                    self.select_cylinder.selected_cylinder,
                    Message::SelectedCylinderChanged,
                )
                .width(Length::Fill)
                .placeholder("Select Cylinder"),
                button("Update Cylinder").width(Length::Fill).on_press(
                    Message::UpdateSelectedCylinder(
                        self.select_cylinder.selected_cylinder.unwrap_or_default()
                    ),
                ),
            ]
            .spacing(10.0);

            column!().push(Card::new("Select Cylinder", contents))
        } else if !self.application_state.is_planning && self.select_cylinder.is_multiple_cylinder {
            let contents = column![
                pick_list(
                    &SelectableCylinder::ALL[..],
                    self.select_cylinder.selected_cylinder,
                    Message::SelectedCylinderChanged,
                )
                .width(Length::Fill)
                .placeholder("Select Cylinder"),
            ]
            .spacing(10.0);

            column!().push(Card::new("Select Cylinder", contents))
        } else {
            column!()
        }
    }
}
