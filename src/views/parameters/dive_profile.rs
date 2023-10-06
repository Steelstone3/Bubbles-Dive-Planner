use iced::widget::{button, column, Button, Column};
use crate::{
    commands::messages::Message, models::dive_stage::DiveStage,
};

pub struct DiveProfileView<'a> {
    update_dive_profile: Button<'a, Message>,
}

impl DiveProfileView<'_> {
    pub fn build_view<'a>(dive_stage: &DiveStage) -> Column<'a, Message> {
        let dive_profile = DiveProfileView::new(dive_stage);

        column![dive_profile.update_dive_profile]
    }

    fn new<'a>(dive_stage: &DiveStage) -> DiveProfileView<'a> {
        DiveProfileView {
            update_dive_profile: DiveProfileView::is_update_dive_profile_button_enabled(dive_stage),
        }
    }

    fn is_update_dive_profile_button_enabled<'a>(dive_stage: &DiveStage) -> Button<'a, Message> {
        if !dive_stage.validate() {
            return button("Invalid Parameters");
        }

        button("Update Dive Profile").on_press(Message::UpdateDiveProfile)
    }
}
