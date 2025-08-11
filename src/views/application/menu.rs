use crate::commands::messages::Message;
use crate::models::application::dive_planner::DivePlanner;
use iced::Length;
use iced::widget::{button, column};
use iced_aw::menu::{Item, Menu};
use iced_aw::{menu_bar, menu_items};

impl DivePlanner {
    pub fn menu_view(&self) -> iced::widget::Column<'_, Message> {
        let menu_template = |items| Menu::new(items).max_width(180.0).offset(6.0);

        match self.application_state.is_planning {
            true => {
                let menu_bar = menu_bar!((
                    button("File").on_press(Message::MenuBar),
                    menu_template(menu_items!((button("New")
                        .width(Length::Fill)
                        .on_press(Message::FileNew))(
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
                    menu_template(menu_items!((button("Toggle Theme")
                        .width(Length::Fill)
                        .on_press(Message::ViewToggleTheme))(
                        button("Toggle Cylinder View")
                            .width(Length::Fill)
                            .on_press(Message::ViewToggleSelectedCylinderVisibility)
                    )))
                ));

                column!().push(menu_bar)
            }
            false => {
                let menu_bar = menu_bar!((
                    button("File").on_press(Message::MenuBar),
                    menu_template(menu_items!((button("New")
                        .width(Length::Fill)
                        .on_press(Message::FileNew))(
                        button("Save")
                            .width(Length::Fill)
                            .on_press(Message::FileSave)
                    )(
                        button("Save Results")
                            .width(Length::Fill)
                            .on_press(Message::FileSaveResults)
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
                        (button("Toggle Theme")
                            .width(Length::Fill)
                            .on_press(Message::ViewToggleTheme))
                    ))
                ));

                column!().push(menu_bar)
            }
        }
    }
}
