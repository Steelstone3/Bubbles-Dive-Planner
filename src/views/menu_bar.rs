use crate::commands::messages::Message;
use iced::widget::button;
use iced::Renderer;
use iced_aw::{MenuBar, MenuTree};

pub struct MenuBarView<'a> {
    pub file: MenuBar<'a, Message, Renderer>,
    pub edit: MenuBar<'a, Message, Renderer>,
    pub view: MenuBar<'a, Message, Renderer>,
}

impl Default for MenuBarView<'_> {
    fn default() -> Self {
        Self {
            file: MenuBar::new(vec![MenuTree::with_children(
                button("File").on_press(Message::MenuBar),
                vec![
                    MenuTree::new(button("New").on_press(Message::FileNew)),
                    MenuTree::new(button("Save").on_press(Message::FileSave)),
                    MenuTree::new(button("Load").on_press(Message::FileLoad)),
                ],
            )
            .width(100)]),
            edit: MenuBar::new(vec![MenuTree::with_children(
                button("Edit").on_press(Message::MenuBar),
                vec![
                    MenuTree::new(button("Undo").on_press(Message::EditUndo)),
                    MenuTree::new(button("Redo").on_press(Message::EditRedo)),
                ],
            )
            .width(100)]),
            view: MenuBar::new(vec![MenuTree::with_children(
                button("View").on_press(Message::MenuBar),
                vec![MenuTree::new(
                    button("CNS Table").on_press(Message::ViewCns),
                )],
            )
            .width(100)]),
        }
    }
}
