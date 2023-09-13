use serde::{Deserialize, Serialize};

#[derive(Debug, Copy, Clone, Serialize, Deserialize, PartialEq, Eq)]
pub enum SelectableCylinder {
    Bottom,
    Decompression,
    Descend,
}

impl SelectableCylinder {
    pub const ALL: [SelectableCylinder; 3] = [
        SelectableCylinder::Bottom,
        SelectableCylinder::Decompression,
        SelectableCylinder::Descend,
    ];
}

impl std::fmt::Display for SelectableCylinder {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        write!(
            f,
            "{}",
            match self {
                SelectableCylinder::Bottom => "Bottom",
                SelectableCylinder::Decompression => "Decompression",
                SelectableCylinder::Descend => "Descend",
            }
        )
    }
}
