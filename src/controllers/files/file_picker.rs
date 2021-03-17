use rfd::FileDialog;

pub fn select_file_to_load() -> String {
    let file = FileDialog::new().add_filter("json", &["json"]).pick_file();

    match file {
        Some(path_buf) => match path_buf.into_os_string().into_string() {
            Ok(file_path) => file_path.to_string(),
            Err(_) => "".to_string(),
        },
        None => "".to_string(),
    }
}

pub fn save_file_location() -> String {
    let file = FileDialog::new().add_filter("json", &["json"]).save_file();

    match file {
        Some(path_buf) => match path_buf.into_os_string().into_string() {
            Ok(file_path) => file_path.to_string(),
            Err(_) => "".to_string(),
        },
        None => "".to_string(),
    }
}
