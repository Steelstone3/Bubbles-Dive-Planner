// TODO AH remove
// use crate::{commands::messages::Message, models::gas_management::GasManagement};
// use iced::widget::{column, text, text_input, Column, Text, TextInput};

// pub struct GasManagementView<'a> {
//     surface_air_consumption_text: Text<'a>,
//     surface_air_consumption_input: TextInput<'a, Message>,
// }

// impl GasManagementView<'_> {
//     pub fn build_view<'a>(gas_management: &GasManagement) -> Column<'a, Message> {
//         let gas_management = GasManagementView::new(gas_management);

//         column![
//             gas_management.surface_air_consumption_text,
//             gas_management.surface_air_consumption_input
//         ]
//     }

//     fn new<'a>(gas_management: &GasManagement) -> GasManagementView<'a> {
//         GasManagementView {
//             surface_air_consumption_text: text("S.A.C Rate (l/min)"),
//             surface_air_consumption_input: text_input(
//                 "Enter S.A.C Rate",
//                 &gas_management.surface_air_consumption_rate.to_string(),
//             )
//             .on_input(Message::SurfaceAirConsumptionChanged),
//         }
//     }
// }
