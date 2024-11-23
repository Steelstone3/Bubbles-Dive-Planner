use crate::{models::dive_profile::DiveProfile, models::dive_planner::DivePlanner};

impl DivePlanner {
    pub fn decompression_update_dive_profile(&mut self) {
        // Refresh decompression steps
        self.decompression_steps
            .assign_decompression_steps(self.dive_stage.calculate_decompression_dive_steps());

        self.run_decompression_steps();

        self.decompression_steps
            .assign_decompression_steps(self.dive_stage.calculate_decompression_dive_steps());
    }

    fn run_decompression_steps(&mut self) {
        for dive_step in &self.decompression_steps.dive_steps {
            self.dive_stage.dive_step = *dive_step;

            // TODO Refactor to use assign dive stage
            self.dive_stage = DiveProfile::update_dive_profile(self.dive_stage);

            // TODO Refactor to using dive_planner.update_results()
            self.dive_results.results.push(self.dive_stage);
        }

        self.redo_buffer = Default::default();
    }
}

#[cfg(test)]
mod decompression_should {
    use crate::{
        commands::selectable_cylinder::SelectableCylinder,
        models::{
            decompression_steps::DecompressionSteps, dive_profile::DiveProfile,
            select_cylinder::SelectCylinder,
        },
        test_fixture::dive_stage_test_fixture,
        models::dive_planner::DivePlanner,
    };

    #[test]
    fn decompression_update_dive_profile() {
        // Given
        let cylinder = dive_stage_test_fixture().cylinder;
        let selectable_cylinder = SelectableCylinder::Bottom;
        let expected_dive_profile = DiveProfile {
            number_of_compartments: 16,
            maximum_surface_pressures: [
                3.307063, 2.6041071, 2.3123977, 2.0934062, 1.9490435, 1.7928032, 1.6812322,
                1.5981219, 1.5376393, 1.4803765, 1.4364078, 1.3973763, 1.3510859, 1.3317899,
                1.3025703, 1.2758505,
            ],
            compartment_loads: [
                71.06676, 93.61995, 96.758675, 93.54917, 86.678925, 82.38294, 80.15056, 76.16506,
                70.9391, 65.68041, 66.66759, 67.67246, 69.31499, 62.323696, 63.234497, 64.14328,
            ],
            nitrogen_tissue_pressures: [
                2.143461, 2.1411657, 1.9202945, 1.6601363, 1.4312797, 1.2648269, 1.1799228,
                1.0892807, 0.9949037, 0.89806604, 0.8984879, 0.8988156, 0.89907104, 0.7999815,
                0.79998565, 0.7999888,
            ],
            helium_tissue_pressures: [
                0.2067617,
                0.29679793,
                0.3171508,
                0.29822788,
                0.2581302,
                0.21213704,
                0.16759425,
                0.12792979,
                0.095883876,
                0.07425135,
                0.059130467,
                0.046823382,
                0.037433997,
                0.030039186,
                0.023688126,
                0.018383523,
            ],
            total_tissue_pressures: [
                2.3502226, 2.4379637, 2.2374454, 1.9583642, 1.6894099, 1.476964, 1.347517,
                1.2172105, 1.0907875, 0.9723174, 0.9576184, 0.94563895, 0.936505, 0.83002067,
                0.8236738, 0.8183723,
            ],
            tolerated_ambient_pressures: [
                0.52357197, 0.89332557, 0.9466078, 0.89556754, 0.79117966, 0.7357851, 0.7116197,
                0.6620372, 0.59503824, 0.5327056, 0.5547682, 0.5759695, 0.6076899, 0.5217889,
                0.5406939, 0.5587929,
            ],
            a_values: [
                1.2987001, 1.0466264, 0.9085906, 0.8003015, 0.7057081, 0.5974157, 0.52402705,
                0.47104117, 0.4341974, 0.39314097, 0.36103684, 0.33203465, 0.29431748, 0.28252697,
                0.25991827, 0.23897184,
            ],
            b_values: [
                0.49791798, 0.64206254, 0.7123486, 0.77333254, 0.80428815, 0.83654886, 0.864151,
                0.8872479, 0.90625525, 0.9197639, 0.92991173, 0.938666, 0.9462811, 0.95305,
                0.9590928, 0.96443295,
            ],
            oxygen_at_pressure: 0.41599998,
            helium_at_pressure: 0.13,
            nitrogen_at_pressure: 0.75399995,
            dive_ceiling: -0.5339217,
        };
        let mut expected_dive_planner = DivePlanner {
            select_cylinder: SelectCylinder {
                selected_cylinder: Some(selectable_cylinder),
                cylinders: [cylinder, Default::default(), Default::default()],
                ..Default::default()
            },
            dive_stage: dive_stage_test_fixture(),
            decompression_steps: DecompressionSteps { dive_steps: vec![] },
            ..Default::default()
        };
        expected_dive_planner.dive_stage.dive_model.dive_profile = expected_dive_profile;
        let mut dive_planner = DivePlanner {
            select_cylinder: SelectCylinder {
                selected_cylinder: Some(selectable_cylinder),
                cylinders: [cylinder, Default::default(), Default::default()],
                ..Default::default()
            },
            dive_stage: dive_stage_test_fixture(),
            ..Default::default()
        };

        // When
        dive_planner.decompression_update_dive_profile();

        // Then
        assert_eq!(
            expected_dive_planner.decompression_steps,
            dive_planner.decompression_steps
        );
        assert_eq!(
            expected_dive_planner.dive_stage.dive_model.dive_profile,
            dive_planner.dive_stage.dive_model.dive_profile
        );
        assert_eq!(cylinder, dive_planner.select_cylinder.cylinders[0]);
        assert!(!dive_planner.dive_results.results.is_empty());
    }
}
