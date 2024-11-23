use crate::{
    commands::selectable_dive_model::SelectableDiveModel, models::dive_profile::DiveProfile,
    view_models::dive_planner::DivePlanner,
};

impl DivePlanner {
    pub fn dive_model_selected(&mut self, selectable_dive_model: SelectableDiveModel) {
        self.select_dive_model
            .select_dive_model(selectable_dive_model, &mut self.dive_stage.dive_model);
    }

    pub fn update_dive_profile(&mut self) {
        self.select_cylinder
            .assign_cylinder(self.dive_stage.cylinder);

        self.update_dive_stage();

        self.add_result();

        self.select_cylinder
            .assign_cylinder(self.dive_stage.cylinder);

        self.decompression_steps
            .assign_decompression_steps(self.dive_stage.calculate_decompression_dive_steps());

        self.update_visibility();
    }

    fn update_dive_stage(&mut self) {
        let dive_stage = DiveProfile::update_dive_profile(self.dive_stage);
        self.dive_stage = dive_stage;
    }

    fn add_result(&mut self) {
        self.dive_results.add_dive_result(self.dive_stage);
        self.redo_buffer = Default::default();
    }

    fn update_visibility(&mut self) {
        self.is_planning = true;

        // TODO AH depricate all the needless readonly and is visible flags
        self.select_cylinder.read_only_view();
        // TODO AH depricate all the needless readonly and is visible flags
        self.dive_results.is_visible = true;
    }
}
