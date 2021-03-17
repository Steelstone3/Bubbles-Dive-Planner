use iced::{Sandbox, Settings};
use view_models::dive_planner::DivePlanner;

mod commands;
mod controllers;
mod models;
mod view_models;
mod views;

// TODO Make a results view
// TODO Add a tab for historic results
// TODO This will require an update to dive_planner to include historic_dive_stages Vec<DiveStage>
// TODO On calculate the latest result will be pushed
// TODO On save and load file use a "DivePlanner" which is basically the application state

// TODO Add depth, time, gas mixuture and gas management information to each dive result
// TODO E.G
// | Dive Step #1 |
// | Depth: 50, Time: 10 |
// | Gas Mix: 21% O2, 10% He, 69% N |
// | Gas Management: 2400 Initial, 1680 Remaining, 720 Used |
// <Results Here>

// TODO Add multiple dive models

// TODO Theme and style it
pub fn main() -> iced::Result {
    DivePlanner::run(Settings::default())
}
