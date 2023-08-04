use iced::widget::{button, column, container, text, text_input};
use iced::{Alignment, Element, Sandbox, Settings};
use models::dive_stage::DiveStage;

mod commands;
mod controllers;
mod models;
mod view_models;
mod views;

pub fn main() -> iced::Result {
    DivePlanner::run(Settings::default())
}

// TODO move this to view models
struct DivePlanner {
    dive_stage: DiveStage,
}

// TODO move this to view models
#[derive(Debug, Clone)]
enum Message {
    CalculateDivePlan,
    DepthChanged(String),
    TimeChanged(String),
    OxygenChanged(String),
    HeliumChanged(String),
}

// TODO move this to views
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
                let mut depth = match depth.parse::<u32>() {
                    Ok(depth) => depth,
                    Err(_) => 0,
                };

                // TODO move this to model validation
                if depth > 100 {
                    depth = 100;
                }

                self.dive_stage.dive_step.depth = depth;
            }
            Message::TimeChanged(time) => {
                // TODO move this to a parser controller in views
                let mut time = match time.parse::<u32>() {
                    Ok(time) => time,
                    Err(_) => 0,
                };

                // TODO move this to model validation
                if time > 60 {
                    time = 60;
                }

                self.dive_stage.dive_step.time = time;
            }
            Message::OxygenChanged(oxygen) => {
                // TODO move this to a parser controller in views
                let mut oxygen_input = match oxygen.parse::<u32>() {
                    Ok(oxygen) => oxygen,
                    Err(_) => 5,
                };

                // TODO Move this to model validation
                if oxygen_input > 100 {
                    oxygen_input = 100;
                }
                // TODO Move this to model validation
                let mut helium = self.dive_stage.selected_cylinder.gas_mixture.helium;
                if oxygen_input + helium > 100 {
                    helium = 100 - oxygen_input;
                }

                // TODO Move this to model validation (output a gas mixture with nitrogen)
                self.dive_stage.selected_cylinder.gas_mixture.oxygen = oxygen_input;
                self.dive_stage.selected_cylinder.gas_mixture.helium = helium;
                self.dive_stage.selected_cylinder.gas_mixture.nitrogen =
                    100 - oxygen_input - helium;
            }
            Message::HeliumChanged(helium) => {
                // TODO move this to a parser controller in views
                let mut helium_input = match helium.parse::<u32>() {
                    Ok(helium) => helium,
                    Err(_) => 0,
                };

                // TODO Move this to model validation
                let mut oxygen = self.dive_stage.selected_cylinder.gas_mixture.oxygen;
                if helium_input > 95 {
                    helium_input = 95;
                    oxygen = 5
                }
                // TODO Move this to model validation
                if helium_input + oxygen > 100 {
                    oxygen = 100 - helium_input;
                }

                // TODO Move this to model validation (output a gas mixture with nitrogen)
                self.dive_stage.selected_cylinder.gas_mixture.helium = helium_input;
                self.dive_stage.selected_cylinder.gas_mixture.oxygen = oxygen;
                self.dive_stage.selected_cylinder.gas_mixture.nitrogen =
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
                        .selected_cylinder
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
                        .selected_cylinder
                        .gas_mixture
                        .helium
                        .to_string()
                )
                .on_input(Self::Message::HeliumChanged),
                text("Nitrogen").size(24),
                text(self.dive_stage.selected_cylinder.gas_mixture.nitrogen).size(24),
                button("Calculate").on_press(Self::Message::CalculateDivePlan),
                text(self.dive_stage.dive_step.depth).size(24),
            ]
            .padding(20)
            .align_items(Alignment::Start),
        ))
        .into()
    }
}

// TODO move this to view models
impl Default for DivePlanner {
    fn default() -> Self {
        Self::new()
    }
}
