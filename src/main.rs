use iced::widget::{button, column, text, text_input};
use iced::{Alignment, Element, Sandbox, Settings};

mod commands;
mod controllers;
mod models;
mod view_models;
mod views;

pub fn main() -> iced::Result {
    DivePlanner::run(Settings::default())
}

struct DivePlanner {
    value: i32,
}

#[derive(Debug, Clone, Copy)]
enum Message {
    CalculateDivePlan,
}

impl Sandbox for DivePlanner {
    type Message = Message;

    fn new() -> Self {
        Self { value: 0 }
    }

    fn title(&self) -> String {
        String::from("Bubbles Dive Planner")
    }

    fn update(&mut self, message: Message) {
        match message {
            Message::CalculateDivePlan => {
                self.value += 1;
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
            text(self.value).size(24),
        ]
        .padding(20)
        .align_items(Alignment::Center)
        .into()
    }
}
