use iced::widget::{column, Column, Scrollable};
use iced_aw::{TabBar, TabLabel};

use crate::{
    commands::{messages::Message, tab_identifier::TabIdentifier},
    models::dive_planner::DivePlanner,
};

impl DivePlanner {
    pub fn tab_bar_view(&self) -> Column<Message> {
        // TODO AH extract repetition
        match self.tab_identifier {
            TabIdentifier::Plan => {
                let tab_bar = TabBar::new(Message::TabSelected)
                    .push(
                        TabIdentifier::Plan,
                        TabLabel::IconText('p', "Plan".to_string()),
                    )
                    .push(
                        TabIdentifier::Information,
                        TabLabel::IconText('i', "Information".to_string()),
                    )
                    .push(
                        TabIdentifier::Results,
                        TabLabel::IconText('r', "Results".to_string()),
                    )
                    .set_active_tab(&TabIdentifier::Plan);

                let contents = Scrollable::new(column!().push(self.plan_view()));

                column!(self.menu_view(), tab_bar, contents)
            }
            TabIdentifier::Information => {
                let tab_bar = TabBar::new(Message::TabSelected)
                    .push(
                        TabIdentifier::Plan,
                        TabLabel::IconText('p', "Plan".to_string()),
                    )
                    .push(
                        TabIdentifier::Information,
                        TabLabel::IconText('i', "Information".to_string()),
                    )
                    .push(
                        TabIdentifier::Results,
                        TabLabel::IconText('r', "Results".to_string()),
                    )
                    .set_active_tab(&TabIdentifier::Information);

                let contents = Scrollable::new(column!().push(self.information_view()));

                column!(self.menu_view(), tab_bar, contents)
            }
            TabIdentifier::Results => {
                let tab_bar = TabBar::new(Message::TabSelected)
                    .push(
                        TabIdentifier::Plan,
                        TabLabel::IconText('p', "Plan".to_string()),
                    )
                    .push(
                        TabIdentifier::Information,
                        TabLabel::IconText('i', "Information".to_string()),
                    )
                    .push(
                        TabIdentifier::Results,
                        TabLabel::IconText('r', "Results".to_string()),
                    )
                    .set_active_tab(&TabIdentifier::Results);

                let contents = Scrollable::new(column!().push(self.results_view()));

                column!(self.menu_view(), tab_bar, contents)
            }
        }
    }
}
