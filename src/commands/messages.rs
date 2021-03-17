use super::selectable_dive_model::SelectableDiveModel;

#[derive(Debug, Clone)]
pub enum Message {
    MenuBar,
    FileNew,
    FileSave,
    FileLoad,
    EditUndo,
    EditRedo,
    DiveModelSelected(SelectableDiveModel),
    DepthChanged(String),
    TimeChanged(String),
    CylinderVolumeChanged(String),
    CylinderPressureChanged(String),
    SurfaceAirConsumptionChanged(String),
    OxygenChanged(String),
    HeliumChanged(String),
    CalculateDivePlan,
}
