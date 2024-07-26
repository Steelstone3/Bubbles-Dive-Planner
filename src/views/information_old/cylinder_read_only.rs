// TODO AH remove
// use crate::{commands::messages::Message, models::cylinder::Cylinder};
// use iced::widget::{column, text, Column};
// use iced_aw::Card;

// pub struct CylinderReadOnlyView<'a> {
//     cylinder_read_only_text: Card<'a, Message>,
// }

// impl CylinderReadOnlyView<'_> {
//     pub fn build_view<'a>(cylinder: &Cylinder) -> Column<'a, Message> {
//         let cylinder_read_only = CylinderReadOnlyView::new(cylinder);

//         column![cylinder_read_only.cylinder_read_only_text]
//             .padding(10.0)
//             .spacing(10.0)
//     }

//     fn new(cylinder: &Cylinder) -> Self {
//         Self {
//             cylinder_read_only_text: Card::new("Cylinder", text(cylinder)),
//         }
//     }
// }
