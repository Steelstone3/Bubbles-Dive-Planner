use crate::models::cylinder::Cylinder;
use crate::models::dive_stage::DiveStage;
use crate::presenters::presenter::{confirmation, text_prompt};
use std::fs::File;
use std::io::{self, Read, Write};

#[derive(Default)]
pub struct FileController {
    dive_plan_file_name: String,
    cylinders_file_name: String,
}

impl FileController {
    pub fn load_dive_plan(&mut self) -> DiveStage {
        if confirmation("Load Dive Plan?") {
            self.dive_plan_file_name =
                text_prompt("Enter a filename:", "For example: dive_plan", "dive_plan") + ".json";
            self.read_latest_dive_stage()
        } else {
            self.dive_plan_file_name = String::default();
            DiveStage::new()
        }
    }

    pub fn load_cylinders(&mut self) -> Vec<Cylinder> {
        if confirmation("Load Cylinders?") {
            if self.dive_plan_file_name.is_empty() {
                self.cylinders_file_name = "cylinders_only.json".to_string();
            } else {
                self.cylinders_file_name = "cylinders_".to_owned() + &self.dive_plan_file_name;
            }

            let mut cylinders = self.read_cylinders();

            if cylinders.is_empty() {
                cylinders = Cylinder::new_collection();
                self.upsert_cylinders(&cylinders).unwrap();
            }

            cylinders
        } else {
            self.cylinders_file_name = String::default();
            Cylinder::new_collection()
        }
    }

    pub fn upsert_dive_stage(&self, dive_stages: &Vec<DiveStage>) -> std::io::Result<()> {
        let is_using_file = self.dive_plan_file_name != String::default();

        if is_using_file {
            let mut file = self.create_file(&self.dive_plan_file_name);
            let json = serde_json::ser::to_string_pretty(&dive_stages)
                .expect("Can't parse application data to string");
            write!(file, "{}", json).expect("Can't update file with application data");
        }

        Ok(())
    }

    pub fn upsert_cylinders(&self, cylinders: &Vec<Cylinder>) -> std::io::Result<()> {
        let is_using_file = self.cylinders_file_name != String::default();

        if is_using_file {
            let mut file = self.create_file(&self.cylinders_file_name);
            let json = serde_json::ser::to_string_pretty(&cylinders)
                .expect("Can't parse application data to string");
            write!(file, "{}", json).expect("Can't update file with application data");
        }

        Ok(())
    }

    pub fn read_latest_dive_stage(&self) -> DiveStage {
        let contents = self.get_file_contents(&self.dive_plan_file_name);
        let dive_stages = FileController::parse_dive_plan_to_application_data(&contents);

        if dive_stages.is_empty() {
            DiveStage::new()
        } else {
            *dive_stages.last().expect("File content is empty")
        }
    }

    pub fn read_cylinders(&self) -> Vec<Cylinder> {
        let contents = self.get_file_contents(&self.cylinders_file_name);
        FileController::parse_cylinders_to_application_data(&contents)
    }

    fn create_file(&self, file_name: &String) -> File {
        File::create(file_name).expect("Can't create file")
    }

    fn get_file_contents(&self, file_name: &String) -> String {
        let mut contents = String::new();

        if let Ok(mut file) = self.open_file(&(file_name.to_owned())) {
            file.read_to_string(&mut contents).expect("Can't read file");
        }

        contents
    }

    fn open_file(&self, file_name: &String) -> io::Result<File> {
        File::open(file_name)
    }

    fn parse_dive_plan_to_application_data(contents: &String) -> Vec<DiveStage> {
        if contents.is_empty() {
            return Vec::new();
        }

        serde_json::from_str(contents).expect("Can't parse file contents to application data")
    }

    fn parse_cylinders_to_application_data(contents: &String) -> Vec<Cylinder> {
        if contents.is_empty() {
            return Vec::new();
        }

        serde_json::from_str(contents).expect("Can't parse file contents to application data")
    }
}
