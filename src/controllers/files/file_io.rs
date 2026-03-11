use crate::models::application::dive_planner::DivePlannerFile;
use std::fs::File;
use std::io::{Read, Write};

pub fn upsert_dive_planner_state(file_name: &str, dive_planner_file: &DivePlannerFile) {
    let mut file = match File::create(file_name) {
        Ok(file) => file,
        Err(_) => return,
    };
    let json = serde_json::ser::to_string_pretty(&dive_planner_file).unwrap_or_default();

    if let Ok(written_file) = write!(file, "{json}") {
        written_file
    }
}

pub fn read_dive_planner_state(file_name: &str) -> DivePlannerFile {
    let contents = get_file_contents(file_name);

    serde_json::from_str(&contents).unwrap_or_default()
}

fn get_file_contents(file_name: &str) -> String {
    let mut contents = String::new();

    if let Ok(mut file) = File::open(file_name) {
        match file.read_to_string(&mut contents) {
            Ok(_) => return contents,
            Err(_) => String::new(),
        };
    }

    contents
}

#[cfg(test)]
mod file_integration_should {
    use super::*;
    use std::fs;

    #[test]
    fn save_dive_planner_state_file() {
        // Given
        let dive_planner_state_file_name = "test_file_2.json";
        let dive_planner_file = DivePlannerFile::default();

        // When
        upsert_dive_planner_state(dive_planner_state_file_name, &dive_planner_file);

        // Then
        assert!(fs::metadata(dive_planner_state_file_name).is_ok());
        assert!(fs::metadata(dive_planner_state_file_name).unwrap().len() != 0);
    }

    #[test]
    fn handle_loading_an_empty_dive_planner_state() {
        // Given
        let file_name = "non_existant_file.json";
        let expected_dive_planner_file = DivePlannerFile::default();

        // When
        let dive_planner_file = read_dive_planner_state(file_name);

        // Then
        assert_eq!(expected_dive_planner_file, dive_planner_file);
    }

    #[test]
    fn create_a_file_saving_and_loading_dive_planner_state() {
        // Given
        let file_name = "test_file_3.json";
        let expected_dive_planner_file = DivePlannerFile::default();

        // When
        upsert_dive_planner_state(file_name, &expected_dive_planner_file);
        let dive_planner_file = read_dive_planner_state(file_name);

        // Then
        assert_eq!(expected_dive_planner_file, dive_planner_file);
    }
}
