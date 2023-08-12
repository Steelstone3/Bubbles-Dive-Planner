#[derive(Debug, Clone)]
pub enum Message {
    DepthChanged(String),
    TimeChanged(String),
    CylinderVolumeChanged(String),
    CylinderPressureChanged(String),
    SurfaceAirConsumptionChanged(String),
    OxygenChanged(String),
    HeliumChanged(String),
    CalculateDivePlan,
}
