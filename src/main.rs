use iced::Settings;
use models::dive_planner::DivePlanner;

mod commands;
mod controllers;
mod models;
mod test_fixture;
mod views;

pub fn main() -> iced::Result {
    iced::application(
        "Bubbles Dive Planner",
        DivePlanner::update,
        DivePlanner::view,
    )
    // .theme(DivePlanner::theme)
    .antialiasing(true)
    .settings(Settings::default())
    .run()
}
