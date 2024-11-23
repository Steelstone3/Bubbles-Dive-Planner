use crate::{commands::messages::Message, models::dive_planner::DivePlanner};
use iced::widget::{column, text, text_input, Column};

impl DivePlanner {
    pub fn dive_step_view(&self) -> Column<Message> {
        column!()
            .push(text("Dive Step"))
            .spacing(10)
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
            .spacing(10)
    }
}
