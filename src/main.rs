use iced::{Sandbox, Settings};
use models::dive_planner::DivePlanner;

mod commands;
mod controllers;
mod models;
mod test_fixture;
mod views;

pub fn main() -> iced::Result {
    DivePlanner::run(Settings::default())
}
