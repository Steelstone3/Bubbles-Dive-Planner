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

struct DivePlanner {
    dive_stage: DiveStage,
}

#[derive(Debug, Clone)]
enum Message {
    CalculateDivePlan,
    DepthChanged(String),
    TimeChanged(String),
    OxygenChanged(String),
    HeliumChanged(String),
}

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
                self.dive_stage.dive_step.depth = depth.parse::<u32>().unwrap();
            }
            Message::TimeChanged(time) => {
                self.dive_stage.dive_step.time = time.parse::<u32>().unwrap();
            }
            Message::OxygenChanged(oxygen) => {
                self.dive_stage.selected_cylinder.gas_mixture.oxygen =
                    oxygen.parse::<u32>().unwrap();
            }
            Message::HeliumChanged(helium) => {
                self.dive_stage.selected_cylinder.gas_mixture.helium =
                    helium.parse::<u32>().unwrap();
            }
        }
    }

    fn view(&self) -> Element<Message> {
        container(container(
            column![
                text("Depth").size(24),
                text_input("Enter Depth", &self.dive_stage.dive_step.depth.to_string()).on_input(Self::Message::DepthChanged),
                text("Time").size(24),
                text_input("Enter Time", &self.dive_stage.dive_step.time.to_string()).on_input(Self::Message::TimeChanged),
                text("Oxygen").size(24),
                text_input("Enter Oxygen", &self.dive_stage.selected_cylinder.gas_mixture.oxygen.to_string()).on_input(Self::Message::OxygenChanged),
                text("Helium").size(24),
                text_input("Enter Helium", &self.dive_stage.selected_cylinder.gas_mixture.helium.to_string()).on_input(Self::Message::HeliumChanged),
                button("Calculate").on_press(Self::Message::CalculateDivePlan),
                text(self.dive_stage.dive_step.depth).size(24),
            ]
            .padding(20)
            .align_items(Alignment::Start),
        ))
        .into()
    }
}

impl Default for DivePlanner {
    fn default() -> Self {
        Self::new()
    }
}
