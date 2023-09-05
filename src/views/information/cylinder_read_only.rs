use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner};
use iced::{widget::text, widget::Text};
use iced_aw::Card;

pub struct CylinderReadOnlyView<'a> {
    pub cylinder_read_only_text_title: Text<'a>,
    pub cylinder_read_only_text: Card<'a, Message>,
}

impl CylinderReadOnlyView<'_> {
    pub fn new(dive_planner: &DivePlanner) -> Self {
        Self {
            cylinder_read_only_text_title: text("Select Cylinder"),
            cylinder_read_only_text: Card::new("Cylinder", text(dive_planner.dive_stage.cylinder)),
        }
    }
}
