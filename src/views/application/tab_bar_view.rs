use iced::widget::{Column, Scrollable, column};
use iced_aw::{TabBar, TabLabel};

use crate::{
    commands::{messages::Message, tab_identifier::TabIdentifier},
    models::application::dive_planner::DivePlanner,
};

impl DivePlanner {
    pub fn tab_bar_view(&self) -> Column<Message> {
        match self.application_state.tab_identifier {
            TabIdentifier::Plan => {
                let tab_bar = selected_tab_bar(&TabIdentifier::Plan);

                let contents = Scrollable::new(column!().push(self.plan_view()));

                column!(self.menu_view(), tab_bar, contents)
            }
            TabIdentifier::Information => {
                let tab_bar = selected_tab_bar(&TabIdentifier::Information);

                let contents = Scrollable::new(column!().push(self.information_view()));

                column!(self.menu_view(), tab_bar, contents)
            }
            TabIdentifier::Results => {
                let tab_bar = selected_tab_bar(&TabIdentifier::Results);

                let contents = Scrollable::new(column!().push(self.results_view()));

                column!(self.menu_view(), tab_bar, contents)
            }
        }
    }
}

fn selected_tab_bar(active_tab: &TabIdentifier) -> TabBar<'static, Message, TabIdentifier> {
    TabBar::new(Message::SelectedTabChanged)
        .push(
            TabIdentifier::Plan,
            TabLabel::IconText('\u{1F93F}', "Plan".to_string()),
        )
        .push(
            TabIdentifier::Information,
            TabLabel::IconText('\u{1F50D}', "Information".to_string()),
        )
        .push(
            TabIdentifier::Results,
            TabLabel::IconText('\u{1F4CA}', "Results".to_string()),
        )
        .set_active_tab(active_tab)
}
