use super::{
    selectable_cylinder::SelectableCylinder, selectable_dive_model::SelectableDiveModel,
    tab_identifier::TabIdentifier,
};

#[derive(Debug, Clone, PartialEq, Eq)]
pub enum Message {
    MenuBar,
    FileNew,
    FileSave,
    FileLoad,
    EditUndo,
    EditRedo,
    ViewToggleTheme,
    ViewToggleSelectedCylinderVisibility,
    SelectedTabChanged(TabIdentifier),
    SelectedDiveModelChanged(SelectableDiveModel),
    DepthChanged(String),
    TimeChanged(String),
    CylinderVolumeChanged(String),
    CylinderPressureChanged(String),
    SurfaceAirConsumptionChanged(String),
    OxygenChanged(String),
    HeliumChanged(String),
    SelectedCylinderChanged(SelectableCylinder),
    UpdateSelectedCylinder(SelectableCylinder),
    UpdateDiveProfile,
    DecompressionUpdateDiveProfile,
}
