use iced::Settings;
use models::application::dive_planner::DivePlanner;

mod commands;
mod controllers;
mod models;
mod test_fixture;
mod views;

pub fn main() -> iced::Result {
    iced::application(DivePlanner::boot, DivePlanner::update, DivePlanner::view)
        .theme(DivePlanner::theme)
        .antialiasing(true)
        .settings(Settings {
            id: Some("Bubbles Dive Planner".to_string()),
            ..Default::default()
        })
        .run()
}
