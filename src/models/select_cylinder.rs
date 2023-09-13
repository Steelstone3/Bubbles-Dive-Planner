use super::{cylinder::Cylinder, dive_stage::DiveStage};
use crate::commands::selectable_cylinder::{SelectableCylinder, self};
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Serialize, Deserialize)]
pub struct SelectCylinder {
    pub cylinders: [Cylinder; 3],
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

impl SelectCylinder {
    //TODO write a test
    pub fn update_cylinder_selected(&mut self,selectable_cylinder:SelectableCylinder, cylinder: Cylinder) {
        self.selected_cylinder = Some(selectable_cylinder);

        match selectable_cylinder {
            SelectableCylinder::Bottom => {
                self.selected_cylinder = Some(selectable_cylinder);
                self.cylinders[0] = cylinder;
            }
            SelectableCylinder::Decompression => {
                self.selected_cylinder = Some(selectable_cylinder);
                self.cylinders[1] = cylinder;
            }
            SelectableCylinder::Descend => {
                self.selected_cylinder = Some(selectable_cylinder);
                self.cylinders[2] = cylinder;
            }
        }
    }

    //TODO write a test
    pub fn on_cylinder_selected(&mut self,selectable_cylinder:SelectableCylinder, mut cylinder: Cylinder) -> Cylinder {
        self.selected_cylinder = Some(selectable_cylinder);

        match selectable_cylinder {
            SelectableCylinder::Bottom => {
                self.selected_cylinder = Some(selectable_cylinder);
                cylinder = self.cylinders[0]
            }
            SelectableCylinder::Decompression => {
                self.selected_cylinder = Some(selectable_cylinder);
                cylinder = self.cylinders[1]
            }
            SelectableCylinder::Descend => {
                self.selected_cylinder = Some(selectable_cylinder);
                cylinder = self.cylinders[2]
            }
        }

        cylinder
    }

    //TODO write a test
    pub fn assign_cylinder(&mut self, cylinder: Cylinder) {
        match self.selected_cylinder.unwrap() {
            SelectableCylinder::Bottom => {
                self.cylinders[0] = cylinder;
            }
            SelectableCylinder::Decompression => {
                self.cylinders[1] = cylinder;
            }
            SelectableCylinder::Descend => {
                self.cylinders[2] = cylinder;
            }
        }
    }

    //TODO write a test
    pub fn is_read_only(&mut self) {
        self.cylinders[0].is_read_only = true;
        self.cylinders[1].is_read_only = true;
        self.cylinders[2].is_read_only = true;
    }
}

#[cfg(test)]
mod select_cylinder_should {
    use super::*;
}
