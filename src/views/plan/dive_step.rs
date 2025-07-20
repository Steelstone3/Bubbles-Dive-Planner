use crate::{commands::messages::Message, models::application::dive_planner::DivePlanner};
use iced::widget::{Column, column, text, text_input};
use iced_aw::Card;

impl DivePlanner {
    pub fn dive_step_view(&self) -> Column<Message> {
        let contents = column!()
            .push(text("Depth"))
            .spacing(10)
            .push(
                text_input("Enter Depth", &self.dive_stage.dive_step.depth.to_string())
                    .on_input(Message::DepthChanged),
            )
            .spacing(10)
            .push(text("Time"))
            .spacing(10)
            .push(
                text_input("Enter Time", &self.dive_stage.dive_step.time.to_string())
                    .on_input(Message::TimeChanged),
            )
            .spacing(10);

        column!().push(Card::new("Dive Step", contents))
    }
}
