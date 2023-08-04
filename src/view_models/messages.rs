#[derive(Debug, Clone)]
pub enum Message {
    CalculateDivePlan,
    DepthChanged(String),
    TimeChanged(String),
    OxygenChanged(String),
    HeliumChanged(String),
}