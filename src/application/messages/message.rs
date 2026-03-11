use crate::application::states::{
    selectable_dive_model::SelectableDiveModel, tab_identifier::TabIdentifier,
};

#[derive(Debug, Clone, PartialEq, Eq)]
pub enum Message {
    MenuBar,
    FileOnNewClicked,
    FileOnSaveRequested,
    FileOnSaveCompleted,
    FileOnLoadRequested,
    FileOnLoadCompleted,
    EditOnUndoClicked,
    EditOnRedoClicked,
    ViewOnToggleThemeClicked,
    TabSelectionOnSelect(TabIdentifier),
    DiveModelSelectionOnSelect(SelectableDiveModel),
    DepthOnChanged(String),
    TimeOnChanged(String),
    CylinderVolumeOnChanged(String),
    CylinderPressureOnChanged(String),
    SurfaceAirConsumptionOnChanged(String),
    OxygenOnChanged(String),
    HeliumOnChanged(String),
    DiveProfileOnClicked,
    DecompressionProfileOnClicked,
}
