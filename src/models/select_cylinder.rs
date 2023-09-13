use super::cylinder::Cylinder;
use crate::commands::selectable_cylinder::SelectableCylinder;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Serialize, Deserialize)]
pub struct SelectCylinder {
    cylinders: [Cylinder; 3],
    pub selected_cylinder: Option<SelectableCylinder>,
}

impl Default for SelectCylinder {
    fn default() -> Self {
        Self {
            cylinders: Default::default(),
            selected_cylinder: Some(SelectableCylinder::Bottom),
        }
    }
}