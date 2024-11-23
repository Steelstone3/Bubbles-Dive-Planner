use crate::{commands::messages::Message, models::dive_planner::DivePlanner};
use iced::widget::{button, column};
use iced::Length;
use iced_aw::menu::{Item, Menu};
use iced_aw::{menu_bar, menu_items};

impl DivePlanner {
    pub fn menu_view(&self) -> iced::widget::Column<Message> {
        let menu_template = |items| Menu::new(items).max_width(180.0).offset(6.0);

        let menu_bar = menu_bar!((
            button("File").on_press(Message::MenuBar),
            menu_template(menu_items!((button("New")
                .width(Length::Fill)
                .on_press(Message::FileNew))(
                button("Save")
                    .width(Length::Fill)
                    .on_press(Message::FileSave)
            )(
                button("Load")
                    .width(Length::Fill)
                    .on_press(Message::FileLoad)
            )))
        )(
            button("Edit").on_press(Message::MenuBar),
            menu_template(menu_items!((button("Undo")
                .width(Length::Fill)
                .on_press(Message::EditUndo))(
                button("Redo")
                    .width(Length::Fill)
                    .on_press(Message::EditRedo)
            )))
        )(
            button("View").on_press(Message::MenuBar),
            menu_template(menu_items!(
                (button("Toggle Cylinder View")
                    .width(Length::Fill)
                    .on_press(Message::ViewToggleSelectCylinderVisibility))
            ))
        ));

        column!().push(menu_bar)
    }
}
