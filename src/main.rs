mod application;
mod controllers;
mod models;
mod test_fixture;
mod views;

pub fn main() -> iced::Result {
    #[cfg(not(test))]
    {
        use iced::Settings;
        use models::application::dive_planner::DivePlanner;

        iced::application(DivePlanner::boot, DivePlanner::update, DivePlanner::view)
            .theme(DivePlanner::theme)
            .antialiasing(true)
            .settings(Settings {
                id: Some("Bubbles Dive Planner".to_string()),
                ..Default::default()
            })
            .run()
    }
    #[cfg(test)]
    {
        Ok(())
    }
}
