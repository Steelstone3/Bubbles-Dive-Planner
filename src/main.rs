use iced::{Sandbox, Settings};
use view_models::dive_planner::DivePlanner;

mod commands;
mod controllers;
mod models;
mod view_models;
mod views;
mod test_fixture;

pub fn main() -> iced::Result {
    DivePlanner::run(Settings::default())
}
