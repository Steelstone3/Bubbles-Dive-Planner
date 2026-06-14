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

    #[cfg(test)]
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

#[cfg(test)]
mod ambient_pressure_should {
    use crate::models::plan::dive_profile_result::ambient_pressure::AmbientPressure;

    #[test]
    fn test_get_oxygen_at_pressure() {
        // given
        let ambient_pressure = AmbientPressure::new(23.4, Default::default(), Default::default());

        // when
        let oxygen_at_pressure = ambient_pressure.get_oxygen_at_pressure();

        // then
        pretty_assertions::assert_eq!(23.4, oxygen_at_pressure)
    }

    #[test]
    fn test_get_helium_at_pressure() {
        // given
        let ambient_pressure = AmbientPressure::new(Default::default(), 23.4, Default::default());

        // when
        let oxygen_at_pressure = ambient_pressure.get_helium_at_pressure();

        // then
        pretty_assertions::assert_eq!(23.4, oxygen_at_pressure)
    }

    #[test]
    fn test_get_nitrogen_at_pressure() {
        // given
        let ambient_pressure = AmbientPressure::new(Default::default(), Default::default(), 23.4);

        // when
        let oxygen_at_pressure = ambient_pressure.get_nitrogen_at_pressure();

        // then
        pretty_assertions::assert_eq!(23.4, oxygen_at_pressure)
    }
}
