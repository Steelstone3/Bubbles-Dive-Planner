use iced::widget::{column, text};
use iced_aw::widgets::Card;

use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner};

impl DivePlanner {
    pub fn dive_boundaries_view(&self) -> iced::widget::Column<Message> {
        // let maximum_operating_depth = dive_planner
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

        column!(Card::new(
            "Dive Boundaries",
            text(format!(
                "{}\n{}",
                self.dive_stage
                    .cylinder
                    .gas_mixture
                    .display_maximum_operating_depth(),
                self.dive_stage
                    .dive_model
                    .dive_profile
                    .display_dive_ceiling()
            ))
        ))
        .spacing(10)
    }
}
