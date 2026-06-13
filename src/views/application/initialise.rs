use iced::{Element, Task};

use crate::{
    application::messages::message::Message, models::application::dive_planner::DivePlanner,
};

impl DivePlanner {
    #[allow(dead_code)]
    pub fn boot() -> (Self, Task<Message>) {
        (Self::default(), Task::none())
    }

    #[allow(dead_code)]
    pub fn view(&self) -> Element<'_, Message> {
        self.tab_bar_view().into()
    }
}

#[cfg(test)]
mod initialise_should {
    use crate::models::application::dive_planner::DivePlanner;

    #[test]
    fn test_boot() {
        // given
        let expected_dive_planner = DivePlanner::default();

        // when
        let application = DivePlanner::boot();

        // then
        pretty_assertions::assert_eq!(expected_dive_planner, application.0);
    }
}
