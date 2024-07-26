// use crate::commands::messages::Message;
// use crate::view_models::dive_planner::DivePlanner;
// use iced::widget::{button, row, Button, Row};
// use iced::Renderer;
// use iced_aw::{MenuBar, MenuTree};

// pub struct MenuBarView<'a> {
//     file: MenuBar<'a, Message, Renderer>,
//     edit: MenuBar<'a, Message, Renderer>,
//     view: MenuBar<'a, Message, Renderer>,
// }

// impl MenuBarView<'_> {
//     pub fn build_view<'a>(dive_planner: &DivePlanner) -> Row<'a, Message> {
//         let menu_bar = MenuBarView::new(dive_planner);

//         row![menu_bar.file, menu_bar.edit, menu_bar.view]
//             .padding(10.0)
//             .spacing(10.0)
//     }

//     fn new<'a>(dive_planner: &DivePlanner) -> MenuBarView<'a> {
//         MenuBarView {
//             file: MenuBar::new(vec![MenuTree::with_children(
//                 button("File").on_press(Message::MenuBar),
//                 vec![
//                     MenuTree::new(button("New").on_press(Message::FileNew)),
//                     MenuTree::new(button("Save").on_press(Message::FileSave)),
//                     MenuTree::new(button("Load").on_press(Message::FileLoad)),
//                 ],
//             )
//             .width(100)]),
//             edit: MenuBar::new(vec![MenuTree::with_children(
//                 button("Edit").on_press(Message::MenuBar),
//                 vec![
//                     MenuTree::new(Self::undo_button(dive_planner)),
//                     MenuTree::new(Self::redo_button(dive_planner)),
//                 ],
//             )
//             .width(100)]),
//             view: MenuBar::new(vec![MenuTree::with_children(
//                 button("View").on_press(Message::MenuBar),
//                 vec![
//                     MenuTree::new(
//                         button("Cylinders").on_press(Message::ViewToggleSelectCylinderVisibility),
//                     ),
//                     MenuTree::new(
//                         button("CNS Table")
//                             .on_press(Message::ViewToggleCentralNervousSystemToxicityVisibility),
//                     ),
//                 ],
//             )
//             .width(100)]),
//         }
//     }

//     fn undo_button<'a>(dive_planner: &DivePlanner) -> Button<'a, Message, Renderer> {
//         if dive_planner.is_undoable() {
//             button("Undo").on_press(Message::EditUndo)
//         } else {
//             button("Undo")
//         }
//     }

//     fn redo_button<'a>(dive_planner: &DivePlanner) -> Button<'a, Message, Renderer> {
//         if dive_planner.is_redoable() {
//             button("Redo").on_press(Message::EditRedo)
//         } else {
//             button("Redo")
//         }
//     }
// }
