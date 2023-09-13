use serde::{Deserialize, Serialize};

#[derive(Debug, Copy, Clone, Serialize, Deserialize, PartialEq, Eq)]
pub enum SelectableCylinder {
    Descend,
    Bottom,
    Decompression,
}

impl SelectableCylinder {
    pub const ALL: [SelectableCylinder; 3] = [
        SelectableCylinder::Descend,
        SelectableCylinder::Bottom,
        SelectableCylinder::Decompression,
    ];
}

impl std::fmt::Display for SelectableCylinder {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        write!(
            f,
            "{}",
            match self {
                SelectableCylinder::Descend => "Descend",
                SelectableCylinder::Bottom => "Bottom",
                SelectableCylinder::Decompression => "Decompression",
            }
        )
    }
}
