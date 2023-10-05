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

#[cfg(test)]
mod selectable_cylinder_should {
    use super::*;

    #[test]
    fn display_selectable_cylinders() {
        assert_eq!("Bottom", SelectableCylinder::Bottom.to_string());
        assert_eq!("Decompression", SelectableCylinder::Decompression.to_string());
        assert_eq!("Descend", SelectableCylinder::Descend.to_string());
    }
}
