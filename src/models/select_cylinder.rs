use crate::commands::selectable_cylinder::SelectableCylinder;
use super::cylinder::Cylinder;
use serde::{Deserialize, Serialize};

#[derive(Debug, Default, Clone, PartialEq, Serialize, Deserialize)]
pub struct SelectCylinder {
    cylinders: [Cylinder; 3],
    pub selected_cylinder: Option<SelectableCylinder>,
}
