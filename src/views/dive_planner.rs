use crate::commands::messages::Message;
use crate::controllers::dive_stage::update_dive_profile;

use crate::models::dive_step::DiveStep;
use crate::models::gas_mixture::GasMixture;
use crate::{models::dive_stage::DiveStage, view_models::dive_planner::DivePlanner};
use iced::widget::{button, column, scrollable, text};
use iced::{Alignment, Element, Length, Sandbox};

use super::cylinder::CylinderView;
use super::dive_step::DiveStepView;
use super::input_parser::parse_input_u32;

impl Sandbox for DivePlanner {
    type Message = Message;

    fn new() -> Self {
        Self {
            dive_stage: DiveStage::default(),
        }
    }

    fn title(&self) -> String {
        String::from("Bubbles Dive Planner")
    }

    fn update(&mut self, message: Message) {
        match message {
            Message::CalculateDivePlan => {
                self.dive_stage = update_dive_profile(self.dive_stage);
            }
            Message::DepthChanged(depth) => {
                self.dive_stage.dive_step.depth = DiveStepView::update_depth(depth);
            }
            Message::TimeChanged(time) => {
                // TODO move to view
                let time_input = parse_input_u32(time, 0);
                self.dive_stage.dive_step.time = DiveStep::validate(time_input, 60);
            }
            Message::OxygenChanged(oxygen) => {
                // TODO move to view
                let oxygen_input = parse_input_u32(oxygen, 5);

                let helium = self.dive_stage.cylinder.gas_mixture.helium;
                let gas_mixture = GasMixture::validate_oxygen(oxygen_input, helium);

                self.dive_stage.cylinder.gas_mixture = gas_mixture;
            }
            Message::HeliumChanged(helium) => {
                // TODO move to view
                let helium_input = parse_input_u32(helium, 0);

                let oxygen = self.dive_stage.cylinder.gas_mixture.oxygen;
                let gas_mixture = GasMixture::validate_helium(oxygen, helium_input);

                self.dive_stage.cylinder.gas_mixture = gas_mixture;
            }
        }
    }

    fn view(&self) -> Element<Message> {
        let dive_step = DiveStepView::new(self);
        let cylinder = CylinderView::new(self);

        column![iced::widget::row![scrollable(
            column![
                dive_step.depth_text,
                dive_step.depth_input,
                dive_step.time_text,
                dive_step.time_input,
                cylinder.oxygen_text,
                cylinder.oxygen_input,
                cylinder.helium_text,
                cylinder.helium_input,
                cylinder.nitrogen_text,
                cylinder.nitrogen_text_value,
                button("Calculate").on_press(Self::Message::CalculateDivePlan)
            ]
            .align_items(Alignment::Start)
            .spacing(10),
        )]
        .spacing(10)
        .push(scrollable(
            column![text(self.dive_stage.dive_model.dive_profile)]
                .align_items(Alignment::Start)
                .spacing(10)
                .width(Length::FillPortion(4))
        )),]
        .align_items(Alignment::Center)
        .width(Length::FillPortion(1))
        .spacing(10)
        .into()
    }
}
