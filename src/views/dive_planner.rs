use crate::models::dive_step::DiveStep;
use crate::models::gas_mixture::GasMixture;
use crate::view_models::dive_planner::Message;
use crate::{models::dive_stage::DiveStage, view_models::dive_planner::DivePlanner};
use iced::widget::{button, column, container, text, text_input};
use iced::{Alignment, Element, Sandbox};

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
                self.dive_stage.dive_step.depth += 1;
            }
            Message::DepthChanged(depth) => {
                let depth_input = parse_input_u32(depth, 0);
                self.dive_stage.dive_step.depth = DiveStep::validate(depth_input, 100);
            }
            Message::TimeChanged(time) => {
                let time_input = parse_input_u32(time, 0);
                self.dive_stage.dive_step.time = DiveStep::validate(time_input, 60);
            }
            Message::OxygenChanged(oxygen) => {
                let oxygen_input = parse_input_u32(oxygen, 5);

                let helium = self.dive_stage.cylinder.gas_mixture.helium;
                let gas_mixture = GasMixture::validate_oxygen(oxygen_input, helium);

                self.dive_stage.cylinder.gas_mixture = gas_mixture;
            }
            Message::HeliumChanged(helium) => {
                let helium_input = parse_input_u32(helium, 0);

                let oxygen = self.dive_stage.cylinder.gas_mixture.oxygen;
                let gas_mixture = GasMixture::validate_helium(oxygen, helium_input);

                self.dive_stage.cylinder.gas_mixture = gas_mixture;
            }
        }
    }

    fn view(&self) -> Element<Message> {
        container(container(
            column![
                text("Depth").size(24),
                text_input("Enter Depth", &self.dive_stage.dive_step.depth.to_string())
                    .on_input(Self::Message::DepthChanged),
                text("Time").size(24),
                text_input("Enter Time", &self.dive_stage.dive_step.time.to_string())
                    .on_input(Self::Message::TimeChanged),
                text("Oxygen").size(24),
                text_input(
                    "Enter Oxygen",
                    &self.dive_stage.cylinder.gas_mixture.oxygen.to_string()
                )
                .on_input(Self::Message::OxygenChanged),
                text("Helium").size(24),
                text_input(
                    "Enter Helium",
                    &self.dive_stage.cylinder.gas_mixture.helium.to_string()
                )
                .on_input(Self::Message::HeliumChanged),
                text("Nitrogen").size(24),
                text(self.dive_stage.cylinder.gas_mixture.nitrogen).size(24),
                button("Calculate").on_press(Self::Message::CalculateDivePlan),
                text(self.dive_stage.dive_step.depth).size(24),
            ]
            .padding(20)
            .align_items(Alignment::Start),
        ))
        .into()
    }
}
