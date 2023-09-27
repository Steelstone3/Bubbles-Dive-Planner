use crate::models::dive_stage::DiveStage;
use crate::view_models::dive_planner::DivePlanner;
use std::fs::File;
use std::io::{Read, Write};

pub fn upsert_dive_planner_state(file_name: &str, dive_planner: &DivePlanner) {
    let mut file = File::create(file_name).expect("Can't create file.");
    let json = serde_json::ser::to_string_pretty(&dive_planner)
        .expect("Can't parse application data to string");
    write!(file, "{}", json).expect("Can't update file with application data");
}

pub fn read_dive_planner_state(file_name: &str) -> DivePlanner {
    let contents = get_file_contents(file_name);

    // TODO Test contents empty
    if contents.is_empty() {
        return DivePlanner::default();
    }

    serde_json::from_str(&contents).expect("Can't parse file contents to application data")
}

// TODO test
pub fn upsert_dive_results(file_name: &str, dive_stages: &Vec<DiveStage>) {
    let mut file = File::create(file_name).expect("Can't create file.");

    for dive_stage in dive_stages {
        let json = serde_json::ser::to_string_pretty(&dive_stage)
            .expect("Can't parse application data to string");
        write!(file, "{}", json).expect("Can't update file with application data");
    }
}

fn get_file_contents(file_name: &str) -> String {
    let mut contents = String::new();

    if let Ok(mut file) = File::open(file_name) {
        file.read_to_string(&mut contents).expect("Can't read file");
    }

    contents
}

#[cfg(test)]
mod file_integration_should {
    use super::*;

    #[test]
    fn create_a_file_saving_and_loading_dive_planner_state() {
        // Given
        let file_name = "test_file.json";
        let expected_dive_planner = DivePlanner::default();

        // When
        upsert_dive_planner_state(file_name, &expected_dive_planner);
        let dive_planner = read_dive_planner_state(file_name);

        // Then
        assert_eq!(expected_dive_planner, dive_planner);
    }
}
