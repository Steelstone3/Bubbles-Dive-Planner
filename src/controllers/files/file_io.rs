use std::fs::File;
use std::io::{Read, Write};

use crate::models::{application::dive_planner::DivePlanner, plan::dive_stage::DiveStage};

pub fn upsert_dive_planner_state(file_name: &str, dive_planner: &DivePlanner) {
    let mut file = match File::create(file_name) {
        Ok(file) => file,
        Err(_) => return,
    };
    let json = serde_json::ser::to_string_pretty(&dive_planner).unwrap_or_default();

    if let Ok(written_file) = write!(file, "{json}") {
        written_file
    }
}

pub fn read_dive_planner_state(file_name: &str) -> DivePlanner {
    let contents = get_file_contents(file_name);

    serde_json::from_str(&contents).unwrap_or_default()
}

pub fn upsert_dive_results(file_name: &str, dive_stages: &Vec<DiveStage>) {
    let mut file = match File::create(file_name) {
        Ok(file) => file,
        Err(_) => return,
    };

    for dive_stage in dive_stages {
        let json = serde_json::ser::to_string_pretty(&dive_stage).unwrap_or_default();

        match write!(file, "{json}") {
            Ok(written_file) => written_file,
            Err(_) => return,
        }
    }
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
    fn save_dive_results_file() {
        // Given
        let dive_plan = "test_file_1.json";
        let results = vec![DiveStage::default()];

        // When
        upsert_dive_results(dive_plan, &results);

        // Then
        assert!(fs::metadata(dive_plan).is_ok());
        assert!(fs::metadata(dive_plan).unwrap().len() != 0);
    }

    #[test]
    fn save_dive_planner_state_file() {
        // Given
        let dive_planner_state_file_name = "test_file_2.json";
        let dive_planner = DivePlanner::default();

        // When
        upsert_dive_planner_state(dive_planner_state_file_name, &dive_planner);

        // Then
        assert!(fs::metadata(dive_planner_state_file_name).is_ok());
        assert!(fs::metadata(dive_planner_state_file_name).unwrap().len() != 0);
    }

    #[test]
    fn handle_loading_an_empty_dive_planner_state() {
        // Given
        let file_name = "non_existant_file.json";
        let expected_dive_planner = DivePlanner::default();

        // When
        let dive_planner = read_dive_planner_state(file_name);

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }

    #[test]
    fn create_a_file_saving_and_loading_dive_planner_state() {
        // Given
        let file_name = "test_file_3.json";
        let expected_dive_planner = DivePlanner::default();

        // When
        upsert_dive_planner_state(file_name, &expected_dive_planner);
        let dive_planner = read_dive_planner_state(file_name);

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }
}
