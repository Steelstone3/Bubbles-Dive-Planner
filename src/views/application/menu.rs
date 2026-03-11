use crate::application::messages::message::Message;
use crate::models::application::dive_planner::DivePlanner;
use iced::Length;
use iced::widget::{button, column};
use iced_aw::menu::Menu;
use iced_aw::{menu_bar, menu_items};

impl DivePlanner {
    pub fn menu_view(&self) -> iced::widget::Column<'_, Message> {
        let menu_template = |items| Menu::new(items).max_width(180.0).offset(6.0);

        match self.dive_planning.is_planning {
            true => {
                let menu_bar = menu_bar!(
                    (
                        button("File").on_press(Message::MenuBar),
                        menu_template(menu_items!(
                            (button("New")
                                .width(Length::Fill)
                                .on_press(Message::FileOnNewClicked)),
                            (button("Load")
                                .width(Length::Fill)
                                .on_press(Message::FileOnLoadCompleted))
                        ))
                    ),
                    (
                        button("Edit").on_press(Message::MenuBar),
                        menu_template(menu_items!(
                            (button("Undo")
                                .width(Length::Fill)
                                .on_press(Message::EditOnUndoClicked)),
                            (button("Redo")
                                .width(Length::Fill)
                                .on_press(Message::EditOnRedoClicked))
                        ))
                    ),
                    (
                        button("View").on_press(Message::MenuBar),
                        menu_template(menu_items!(
                            (button("Toggle Theme")
                                .width(Length::Fill)
                                .on_press(Message::ViewOnToggleThemeClicked)),
                        ))
                    )
                );

                column!().push(menu_bar)
            }
            false => {
                let menu_bar = menu_bar!(
                    (
                        button("File").on_press(Message::MenuBar),
                        menu_template(menu_items!(
                            (button("New")
                                .width(Length::Fill)
                                .on_press(Message::FileOnNewClicked)),
                            (button("Save")
                                .width(Length::Fill)
                                .on_press(Message::FileOnSaveCompleted)),
                            (button("Save Results")
                                .width(Length::Fill)
                                .on_press(Message::FileOnSaveResultsCompleted)),
                            (button("Load")
                                .width(Length::Fill)
                                .on_press(Message::FileOnLoadCompleted))
                        ))
                    ),
                    (
                        button("Edit").on_press(Message::MenuBar),
                        menu_template(menu_items!(
                            (button("Undo")
                                .width(Length::Fill)
                                .on_press(Message::EditOnUndoClicked)),
                            (button("Redo")
                                .width(Length::Fill)
                                .on_press(Message::EditOnRedoClicked))
                        ))
                    ),
                    (
                        button("View").on_press(Message::MenuBar),
                        menu_template(menu_items!(
                            (button("Toggle Theme")
                                .width(Length::Fill)
                                .on_press(Message::ViewOnToggleThemeClicked))
                        ))
                    )
                );

                column!().push(menu_bar)
            }
        }
    }
}
