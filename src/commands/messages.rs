use super::{
    selectable_cylinder::SelectableCylinder, selectable_dive_model::SelectableDiveModel,
    tab_identifier::TabIdentifier,
};

#[derive(Debug, Clone, PartialEq, Eq)]
pub enum Message {
    MenuBar,
    TabSelected(TabIdentifier),
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
