use super::{selectable_cylinder::SelectableCylinder, selectable_dive_model::SelectableDiveModel};

#[derive(Debug, Clone)]
pub enum Message {
    // PaneDragged(pane_grid::DragEvent),
    // PaneResized(pane_grid::ResizeEvent),
    MenuBar,
    FileNew,
    FileSave,
    FileLoad,
    EditUndo,
    EditRedo,
    ViewToggleSelectCylinderVisibility,
    DiveModelSelected(SelectableDiveModel),
    DepthChanged(String),
    TimeChanged(String),
    CylinderVolumeChanged(String),
    CylinderPressureChanged(String),
    SurfaceAirConsumptionChanged(String),
    OxygenChanged(String),
    HeliumChanged(String),
    CylinderSelected(SelectableCylinder),
    UpdateCylinderSelected(SelectableCylinder),
    UpdateDiveProfile,
    DecompressionUpdateDiveProfile,
}
