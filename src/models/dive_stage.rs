use super::{cylinder::Cylinder, dive_model::DiveModel, dive_step::DiveStep};
use crate::controllers::dive_stage::run_dive_profile;
use serde::{Deserialize, Serialize};

#[derive(Clone, Copy, Serialize, Deserialize)]
pub struct DiveStage {
    pub dive_model: DiveModel,
    pub dive_step: DiveStep,
    pub selected_cylinder: Cylinder,
}

impl DiveStage {
    pub fn new() -> Self {
        Self {
            dive_model: DiveModel::select(),
            dive_step: DiveStep::default(),
            selected_cylinder: Cylinder::default(),
        }
    }

    pub fn update(mut self, cylinders: Vec<Cylinder>) -> Self {
        self.dive_step = DiveStep::new();
        self.selected_cylinder = Cylinder::select(cylinders);

        self.dive_model.dive_profile =
            run_dive_profile(self.dive_model, self.dive_step, self.selected_cylinder);
        self.selected_cylinder = Cylinder::update_gas_usage(self.selected_cylinder, self.dive_step);

        self
    }

    pub fn print_result(&self) {
        println!("{}", self.dive_model.dive_profile);
        println!("{}", self.selected_cylinder);
    }
}
