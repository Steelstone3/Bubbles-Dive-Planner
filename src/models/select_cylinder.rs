use super::cylinder::Cylinder;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Serialize, Deserialize)]
pub struct SelectCylinder {
    cylinders: Vec<Cylinder>,
}

impl Default for SelectCylinder {
    fn default() -> Self {
        Self {
            cylinders: vec![Cylinder::default()],
        }
    }
}

impl SelectCylinder {
    // TODO Test this
    pub fn add_cylinder(&mut self) {
        self.cylinders.push(Cylinder::default())
    }
}
