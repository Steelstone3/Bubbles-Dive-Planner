use crate::{
    application::{messages::message::Message, states::tab_identifier::TabIdentifier},
    models::application::dive_planner::DivePlanner,
};
use iced::{Element, Task, Theme};

impl DivePlanner {
    #[allow(dead_code)]
    pub fn boot() -> (Self, Task<Message>) {
        (Self::default(), Task::none())
    }

    #[allow(dead_code)]
    pub fn view(&self) -> Element<'_, Message> {
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

#[cfg(test)]
mod application_state_should {
    use crate::{
        application::states::tab_identifier::TabIdentifier,
        models::application::{application_state::ApplicationState, dive_planner::DivePlanner},
    };
    use iced::Theme;
    use rstest::rstest;

    #[rstest]
    #[case(true, Theme::Light)]
    #[case(false, Theme::Dark)]
    fn test_theme(#[case] expected_is_light_theme: bool, #[case] expected_theme: Theme) {
        // Given
        let dive_planner = DivePlanner {
            application_state: ApplicationState {
                is_light_theme: expected_is_light_theme,
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        let theme = dive_planner.theme();

        // Then
        pretty_assertions::assert_eq!(
            expected_is_light_theme,
            dive_planner.application_state.is_light_theme
        );
        pretty_assertions::assert_eq!(expected_theme, theme);
    }

    #[rstest]
    #[case(true, Theme::Dark, false)]
    #[case(false, Theme::Light, true)]
    fn test_switch_theme(
        #[case] is_light_theme: bool,
        #[case] expected_theme: Theme,
        #[case] expected_is_light_theme: bool,
    ) {
        // Given
        let mut dive_planner = DivePlanner {
            application_state: ApplicationState {
                is_light_theme,
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        dive_planner.switch_theme();

        // Then
        pretty_assertions::assert_eq!(
            expected_is_light_theme,
            dive_planner.application_state.is_light_theme
        );
        pretty_assertions::assert_eq!(expected_theme, dive_planner.theme());
    }

    #[rstest]
    #[case(TabIdentifier::Plan, TabIdentifier::Plan)]
    #[case(TabIdentifier::Information, TabIdentifier::Plan)]
    #[case(TabIdentifier::Plan, TabIdentifier::Information)]
    #[case(TabIdentifier::Plan, TabIdentifier::Results)]
    fn test_switch_tab(
        #[case] tab_identifier: TabIdentifier,
        #[case] expected_tab_identifier: TabIdentifier,
    ) {
        // Given
        let mut dive_planner = DivePlanner {
            application_state: ApplicationState {
                tab_identifier,
                ..Default::default()
            },
            ..Default::default()
        };

        // When
        dive_planner.switch_tab(expected_tab_identifier.clone());

        // Then
        pretty_assertions::assert_eq!(
            expected_tab_identifier.clone(),
            dive_planner.application_state.tab_identifier
        )
    }
}
