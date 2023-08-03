use iced::widget::{button, column, text, text_input};
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

#[derive(Debug, Clone, Copy)]
enum Message {
    CalculateDivePlan,
    DepthChanged(u32),
    TimeChanged(u32),
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
                self.dive_stage.dive_step.depth = depth;
            }
            Message::TimeChanged(time) => {
                self.dive_stage.dive_step.time = time;
            }
        }
    }

    fn view(&self) -> Element<Message> {
        column![
            text("Depth").size(24),
            text_input("", ""),
            text("Time").size(24),
            text_input("", ""),
            button("Calculate").on_press(Self::Message::CalculateDivePlan),
            text(self.dive_stage.dive_step.depth).size(24),
        ]
        .padding(20)
        .align_items(Alignment::Center)
        .into()
    }
}

impl Default for DivePlanner {
    fn default() -> Self {
        Self::new()
    }
}
