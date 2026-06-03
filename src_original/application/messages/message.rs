use crate::application::states::{
    selectable_dive_model::SelectableDiveModel, tab_identifier::TabIdentifier,
};

#[derive(Debug, Clone, PartialEq, Eq)]
pub enum Message {
    MenuBar,
    FileOnNewClicked,
    #[allow(dead_code)]
    FileOnSaveRequested,
    FileOnSaveCompleted(Option<String>),
    #[allow(dead_code)]
    FileOnLoadRequested,
    FileOnLoadCompleted(Option<String>),
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
