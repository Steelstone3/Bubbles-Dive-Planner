use crate::{
    commands::{messages::Message, tab_identifier::TabIdentifier},
    models::application::dive_planner::DivePlanner,
};
use iced::{Element, Theme};

impl DivePlanner {
    pub fn view(&self) -> Element<Message> {
        self.tab_bar_view().into()
    }

    pub fn switch_tab(&mut self, tab_identifier: TabIdentifier) {
        match tab_identifier {
            TabIdentifier::Plan => {
                self.application_state.tab_identifier = TabIdentifier::Plan;
            }
            TabIdentifier::Information => {
                self.application_state.tab_identifier = TabIdentifier::Information;
            }
            TabIdentifier::Results => {
                self.application_state.tab_identifier = TabIdentifier::Results;
            }
        }
    }

    pub fn theme(&self) -> Theme {
        // Theme::GruvboxDark
        match self.application_state.is_light_theme {
            true => Theme::Light,
            false => Theme::Dark,
        }
    }

    pub fn switch_theme(&mut self) {
        match self.application_state.is_light_theme {
            true => self.application_state.is_light_theme = false,
            false => self.application_state.is_light_theme = true,
        }
    }
}
