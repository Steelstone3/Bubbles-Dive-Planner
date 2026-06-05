use serde::{Deserialize, Serialize};

#[derive(PartialEq, Debug, Default, Clone, Serialize, Deserialize)]
pub struct AmbientPressure {
    oxygen_at_pressure: f32,
    helium_at_pressure: f32,
    nitrogen_at_pressure: f32,
}

impl AmbientPressure {
    pub fn new(
        oxygen_at_pressure: f32,
        helium_at_pressure: f32,
        nitrogen_at_pressure: f32,
    ) -> Self {
        Self {
            oxygen_at_pressure,
            helium_at_pressure,
            nitrogen_at_pressure,
        }
    }

    #[allow(dead_code)]
    pub fn get_oxygen_at_pressure(&self) -> f32 {
        self.oxygen_at_pressure
    }

    pub fn get_helium_at_pressure(&self) -> f32 {
        self.helium_at_pressure
    }

    pub fn get_nitrogen_at_pressure(&self) -> f32 {
        self.nitrogen_at_pressure
    }
}
