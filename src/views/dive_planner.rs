use crate::view_models::dive_planner::Message;
use crate::{models::dive_stage::DiveStage, view_models::dive_planner::DivePlanner};
use iced::widget::{button, column, container, text, text_input};
use iced::{Alignment, Element, Sandbox};

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
                // TODO move this to a parser controller in views
                let mut depth = depth.parse::<u32>().unwrap_or(0);

                // TODO move this to model validation
                if depth > 100 {
                    depth = 100;
                }

                self.dive_stage.dive_step.depth = depth;
            }
            Message::TimeChanged(time) => {
                // TODO move this to a parser controller in views
                let mut time = time.parse::<u32>().unwrap_or(0);

                // TODO move this to model validation
                if time > 60 {
                    time = 60;
                }

                self.dive_stage.dive_step.time = time;
            }
            Message::OxygenChanged(oxygen) => {
                // TODO move this to a parser controller in views
                let mut oxygen_input = oxygen.parse::<u32>().unwrap_or(5);

                // TODO Move this to model validation
                if oxygen_input > 100 {
                    oxygen_input = 100;
                }
                // TODO Move this to model validation
                let mut helium = self.dive_stage.cylinder.gas_mixture.helium;
                if oxygen_input + helium > 100 {
                    helium = 100 - oxygen_input;
                }

                // TODO Move this to model validation (output a gas mixture with nitrogen)
                self.dive_stage.cylinder.gas_mixture.oxygen = oxygen_input;
                self.dive_stage.cylinder.gas_mixture.helium = helium;
                self.dive_stage.cylinder.gas_mixture.nitrogen =
                    100 - oxygen_input - helium;
            }
            Message::HeliumChanged(helium) => {
                // TODO move this to a parser controller in views
                let mut helium_input = helium.parse::<u32>().unwrap_or(0);

                // TODO Move this to model validation
                let mut oxygen = self.dive_stage.cylinder.gas_mixture.oxygen;
                if helium_input > 95 {
                    helium_input = 95;
                    oxygen = 5
                }
                // TODO Move this to model validation
                if helium_input + oxygen > 100 {
                    oxygen = 100 - helium_input;
                }

                // TODO Move this to model validation (output a gas mixture with nitrogen)
                self.dive_stage.cylinder.gas_mixture.helium = helium_input;
                self.dive_stage.cylinder.gas_mixture.oxygen = oxygen;
                self.dive_stage.cylinder.gas_mixture.nitrogen =
                    100 - helium_input - oxygen;
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
                    &self
                        .dive_stage
                        .cylinder
                        .gas_mixture
                        .oxygen
                        .to_string()
                )
                .on_input(Self::Message::OxygenChanged),
                text("Helium").size(24),
                text_input(
                    "Enter Helium",
                    &self
                        .dive_stage
                        .cylinder
                        .gas_mixture
                        .helium
                        .to_string()
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
