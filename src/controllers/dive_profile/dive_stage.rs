use crate::{application::states::selectable_dive_model::SelectableDiveModel, models::{application::dive_planner::DivePlanner, plan::dive_model::DiveModel, result::dive_profile::DiveProfile}};

impl DivePlanner {
 pub fn dive_model_selected(&mut self, selectable_dive_model: SelectableDiveModel) {
        match selectable_dive_model {
            SelectableDiveModel::Bulhmann => {
                self.dive_planning.select_dive_model.selected_dive_model =
                    Some(SelectableDiveModel::Bulhmann);
                self.dive_stage.dive_model = DiveModel::create_zhl16_dive_model()
            }
            SelectableDiveModel::Usn => {
                self.dive_planning.select_dive_model.selected_dive_model =
                    Some(SelectableDiveModel::Usn);
                self.dive_stage.dive_model = DiveModel::create_usn_rev_6_dive_model()
            }
        }
    }

    // TODO test
    pub fn update_dive_profile(&self) -> DiveProfile {
        todo!()
    }
}
