use iced::{Sandbox, Settings};
use view_models::dive_planner::DivePlanner;

mod commands;
mod controllers;
mod models;
mod test_fixture;
mod view_models;
mod views;

pub fn main() -> iced::Result {
    DivePlanner::run(Settings::default())
}
