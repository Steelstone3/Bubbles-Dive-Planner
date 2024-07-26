// TODO AH remove
// use super::{gas_management::GasManagementView, gas_mixture::GasMixtureView};
// use crate::{commands::messages::Message, models::cylinder::Cylinder};
// use iced::{
//     widget::Text,
//     widget::{column, text, text_input, Column, TextInput},
// };

// pub struct CylinderView<'a> {
//     cylinder_setup_text: Text<'a>,
//     cylinder_volume_text: Text<'a>,
//     cylinder_volume_input: TextInput<'a, Message>,
//     cylinder_pressure_text: Text<'a>,
//     cylinder_pressure_input: TextInput<'a, Message>,
//     cylinder_initial_pressurised_cylinder_volume_text: Text<'a>,
//     cylinder_initial_pressurised_cylinder_volume_text_value: Text<'a>,
//     gas_mixture: Column<'a, Message>,
//     gas_management: Column<'a, Message>,
// }

// impl CylinderView<'_> {
//     pub fn build_view<'a>(is_read_only: bool, cylinder: &Cylinder) -> Column<'a, Message> {
//         if is_read_only {
//             return column![];
//         }

//         let cylinder = CylinderView::new(cylinder);

//         column![
//             cylinder.cylinder_setup_text,
//             cylinder.cylinder_volume_text,
//             cylinder.cylinder_volume_input,
//             cylinder.cylinder_pressure_text,
//             cylinder.cylinder_pressure_input,
//             cylinder.cylinder_initial_pressurised_cylinder_volume_text,
//             cylinder.cylinder_initial_pressurised_cylinder_volume_text_value,
//             cylinder.gas_management.spacing(10.0),
//             cylinder.gas_mixture.spacing(10.0),
//         ]
//         .spacing(10.0)
//         .padding(10.0)
//     }

//     fn new(cylinder: &Cylinder) -> Self {
//         Self {
//             cylinder_setup_text: text("Cylinder Setup"),
//             cylinder_volume_text: text("Volume (l)"),
//             cylinder_volume_input: text_input(
//                 "Enter Cylinder Volume",
//                 &cylinder.volume.to_string(),
//             )
//             .on_input(Message::CylinderVolumeChanged),
//             cylinder_pressure_text: text("Pressure (bar)"),
//             cylinder_pressure_input: text_input(
//                 "Enter Cylinder Pressure",
//                 &cylinder.pressure.to_string(),
//             )
//             .on_input(Message::CylinderPressureChanged),
//             cylinder_initial_pressurised_cylinder_volume_text: text("Pressurised Volume"),
//             cylinder_initial_pressurised_cylinder_volume_text_value: text(
//                 cylinder.initial_pressurised_cylinder_volume,
//             ),
//             gas_mixture: GasMixtureView::build_view(&cylinder.gas_mixture),
//             gas_management: GasManagementView::build_view(&cylinder.gas_management),
//         }
//     }
// }
