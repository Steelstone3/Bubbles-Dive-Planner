use crate::{commands::selectable_cylinder::SelectableCylinder, models::plan::cylinder::Cylinder};
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Serialize, Deserialize)]
pub struct SelectCylinder {
    pub is_multiple_cylinder: bool,
    pub cylinders: [Cylinder; 3],
    pub selected_cylinder: Option<SelectableCylinder>,
}

impl Default for SelectCylinder {
    fn default() -> Self {
        Self {
            is_multiple_cylinder: true,
            cylinders: Default::default(),
            selected_cylinder: Some(SelectableCylinder::Bottom),
        }
    }
}

impl SelectCylinder {
    pub fn on_select_update_cylinder_from_selected_cylinder(
        &mut self,
        selectable_cylinder: SelectableCylinder,
        cylinder: &mut Cylinder,
    ) {
        match selectable_cylinder {
            SelectableCylinder::Bottom => {
                self.selected_cylinder = Some(SelectableCylinder::Bottom);

                if let Some(the_cylinder) = self.cylinders.first() {
                    *cylinder = *the_cylinder
                }
            }
            SelectableCylinder::Decompression => {
                self.selected_cylinder = Some(SelectableCylinder::Decompression);

                if let Some(the_cylinder) = self.cylinders.get(1) {
                    *cylinder = *the_cylinder
                }
            }
            SelectableCylinder::Descend => {
                self.selected_cylinder = Some(SelectableCylinder::Descend);

                if let Some(the_cylinder) = self.cylinders.get(2) {
                    *cylinder = *the_cylinder
                }
            }
        }
    }

    pub fn on_update_selected_cylinder(
        &mut self,
        selectable_cylinder: SelectableCylinder,
        cylinder: Cylinder,
    ) {
        match selectable_cylinder {
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

    pub fn assign_selected_cylinder(&mut self, cylinder: Cylinder) {
        if let Some(selected_cylinder) = self.selected_cylinder {
            match selected_cylinder {
                SelectableCylinder::Bottom => self.cylinders[0] = cylinder,
                SelectableCylinder::Decompression => self.cylinders[1] = cylinder,
                SelectableCylinder::Descend => self.cylinders[2] = cylinder,
            }
        }
    }

    pub fn toggle_visibility(&mut self) {
        let is_visible = match self.is_multiple_cylinder {
            true => false,
            false => true,
        };

        self.is_multiple_cylinder = is_visible;
    }
}

#[cfg(test)]
mod select_cylinder_should {
    use super::*;
    use crate::models::{
        information::gas_management::GasManagement, plan::gas_mixture::GasMixture,
    };
    use rstest::rstest;

    #[rstest]
    #[case(false, true)]
    #[case(true, false)]
    fn toggle_select_cylinder_visibility(
        #[case] is_multiple_cylinder: bool,
        #[case] expected_is_visible: bool,
    ) {
        // Given
        let mut select_cylinder = SelectCylinder {
            is_multiple_cylinder,
            ..Default::default()
        };

        // When
        select_cylinder.toggle_visibility();

        // Then
        assert_eq!(expected_is_visible, select_cylinder.is_multiple_cylinder);
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
            is_multiple_cylinder: true,
        };
        let cylinder = Cylinder {
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
        select_cylinder.assign_selected_cylinder(cylinder);

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
            is_multiple_cylinder: true,
        };

        // When
        select_cylinder.on_select_update_cylinder_from_selected_cylinder(
            select_cylinder.selected_cylinder.unwrap(),
            &mut cylinder,
        );

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
            is_multiple_cylinder: true,
        };
        let cylinder = Cylinder {
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
        select_cylinder.on_update_selected_cylinder(selectable_cylinder, cylinder);

        // Then
        assert_eq!(cylinder, select_cylinder.cylinders[index]);
    }
}
