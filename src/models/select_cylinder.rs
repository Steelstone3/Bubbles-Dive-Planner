use super::cylinder::Cylinder;
use serde::{Deserialize, Serialize};

#[derive(Debug, Default, Clone, PartialEq, Serialize, Deserialize)]
pub struct SelectCylinder {
    cylinders: Vec<Cylinder>,
}
