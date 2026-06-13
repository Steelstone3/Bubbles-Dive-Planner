use crate::{application::input_parser::parse_input_u32, models::plan::dive_step::DiveStep};
use serde::{Deserialize, Serialize};

pub const MAXIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE: u32 = 30;
pub const MINIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE: u32 = 3;

#[derive(PartialEq, Debug, Default, Clone, Serialize, Deserialize)]
pub struct GasManagement {
    remaining: u32,
    used: u32,
    surface_air_consumption_rate: u32,
}

impl GasManagement {
    pub fn new(
        initial_pressurised_cylinder_volume: u32,
        used: u32,
        surface_air_consumption_rate: u32,
    ) -> Self {
        Self {
            remaining: initial_pressurised_cylinder_volume,
            used,
            surface_air_consumption_rate,
        }
    }

    pub fn is_valid(&self) -> bool {
        if self.surface_air_consumption_rate > MAXIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE
            || self.surface_air_consumption_rate < MINIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE
        {
            return false;
        }

        true
    }

    pub fn update_surface_air_consumption_rate(
        &self,
        surface_air_consumption_rate: String,
    ) -> Self {
        let surface_air_consumption_rate = parse_input_u32(
            surface_air_consumption_rate,
            MINIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE,
            MAXIMUM_SURFACE_AIR_CONSUMPTION_RATE_VALUE,
        );

        GasManagement::new(self.remaining, self.used, surface_air_consumption_rate)
    }

    pub fn update_gas_management(&self, dive_step: &DiveStep) -> GasManagement {
        let pressure_at_depth = (dive_step.depth / 10) + 1;
        let used = pressure_at_depth * dive_step.time * self.surface_air_consumption_rate;

        if used > self.remaining {
            // is out of air
            Self::new(0, used, self.surface_air_consumption_rate)
        } else {
            let remaining = self.remaining - used;
            Self::new(remaining, used, self.surface_air_consumption_rate)
        }
    }

    pub fn get_remaining(&self) -> u32 {
        self.remaining
    }

    pub fn get_used(&self) -> u32 {
        self.used
    }

    pub fn get_surface_air_consumption_rate(&self) -> u32 {
        self.surface_air_consumption_rate
    }
}

#[cfg(test)]
mod gas_management_should {
    use crate::models::plan::{cylinders::gas_management::GasManagement, dive_step::DiveStep};
    use rstest::rstest;

    #[rstest]
    #[case(12, true)]
    #[case(31, false)]
    #[case(2, false)]
    fn test_validate_gas_management(
        #[case] surface_air_consumption_rate: u32,
        #[case] expected_is_valid: bool,
    ) {
        // given
        let gas_management = GasManagement::new(0, 0, surface_air_consumption_rate);

        // when
        let is_valid = gas_management.is_valid();

        // then
        pretty_assertions::assert_eq!(expected_is_valid, is_valid);
    }

    #[test]
    fn test_update_surface_air_consumption_rate() {
        // given
        let expected_surface_air_consumption = "15".to_string();
        let gas_management = GasManagement::new(2400, 0, 12);
        let expected_gas_management = GasManagement::new(2400, 0, 15);

        // when
        let gas_management =
            gas_management.update_surface_air_consumption_rate(expected_surface_air_consumption);

        // then
        pretty_assertions::assert_eq!(expected_gas_management, gas_management);
    }

    #[test]
    fn test_update_gas_management() {
        // given
        let dive_step = DiveStep::new(50, 10);
        let original_gas_management = GasManagement::new(2400, 0, 12);
        let expected_gas_management = GasManagement::new(1680, 720, 12);

        // when
        let gas_management = original_gas_management.update_gas_management(&dive_step);

        // then
        pretty_assertions::assert_eq!(expected_gas_management, gas_management);
    }

    #[test]
    fn test_update_gas_management_out_of_air() {
        // given
        let dive_step = DiveStep::new(50, 10);
        let original_gas_management = GasManagement::new(719, 0, 12);
        let expected_gas_management = GasManagement::new(0, 720, 12);

        // when
        let gas_management = original_gas_management.update_gas_management(&dive_step);

        // then
        pretty_assertions::assert_eq!(expected_gas_management, gas_management);
    }

    #[test]
    fn test_get_remaining() {
        // given
        let expected_remaining = 2400;
        let gas_management =
            GasManagement::new(expected_remaining, Default::default(), Default::default());

        // when
        let remaining = gas_management.get_remaining();

        // then
        pretty_assertions::assert_eq!(expected_remaining, remaining);
    }

    #[test]
    fn test_get_used() {
        // given
        let expected_used = 720;
        let gas_management =
            GasManagement::new(Default::default(), expected_used, Default::default());

        // when
        let used = gas_management.get_used();

        // then
        pretty_assertions::assert_eq!(expected_used, used);
    }

    #[test]
    fn test_get_surface_air_consumption() {
        // given
        let expected_surface_air_consumption = 12;
        let gas_management = GasManagement::new(
            Default::default(),
            Default::default(),
            expected_surface_air_consumption,
        );

        // when
        let surface_air_consumption = gas_management.get_surface_air_consumption_rate();

        // then
        pretty_assertions::assert_eq!(expected_surface_air_consumption, surface_air_consumption);
    }
}
