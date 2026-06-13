use serde::{Deserialize, Serialize};

#[derive(Default, Debug, Clone, Serialize, Deserialize, PartialEq, Eq)]
pub enum SelectableDiveModel {
    #[default]
    BulhmannZhl16,
    UsnRevision6,
}

impl SelectableDiveModel {
    pub const ALL: [SelectableDiveModel; 2] = [
        SelectableDiveModel::BulhmannZhl16,
        SelectableDiveModel::UsnRevision6,
    ];
}

impl std::fmt::Display for SelectableDiveModel {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        write!(
            f,
            "{}",
            match self {
                SelectableDiveModel::BulhmannZhl16 => "Zhl16",
                SelectableDiveModel::UsnRevision6 => "USN Rev 6",
            }
        )
    }
}

#[cfg(test)]
mod selectable_dive_model_should {
    use super::*;

    #[test]
    fn display_selectable_dive_models() {
        assert_eq!("Zhl16", SelectableDiveModel::BulhmannZhl16.to_string());
        assert_eq!("USN Rev 6", SelectableDiveModel::UsnRevision6.to_string());
    }
}
