use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner};
use iced::widget::{button, column};
use iced::Length;
use iced_aw::menu::{Item, Menu};
use iced_aw::{menu_bar, menu_items};

impl DivePlanner {
    pub fn menu_view(&self) -> iced::widget::Column<Message> {
        // let file = Item::with_menu(
        //     button("File").on_press(Message::MenuBar),
        //     Menu::new(
        //         [
        //             Item::new(button("New").on_press(Message::FileNew)),
        //             Item::new(button("Save").on_press(Message::FileSave)),
        //             Item::new(button("Load").on_press(Message::FileLoad)),
        //         ]
        //         .into(),
        //     ),
        // );

        // let edit = Item::with_menu(
        //     button("Edit").on_press(Message::MenuBar),
        //     Menu::new(
        //         [
        //             Item::new(button("Undo").on_press(Message::EditUndo)),
        //             Item::new(button("Redo").on_press(Message::EditRedo)),
        //         ]
        //         .into(),
        //     ),
        // );

        // column!().push(MenuBar::new(vec![file, edit]))

        // use iced::widget::button;
        // use iced_aw::menu::{Menu}
        // use iced_aw::{menu_bar, menu_items}

        // let menu_template = |items| Menu::new(items).max_width(180.0).offset(6.0);

        // let menu_bar = menu_bar!(
        //     (button("Menu 1"),menu_template(menu_items!(
        //         (button("item_1"))
        //         (button("item_2"))
        //         (button("Sub Menu 1"), menu_template(menu_items!(
        //             (button("item_1"))
        //             (button("Sub Menu 2"), menu_template(menu_items!(
        //                 (button("item_1"))
        //                 (button("item_2"))
        //                 (button("item_3"))
        //             )))
        //             (button("item_2"))
        //             (button("item_3"))
        //         )))
        //         (button("item_3"))
        //     )))
        //     (button("Menu 2"), menu_template(menu_items!(
        //         (button("item_1"))
        //         (button("item_2"))
        //         (button("item_3"))
        //     )))
        // )

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
        ));

        column!().push(menu_bar)
    }

    // use iced::widget::button;
    // use iced_aw::menu::{Item, Menu, MenuBar};

    // let sub_2 = Item::with_menu(
    //     button("Sub Menu 2"),
    //     Menu::new([
    //         Item::new(button("item_1")),
    //         Item::new(button("item_2")),
    //         Item::new(button("item_3")),
    //     ].into())
    // );

    // let sub_1 = Item::with_menu(
    //     button("Sub Menu 1"),
    //     Menu::new([
    //         Item::new(button("item_1")),
    //         sub_2,
    //         Item::new(button("item_2")),
    //         Item::new(button("item_3")),
    //     ].into())
    // );

    // let root_1 = Item::with_menu(
    //     button("Menu 1"),
    //     Menu::new([
    //         Item::new(button("item_1")),
    //         Item::new(button("item_2")),
    //         sub_1,
    //         Item::new(button("item_3")),
    //     ].into())
    // );

    // let root_2 = Item::with_menu(
    //     button("Menu 2"),
    //     Menu::new([
    //         Item::new(button("item_1")),
    //         Item::new(button("item_2")),
    //         Item::new(button("item_3")),
    //     ].into())
    // );

    // let menu_bar = MenuBar::new(vec![root_1, root_2]);

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
