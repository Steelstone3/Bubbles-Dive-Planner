use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner};
use iced::widget::{button, column};
use iced_aw::menu::{Item, MenuBar};

impl DivePlanner {
    pub fn menu_view(&self) -> iced::widget::Column<Message> {
        column!(MenuBar::new(vec![Item::new(
            button("New").on_press(Message::FileNew)
        )]))
    }

    // MenuBarView {
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
}
