use iced::Task;
use rfd::AsyncFileDialog;

pub fn select_file_to_load() -> Task<Option<String>> {
    Task::future(async {
        let file = AsyncFileDialog::new()
            .add_filter("toml", &["toml"])
            .pick_file()
            .await;

        file.and_then(|handle| handle.path().to_str().map(|s| s.to_string()))
    })
}

pub fn save_file_location() -> Task<Option<String>> {
    Task::future(async {
        let file = AsyncFileDialog::new()
            .add_filter("toml", &["toml"])
            .save_file()
            .await;

        file.and_then(|handle| handle.path().to_str().map(|s| s.to_string()))
    })
}
