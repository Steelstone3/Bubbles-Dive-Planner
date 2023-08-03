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

#[derive(Debug, Clone)]
enum Message {
    CalculateDivePlan,
    DepthChanged(String),
    TimeChanged(String),
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
                match depth.parse::<u32>() {
                    Ok() =>
                }
            }
            Message::TimeChanged(time) => {
                self.dive_stage.dive_step.time = time.parse::<u32>().unwrap();
            }
        }
    }

    fn view(&self) -> Element<Message> {
        // fn text_input(
        //     value: &str,
        //     is_secure: bool,
        //     is_showing_icon: bool,
        // ) -> Column<'a, StepMessage> {
        //     let mut text_input = text_input("Type something to continue...", value)
        //         .on_input(StepMessage::InputChanged)
        //         .padding(10)
        //         .size(30);

        column![
            text("Depth").size(24),
            text_input("", "").on_input(Self::Message::DepthChanged),
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
