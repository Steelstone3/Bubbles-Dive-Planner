use iced::Settings;
use models::dive_planner::DivePlanner;

mod commands;
mod controllers;
mod models;
mod test_fixture;
mod views;

pub fn main() -> iced::Result {
    iced::application("Clock - Iced", DivePlanner::update, DivePlanner::view)
    // .subscription(DivePlanner::subscription)
    // .theme(DivePlanner::theme)
    .antialiasing(true)
    .settings(Settings::default())
    .run()
}
