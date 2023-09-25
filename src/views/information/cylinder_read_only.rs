use crate::{commands::messages::Message, models::cylinder::Cylinder};
use iced::widget::text;
use iced_aw::Card;

pub struct CylinderReadOnlyView<'a> {
    pub cylinder_read_only_text: Card<'a, Message>,
}

impl CylinderReadOnlyView<'_> {
    // TODO add a build view
    pub fn new(cylinder: &Cylinder) -> Self {
        Self {
            cylinder_read_only_text: Card::new("Cylinder", text(cylinder)),
        }
    }
}
