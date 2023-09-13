use super::{gas_management::GasManagementView, gas_mixture::GasMixtureView};
use crate::{
    commands::messages::Message,
    models::cylinder::{
        Cylinder, MAXIMUM_PRESSURE_VALUE, MAXIMUM_VOLUME_VALUE, MINIMUM_PRESSURE_VALUE,
        MINIMUM_VOLUME_VALUE,
    },
    view_models::dive_planner::DivePlanner,
    views::input_parser::parse_input_u32,
};
use iced::{
    widget::Text,
    widget::{button, text, text_input, Button, TextInput},
    Length,
};

pub struct CylinderView<'a> {
    pub cylinder_setup_text: Text<'a>,
    pub cylinder_volume_text: Text<'a>,
    pub cylinder_volume_input: TextInput<'a, Message>,
    pub cylinder_pressure_text: Text<'a>,
    pub cylinder_pressure_input: TextInput<'a, Message>,
    pub cylinder_initial_pressurised_cylinder_volume_text: Text<'a>,
    pub cylinder_initial_pressurised_cylinder_volume_text_value: Text<'a>,
    pub update_cylinder: Button<'a, Message>,
    pub gas_mixture: GasMixtureView<'a>,
    pub gas_management: GasManagementView<'a>,
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
            cylinder_initial_pressurised_cylinder_volume_text: text("Pressurised Volume"),
            cylinder_initial_pressurised_cylinder_volume_text_value: text(
                dive_planner
                    .dive_stage
                    .cylinder
                    .initial_pressurised_cylinder_volume,
            ),
            update_cylinder: button("Update Cylinder").width(Length::Fill).on_press(
                Message::UpdateCylinderSelected(
                    dive_planner.select_cylinder.selected_cylinder.unwrap(),
                ),
            ),
            gas_mixture: GasMixtureView::new(dive_planner),
            gas_management: GasManagementView::new(dive_planner),
        }
    }

    pub fn update_cylinder_volume(cylinder_volume: String, mut cylinder: Cylinder) -> Cylinder {
        cylinder.volume =
            parse_input_u32(cylinder_volume, MINIMUM_VOLUME_VALUE, MAXIMUM_VOLUME_VALUE);
        cylinder.update_initial_pressurised_cylinder_volume();

        cylinder
    }

    pub fn update_cylinder_pressure(cylinder_pressure: String, mut cylinder: Cylinder) -> Cylinder {
        cylinder.pressure = parse_input_u32(
            cylinder_pressure,
            MINIMUM_PRESSURE_VALUE,
            MAXIMUM_PRESSURE_VALUE,
        );
        cylinder.update_initial_pressurised_cylinder_volume();

        cylinder
    }
}

#[cfg(test)]
mod cylinder_view_should {
    use super::*;
    use crate::models::gas_management::GasManagement;

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
            gas_management: GasManagement {
                remaining: 2400,
                ..Default::default()
            },
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
            gas_management: GasManagement {
                remaining: 2400,
                ..Default::default()
            },
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
