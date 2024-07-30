// TODO AH remove
// use crate::{
//     commands::{messages::Message, selectable_cylinder::SelectableCylinder},
//     models::select_cylinder::SelectCylinder,
// };
// use iced::{
//     widget::{button, column, pick_list, text, Button, Column, PickList, Text},
//     Length,
// };

// pub struct SelectCylinderView<'a> {
//     update_cylinder: Button<'a, Message>,
//     cylinder_read_only_text_title: Text<'a>,
//     selectable_cylinder: PickList<'a, SelectableCylinder, Message>,
// }

// impl SelectCylinderView<'_> {
//     pub fn build_view<'a>(
//         is_visible: bool,
//         is_read_only: bool,
//         select_cylinder: &SelectCylinder,
//     ) -> Column<'a, Message> {
//         let select_cylinder = SelectCylinderView::new(select_cylinder);

//         if is_visible && !is_read_only {
//             return column![
//                 select_cylinder.update_cylinder,
//                 select_cylinder.cylinder_read_only_text_title,
//                 select_cylinder.selectable_cylinder,
//             ]
//             .spacing(10.0);
//         } else if is_visible && is_read_only {
//             return column![
//                 select_cylinder.cylinder_read_only_text_title,
//                 select_cylinder.selectable_cylinder,
//             ]
//             .spacing(10.0);
//         } else {
//             return column![];
//         }
//     }

//     fn new(select_cylinder: &SelectCylinder) -> Self {
//         Self {
//             update_cylinder: button("Update Cylinder").width(Length::Fill).on_press(
//                 Message::UpdateCylinderSelected(select_cylinder.selected_cylinder.unwrap()),
//             ),
//             cylinder_read_only_text_title: text("Select Cylinder"),
//             selectable_cylinder: pick_list(
//                 &SelectableCylinder::ALL[..],
//                 select_cylinder.selected_cylinder,
//                 Message::CylinderSelected,
//             )
//             .width(Length::Fill)
//             .placeholder("Select Cylinder"),
//         }
//     }
// }
