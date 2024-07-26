// TODO AH remove
// use iced::widget::{column, text, Column};
// use iced_aw::Card;

// use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner};

// use super::decompression_steps::DecompressionStepsView;

// pub struct DiveInformationView<'a> {
//     dive_information_text: Card<'a, Message>,
//     decompression_steps: Column<'a, Message>,
// }

// impl DiveInformationView<'_> {
//     pub fn build_view<'a>(dive_planner: &DivePlanner) -> Column<'a, Message> {
//         let dive_information = Self::new(dive_planner);

//         column![
//             dive_information.dive_information_text,
//             dive_information.decompression_steps
//         ]
//         .padding(10.0)
//     }

//     fn new<'a>(dive_planner: &DivePlanner) -> DiveInformationView<'a> {
//         let maximum_operating_depth = dive_planner
//             .dive_stage
//             .cylinder
//             .gas_mixture
//             .display_maximum_operating_depth();
//         let dive_ceiling = dive_planner
//             .dive_stage
//             .dive_model
//             .dive_profile
//             .display_dive_ceiling();

//         DiveInformationView {
//             dive_information_text: Card::new(
//                 "Dive Information",
//                 text(format!(
//                     "{}Dive Boundaries\n\n{}\n{}",
//                     &dive_planner.cns_toxicity, maximum_operating_depth, dive_ceiling
//                 )),
//             )
//             .width(iced::Length::Fixed(500.0)),
//             decompression_steps: DecompressionStepsView::build_view(dive_planner),
//         }
//     }
// }
