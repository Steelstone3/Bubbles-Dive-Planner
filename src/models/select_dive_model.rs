use crate::commands::selectable_dive_model::SelectableDiveModel;

use super::dive_model::DiveModel;
use serde::{Deserialize, Serialize};

#[derive(Debug, PartialEq, Copy, Clone, Serialize, Deserialize)]
pub struct SelectDiveModel {
    pub dive_model_list: [DiveModel; 2],
    pub selected_dive_model: Option<SelectableDiveModel>,
}

impl Default for SelectDiveModel {
    fn default() -> Self {
        Self {
            dive_model_list: Default::default(),
            selected_dive_model: Some(SelectableDiveModel::Bulhmann),
        }
    }
}

impl SelectDiveModel {
    pub fn select_dive_model(
        &mut self,
        selectable_dive_model: SelectableDiveModel,
        dive_model: &mut DiveModel,
    ) {
        self.selected_dive_model = Some(selectable_dive_model);

        match selectable_dive_model {
            SelectableDiveModel::Bulhmann => *dive_model = DiveModel::create_zhl16_dive_model(),
            SelectableDiveModel::Usn => *dive_model = DiveModel::create_usn_rev_6_model(),
        }
    }
}

#[cfg(test)]
mod select_dive_model_should {
    use super::*;

    #[test]
    fn select_a_dive_model() {
        // Given
        let mut dive_model = DiveModel::create_zhl16_dive_model();
        let mut select_dive_model = SelectDiveModel {
            dive_model_list: Default::default(),
            selected_dive_model: Some(SelectableDiveModel::Usn),
        };

        // When
        select_dive_model.select_dive_model(
            select_dive_model.selected_dive_model.unwrap(),
            &mut dive_model,
        );

        // Then
        assert_eq!(DiveModel::create_usn_rev_6_model(), dive_model);
    }
}
