use super::{
    gas_mixture::GasMixtureView,
    input_parser::parse_input_u32,
    validation::validate_maximum,
};
use crate::{
    commands::messages::Message, models::cylinder::Cylinder, view_models::dive_planner::DivePlanner,
};
use iced::{
    widget::Text,
    widget::{text, text_input, TextInput},
};

pub struct CylinderView<'a> {
    pub cylinder_setup_text: Text<'a>,
    pub cylinder_volume_text: Text<'a>,
    pub cylinder_volume_input: TextInput<'a, Message>,
    pub cylinder_pressure_text: Text<'a>,
    pub cylinder_pressure_input: TextInput<'a, Message>,
    pub gas_mixture: GasMixtureView<'a>,
}

impl CylinderView<'_> {
    pub fn new(dive_planner: &DivePlanner) -> Self {
        Self {
            cylinder_setup_text: text("Cylinder Setup"),
            cylinder_volume_text: text("Volume (l)"),
            cylinder_volume_input: text_input(
                "Enter Cylinder Volume",
                &dive_planner.dive_stage.cylinder.volume.to_string(),
            )
            .on_input(Message::CylinderVolumeChanged),
            cylinder_pressure_text: text("Pressure (bar)"),
            cylinder_pressure_input: text_input(
                "Enter Cylinder Pressure",
                &dive_planner.dive_stage.cylinder.pressure.to_string(),
            )
            .on_input(Message::CylinderPressureChanged),
            gas_mixture: GasMixtureView::new(dive_planner),
        }
    }

    pub fn update_cylinder_volume(cylinder_volume: String, mut cylinder: Cylinder) -> Cylinder {
        let cylinder_volume_input = parse_input_u32(cylinder_volume, 3);
        cylinder.volume = validate_maximum(cylinder_volume_input, 30);
        cylinder.update_initial_pressurised_cylinder_volume();

        cylinder
    }

    pub fn update_cylinder_pressure(cylinder_pressure: String, mut cylinder: Cylinder) -> Cylinder {
        let cylinder_pressure_input = parse_input_u32(cylinder_pressure, 50);
        cylinder.pressure = validate_maximum(cylinder_pressure_input, 300);
        cylinder.update_initial_pressurised_cylinder_volume();

        cylinder
    }
}

#[cfg(test)]
mod cylinder_view_should {
    use crate::models::gas_management::GasManagement;

    use super::*;

    #[test]
    fn update_cylinder_volume_by_parsing_and_validating_input_successfully() {
        // Given
        let expected = Cylinder {
            volume: 12,
            ..Default::default()
        };
        let cylinder = Cylinder {
            ..Default::default()
        };
        let input = "12".to_string();

        // When
        let validated_cylinder_volume = CylinderView::update_cylinder_volume(input, cylinder);

        // Then
        assert_eq!(expected, validated_cylinder_volume);
    }

    #[test]
    fn updating_cylinder_volume_updates_initial_pressurised_cylinder_volume() {
        // Given
        let expected = Cylinder {
            volume: 12,
            pressure: 200,
            initial_pressurised_cylinder_volume: 2400,
            gas_management: GasManagement{remaining: 2400, ..Default::default()},
            ..Default::default()
        };
        let cylinder = Cylinder {
            pressure: 200,
            ..Default::default()
        };
        let input = "12".to_string();

        // When
        let validated_cylinder_volume = CylinderView::update_cylinder_volume(input, cylinder);

        // Then
        assert_eq!(expected, validated_cylinder_volume);
    }

    #[test]
    fn update_cylinder_volume_by_parsing_an_input_beyond_range() {
        // Given
        let expected = Cylinder {
            volume: 30,
            ..Default::default()
        };
        let cylinder = Cylinder {
            ..Default::default()
        };
        let input = "31".to_string();

        // When
        let validated_cylinder_volume = CylinderView::update_cylinder_volume(input, cylinder);

        // Then
        assert_eq!(expected, validated_cylinder_volume);
    }

    #[test]
    fn update_cylinder_volume_by_being_unable_to_parse_input() {
        // Given
        let expected = Cylinder {
            volume: 3,
            ..Default::default()
        };
        let cylinder = Cylinder {
            ..Default::default()
        };
        let input = "2£%^sdf".to_string();

        // When
        let validated_cylinder_volume = CylinderView::update_cylinder_volume(input, cylinder);

        // Then
        assert_eq!(expected, validated_cylinder_volume);
    }

    #[test]
    fn update_cylinder_pressure_by_parsing_and_validating_input_successfully() {
        // Given
        let expected = Cylinder {
            pressure: 200,
            ..Default::default()
        };
        let cylinder = Cylinder {
            ..Default::default()
        };
        let input = "200".to_string();

        // When
        let validated_cylinder_pressure = CylinderView::update_cylinder_pressure(input, cylinder);

        // Then
        assert_eq!(expected, validated_cylinder_pressure);
    }

    #[test]
    fn updating_cylinder_pressure_updates_initial_pressurised_cylinder_volume() {
        // Given
        let expected = Cylinder {
            volume: 12,
            pressure: 200,
            initial_pressurised_cylinder_volume: 2400,
            gas_management: GasManagement{remaining: 2400, ..Default::default()},
            ..Default::default()
        };
        let cylinder = Cylinder {
            volume: 12,
            ..Default::default()
        };
        let input = "200".to_string();

        // When
        let validated_cylinder_pressure = CylinderView::update_cylinder_pressure(input, cylinder);

        // Then
        assert_eq!(expected, validated_cylinder_pressure);
    }

    #[test]
    fn update_cylinder_pressure_by_parsing_an_input_beyond_range() {
        // Given
        let expected = Cylinder {
            pressure: 300,
            ..Default::default()
        };
        let cylinder = Cylinder {
            ..Default::default()
        };
        let input = "301".to_string();

        // When
        let validated_cylinder_pressure = CylinderView::update_cylinder_pressure(input, cylinder);

        // Then
        assert_eq!(expected, validated_cylinder_pressure);
    }

    #[test]
    fn update_cylinder_pressure_by_being_unable_to_parse_input() {
        // Given
        let expected = Cylinder {
            pressure: 50,
            ..Default::default()
        };
        let cylinder = Cylinder {
            ..Default::default()
        };
        let input = "2£%^sdf".to_string();

        // When
        let validated_cylinder_pressure = CylinderView::update_cylinder_pressure(input, cylinder);

        // Then
        assert_eq!(expected, validated_cylinder_pressure);
    }
}
