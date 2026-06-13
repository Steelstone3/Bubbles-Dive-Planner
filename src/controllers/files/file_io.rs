use crate::models::application::dive_planner::DivePlannerFile;
use std::fs::File;
use std::io::{Read, Write};

pub fn upsert_dive_planner_state(file_name: &str, dive_planner_file: &DivePlannerFile) {
    let mut file = match File::create(file_name) {
        Ok(file) => file,
        Err(_) => return,
    };
    let toml = toml::ser::to_string_pretty(&dive_planner_file).unwrap_or_default();

    if let Ok(written_file) = write!(file, "{toml}") {
        written_file
    }
}

pub fn read_dive_planner_state(file_name: &str) -> DivePlannerFile {
    let contents = get_file_contents(file_name);

    toml::from_str(&contents).unwrap_or_default()
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
    use crate::controllers::files::test_file_guard::file_guard::TestFileGuard;
    use std::{env, fs};

    #[test]
    fn test_upsert_dive_planner_state() {
        // Given
        let dive_planner_state_file_name = "test_file_2.toml";
        let dive_planner_file = DivePlannerFile::default();

        // When
        let _guard = TestFileGuard::new(dive_planner_state_file_name);
        upsert_dive_planner_state(dive_planner_state_file_name, &dive_planner_file);

        // Then
        assert!(fs::metadata(dive_planner_state_file_name).is_ok());
    }

    #[test]
    fn test_read_dive_planner_state() {
        // Given
        let root = env::current_dir().unwrap_or_default();
        let file_name = root.to_string_lossy();
        let expected_dive_planner_file = DivePlannerFile::default();

        // When
        let _guard = TestFileGuard::new(&file_name);
        let dive_planner_file = read_dive_planner_state(&file_name);

        // Then
        assert_eq!(expected_dive_planner_file, dive_planner_file);
    }

    #[test]
    fn acceptance_test_save_and_load_dive_planner_state() {
        // Given
        let file_name = "test_file_3.toml";
        let expected_dive_planner_file = DivePlannerFile::default();

        // When
        let _guard = TestFileGuard::new(file_name);
        upsert_dive_planner_state(file_name, &expected_dive_planner_file);
        let dive_planner_file = read_dive_planner_state(file_name);

        // Then
        assert_eq!(expected_dive_planner_file, dive_planner_file);
    }
}
