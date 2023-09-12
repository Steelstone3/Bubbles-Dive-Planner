use super::cylinder::Cylinder;
use serde::{Deserialize, Serialize};

#[derive(Debug, Default, Clone, PartialEq, Serialize, Deserialize)]
pub struct SelectCylinder {
    cylinders: Vec<Cylinder>,
}

impl SelectCylinder {
    // TODO Test this
    pub fn add_cylinder(&mut self) {
        self.cylinders.push(Cylinder::default())
    }
}

