use serde::{Deserialize, Serialize};

#[derive(Debug, Copy, Clone, Serialize, Deserialize, PartialEq, Eq)]
pub enum SelectableDiveModel {
    Bulhmann,
    Usn,
}

impl SelectableDiveModel {
    pub const ALL: [SelectableDiveModel; 2] =
        [SelectableDiveModel::Bulhmann, SelectableDiveModel::Usn];
}

impl std::fmt::Display for SelectableDiveModel {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        write!(
            f,
            "{}",
            match self {
                SelectableDiveModel::Bulhmann => "Zhl16",
                SelectableDiveModel::Usn => "USN Rev 6",
            }
        )
    }
}

#[cfg(test)]
mod selectable_dive_model_should {
    use super::*;

    #[test]
    fn display_selectable_dive_models() {
        assert_eq!("Zhl16", SelectableDiveModel::Bulhmann.to_string());
        assert_eq!("USN Rev 6", SelectableDiveModel::Usn.to_string());
    }
}
