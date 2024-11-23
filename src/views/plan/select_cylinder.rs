use crate::{
    commands::{messages::Message, selectable_cylinder::SelectableCylinder},
    models::dive_planner::DivePlanner,
};
use iced::{
    widget::{button, column, pick_list, Column},
    Length,
};
use iced_aw::Card;

impl DivePlanner {
    pub fn select_cylinder_view<'a>(&self) -> Column<'a, Message> {
        if self.is_planning && self.select_cylinder.is_multiple_cylinder {
            let contents = column![
                pick_list(
                    &SelectableCylinder::ALL[..],
                    self.select_cylinder.selected_cylinder,
                    Message::CylinderSelected,
                )
                .width(Length::Fill)
                .placeholder("Select Cylinder"),
                button("Update Cylinder").width(Length::Fill).on_press(
                    Message::UpdateCylinderSelected(
                        self.select_cylinder.selected_cylinder.unwrap()
                    ),
                ),
            ]
            .spacing(10.0);

            return column!().push(Card::new("Select Cylinder", contents));
        } else if !self.is_planning && self.select_cylinder.is_multiple_cylinder {
            let contents = column![
                pick_list(
                    &SelectableCylinder::ALL[..],
                    self.select_cylinder.selected_cylinder,
                    Message::CylinderSelected,
                )
                .width(Length::Fill)
                .placeholder("Select Cylinder"),
            ]
            .spacing(10.0);
            
            return column!().push(Card::new("Select Cylinder", contents))
        } else {
            return column![];
        }
    }
}
