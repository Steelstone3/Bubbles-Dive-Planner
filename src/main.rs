use iced::{Sandbox, Settings};
use view_models::dive_planner::DivePlanner;

mod commands;
mod controllers;
mod models;
mod view_models;
mod views;

// TODO Header Bar View
// TODO Theme and style it
// TODO Refactor The Calculate Button To A View
// TODO Save Functionality
// TODO Load Functionality
pub fn main() -> iced::Result {
    DivePlanner::run(Settings::default())
}
