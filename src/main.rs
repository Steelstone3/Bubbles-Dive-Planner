use iced::{Sandbox, Settings};
use view_models::dive_planner::DivePlanner;

mod commands;
mod controllers;
mod models;
mod view_models;
mod views;

// TODO Update README.md
// TODO Header Bar View
// TODO Theme and style it
// TODO Refactor The Calculate Button To A View
// TODO Improve results output 3.S.F as a starting point
// TODO Save Functionality
// TODO Load Functionality
pub fn main() -> iced::Result {
    DivePlanner::run(Settings::default())
}
