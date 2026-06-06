use crate::application::input_parser::parse_input_u32;
use serde::{Deserialize, Serialize};

const MAXIMUM_OXYGEN_VALUE: u32 = 100;
const MAXIMUM_HELIUM_VALUE: u32 = 100;
const MINIMUM_OXYGEN_VALUE: u32 = 5;
const MINIMUM_HELIUM_VALUE: u32 = 0;

#[derive(PartialEq, Debug, Clone, Serialize, Deserialize)]
pub struct GasMixture {
    pub oxygen: u32,
    pub helium: u32,
    nitrogen: u32,
    maximum_operating_depth: f32,
}

impl Default for GasMixture {
    fn default() -> Self {
        Self {
            oxygen: 0,
            helium: 0,
            nitrogen: 100,
            maximum_operating_depth: 0.0,
        }
    }
}

impl GasMixture {
    // TODO test
    pub fn new(oxygen: u32, helium: u32) -> Self {
        Self {
            oxygen,
            helium,
            nitrogen: GasMixture::update_nitrogen(oxygen, helium),
            maximum_operating_depth: GasMixture::calculate_maximum_operating_depth(oxygen),
        }
    }

    // TODO test
    pub fn update_oxygen(&self, oxygen: String) -> Self {
        let oxygen = parse_input_u32(
            oxygen,
            MINIMUM_OXYGEN_VALUE,
            MAXIMUM_OXYGEN_VALUE - self.helium,
        );

        Self::new(oxygen, self.helium)
    }

    // TODO test
    pub fn update_helium(&self, helium: String) -> Self {
        let helium = parse_input_u32(
            helium,
            MINIMUM_HELIUM_VALUE,
            MAXIMUM_HELIUM_VALUE - self.oxygen,
        );

        Self::new(self.oxygen, helium)
    }

    // TODO test
    pub fn get_nitrogen(&self) -> u32 {
        self.nitrogen
    }

    // TODO test
    pub fn get_maximum_operating_depth(&self) -> f32 {
        self.maximum_operating_depth
    }

    fn update_nitrogen(oxygen: u32, helium: u32) -> u32 {
        100 - oxygen - helium
    }

    fn calculate_maximum_operating_depth(oxygen: u32) -> f32 {
        const TOLERATED_PARTIAL_PRESSURE: f32 = 1.4;
        let oxygen_partial_pressure = oxygen as f32 / 100.0;
        let tolerated_pressure = TOLERATED_PARTIAL_PRESSURE / oxygen_partial_pressure;

        (tolerated_pressure * 10.0) - 10.0
    }

    pub fn is_valid(&self) -> bool {
        if self.oxygen > MAXIMUM_OXYGEN_VALUE {
            return false;
        } else if self.oxygen < MINIMUM_OXYGEN_VALUE {
            return false;
        } else if self.helium > MAXIMUM_HELIUM_VALUE {
            return false;
        } else if self.helium < MINIMUM_HELIUM_VALUE {
            return false;
        }

        true
    }
}

#[cfg(test)]
mod gas_mixture_should {}
