use super::{
    gas_mixture::GasMixtureView, input_parser::parse_input_u32, validation::validate_range,
};
use crate::{
    commands::messages::Message, models::cylinder::Cylinder, view_models::dive_planner::DivePlanner,
};
use iced::{widget::Text, widget::TextInput};

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
            cylinder_setup_text: todo!(),
            cylinder_volume_text: todo!(),
            cylinder_volume_input: todo!(),
            cylinder_pressure_text: todo!(),
            cylinder_pressure_input: todo!(),
            gas_mixture: GasMixtureView::new(dive_planner),
        }
    }

    pub fn update_cylinder_volume(cylinder_volume: String, mut cylinder: Cylinder) -> Cylinder {
        let cylinder_volume_input = parse_input_u32(cylinder_volume, 0);
        cylinder.cylinder_volume = validate_range(cylinder_volume_input, 3, 30);
        cylinder.update_initial_pressurised_cylinder_volume();

        cylinder
    }

    fn update_cylinder_pressure(input: String, cylinder: Cylinder) -> Cylinder {
        todo!()
    }
}

#[cfg(test)]
mod cylinder_view_should {
    use super::*;

    #[test]
    fn update_cylinder_volume_by_parsing_and_validating_input_successfully() {
        // Given
        let expected = Cylinder {
            cylinder_volume: 12,
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
            cylinder_volume: 12,
            cylinder_pressure: 200,
            initial_pressurised_cylinder_volume: 2400,
            ..Default::default()
        };
        let cylinder = Cylinder {
            cylinder_pressure: 200,
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
            cylinder_volume: 30,
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
    fn update_cylinder_volume_by_parsing_an_input_below_range() {
        // Given
        let expected = Cylinder {
            cylinder_volume: 3,
            ..Default::default()
        };
        let cylinder = Cylinder {
            ..Default::default()
        };
        let input = "2".to_string();

        // When
        let validated_cylinder_volume = CylinderView::update_cylinder_volume(input, cylinder);

        // Then
        assert_eq!(expected, validated_cylinder_volume);
    }

    #[test]
    fn update_cylinder_volume_by_being_unable_to_parse_input() {
        // Given
        let expected = Cylinder {
            cylinder_volume: 3,
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
}
