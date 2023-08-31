use iced::{Sandbox, Settings};
use view_models::dive_planner::DivePlanner;

mod commands;
mod controllers;
mod models;
mod view_models;
mod views;

// TODO Theme and style it
pub fn main() -> iced::Result {
    DivePlanner::run(Settings::default())
}
