use cosmic::iced::{Sandbox, Settings};
use view_models::dive_planner::DivePlanner;

mod commands;
mod controllers;
mod models;
mod view_models;
mod views;

pub fn main() -> Result<(), cosmic::iced::Error> {
    DivePlanner::run(Settings::default())
}
