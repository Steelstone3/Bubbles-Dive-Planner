use super::cylinder::Cylinder;
use crate::commands::selectable_cylinder::SelectableCylinder;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Serialize, Deserialize)]
pub struct SelectCylinder {
    pub is_visible: bool,
    pub cylinders: [Cylinder; 3],
    pub selected_cylinder: Option<SelectableCylinder>,
}

impl Default for SelectCylinder {
    fn default() -> Self {
        Self {
            is_visible: Default::default(),
            cylinders: Default::default(),
            selected_cylinder: Some(SelectableCylinder::Bottom),
        }
    }
}

impl SelectCylinder {
    pub fn update_cylinder_selected(
        &mut self,
        selectable_cylinder: SelectableCylinder,
        cylinder: Cylinder,
    ) {
        self.selected_cylinder = Some(selectable_cylinder);

        match selectable_cylinder {
            SelectableCylinder::Bottom => {
                self.selected_cylinder = Some(selectable_cylinder);
                self.cylinders[0] = cylinder;
            }
            SelectableCylinder::Decompression => {
                self.selected_cylinder = Some(selectable_cylinder);
                self.cylinders[1] = cylinder;
            }
            SelectableCylinder::Descend => {
                self.selected_cylinder = Some(selectable_cylinder);
                self.cylinders[2] = cylinder;
            }
        }
    }

    pub fn on_cylinder_selected(
        &mut self,
        selectable_cylinder: SelectableCylinder,
        cylinder: &mut Cylinder,
    ) {
        self.selected_cylinder = Some(selectable_cylinder);

        match selectable_cylinder {
            SelectableCylinder::Bottom => {
                self.selected_cylinder = Some(selectable_cylinder);
                *cylinder = self.cylinders[0]
            }
            SelectableCylinder::Decompression => {
                self.selected_cylinder = Some(selectable_cylinder);
                *cylinder = self.cylinders[1]
            }
            SelectableCylinder::Descend => {
                self.selected_cylinder = Some(selectable_cylinder);
                *cylinder = self.cylinders[2]
            }
        }
    }

    pub fn assign_cylinder(&mut self, cylinder: Cylinder) {
        match self.selected_cylinder.unwrap() {
            SelectableCylinder::Bottom => {
                self.cylinders[0] = cylinder;
            }
            SelectableCylinder::Decompression => {
                self.cylinders[1] = cylinder;
            }
            SelectableCylinder::Descend => {
                self.cylinders[2] = cylinder;
            }
        }
    }

    pub fn read_only_view(&mut self) {
        self.cylinders[0].is_read_only = true;
        self.cylinders[1].is_read_only = true;
        self.cylinders[2].is_read_only = true;
    }

    pub fn toggle_visibility(&mut self) {
        let is_visible = match self.is_visible {
            true => false,
            false => true,
        };

        self.is_visible = is_visible;
    }
}

#[cfg(test)]
mod select_cylinder_should {
    use super::*;
    use crate::models::{gas_management::GasManagement, gas_mixture::GasMixture};
    use rstest::rstest;

    #[rstest]
    #[case(false, true)]
    #[case(true, false)]
    fn toggle_select_cylinder_visibility(
        #[case] is_visible: bool,
        #[case] expected_is_visible: bool,
    ) {
        // Given
        let mut select_cylinder = SelectCylinder {
            is_visible,
            ..Default::default()
        };

        // When
        select_cylinder.toggle_visibility();

        // Then
        assert_eq!(expected_is_visible, select_cylinder.is_visible);
    }

    #[test]
    fn set_cylinders_to_read_only() {
        // Given
        let mut select_cylinder = SelectCylinder {
            cylinders: Default::default(),
            selected_cylinder: Default::default(),
            is_visible: true,
        };

        // When
        select_cylinder.read_only_view();

        // Then
        assert!(select_cylinder.cylinders[0].is_read_only);
        assert!(select_cylinder.cylinders[1].is_read_only);
        assert!(select_cylinder.cylinders[2].is_read_only);
    }

    #[rstest]
    #[case(SelectableCylinder::Bottom, 0)]
    #[case(SelectableCylinder::Decompression, 1)]
    #[case(SelectableCylinder::Descend, 2)]
    fn assign_to_the_selected_cylinder(
        #[case] selectable_cylinder: SelectableCylinder,
        #[case] index: usize,
    ) {
        let mut select_cylinder = SelectCylinder {
            cylinders: Default::default(),
            selected_cylinder: Some(selectable_cylinder),
            is_visible: true,
        };
        let cylinder = Cylinder {
            is_read_only: true,
            volume: 12,
            pressure: 200,
            initial_pressurised_cylinder_volume: 2400,
            gas_mixture: GasMixture {
                oxygen: 21,
                helium: 10,
                nitrogen: 69,
                maximum_operating_depth: 56.67,
            },
            gas_management: GasManagement {
                remaining: 1680,
                used: 720,
                surface_air_consumption_rate: 12,
            },
        };

        // When
        select_cylinder.assign_cylinder(cylinder);

        // Then
        assert_eq!(cylinder, select_cylinder.cylinders[index]);
    }

    #[rstest]
    #[case(SelectableCylinder::Bottom)]
    #[case(SelectableCylinder::Decompression)]
    #[case(SelectableCylinder::Descend)]
    fn update_cylinder_setup_parameters_on_selection_changed(
        #[case] selectable_cylinder: SelectableCylinder,
    ) {
        // Given
        let mut cylinder = Cylinder {
            ..Default::default()
        };
        let expected_cylinder = Cylinder {
            is_read_only: true,
            volume: 12,
            pressure: 200,
            initial_pressurised_cylinder_volume: 2400,
            gas_mixture: GasMixture {
                oxygen: 21,
                helium: 10,
                nitrogen: 69,
                maximum_operating_depth: 56.67,
            },
            gas_management: GasManagement {
                remaining: 1680,
                used: 720,
                surface_air_consumption_rate: 12,
            },
        };
        let mut select_cylinder = SelectCylinder {
            cylinders: [expected_cylinder, expected_cylinder, expected_cylinder],
            selected_cylinder: Some(selectable_cylinder),
            is_visible: true,
        };

        // When
        select_cylinder
            .on_cylinder_selected(select_cylinder.selected_cylinder.unwrap(), &mut cylinder);

        // Then
        assert_eq!(expected_cylinder, cylinder);
    }

    #[rstest]
    #[case(SelectableCylinder::Bottom, 0)]
    #[case(SelectableCylinder::Decompression, 1)]
    #[case(SelectableCylinder::Descend, 2)]
    fn update_the_selected_cylinder(
        #[case] selectable_cylinder: SelectableCylinder,
        #[case] index: usize,
    ) {
        // Given
        let mut select_cylinder = SelectCylinder {
            cylinders: Default::default(),
            selected_cylinder: Some(selectable_cylinder),
            is_visible: true,
        };
        let cylinder = Cylinder {
            is_read_only: true,
            volume: 12,
            pressure: 200,
            initial_pressurised_cylinder_volume: 2400,
            gas_mixture: GasMixture {
                oxygen: 21,
                helium: 10,
                nitrogen: 69,
                maximum_operating_depth: 56.67,
            },
            gas_management: GasManagement {
                remaining: 1680,
                used: 720,
                surface_air_consumption_rate: 12,
            },
        };

        // When
        select_cylinder.update_cylinder_selected(selectable_cylinder, cylinder);

        // Then
        assert_eq!(cylinder, select_cylinder.cylinders[index]);
    }
}
