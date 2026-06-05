use crate::{
    application::states::selectable_dive_model::SelectableDiveModel,
    controllers::dive_stages::{
        ambient_pressures::calculate_ambient_pressures,
        tissue_pressures::{
            calculate_helium_tissue_pressures, calculate_nitrogen_tissue_pressures,
            calculate_total_tissue_pressure,
        },
    },
    models::{
        application::dive_planner::DivePlanner,
        plan::{
            dive_model::DiveModel,
            dive_profile_result::tissue_pressure::{self, TissuePressure},
            dive_stage::DiveStage,
        },
    },
};

impl DivePlanner {
    pub fn dive_model_selected(&mut self, selectable_dive_model: SelectableDiveModel) {
        match selectable_dive_model {
            SelectableDiveModel::Bulhmann => {
                self.dive_planning.select_dive_model.selected_dive_model =
                    Some(SelectableDiveModel::Bulhmann);
                self.dive_stage.dive_model = DiveModel::create_zhl16_dive_model()
            }
            SelectableDiveModel::Usn => {
                self.dive_planning.select_dive_model.selected_dive_model =
                    Some(SelectableDiveModel::Usn);
                self.dive_stage.dive_model = DiveModel::create_usn_rev_6_dive_model()
            }
        }
    }

    // TODO test
    pub fn update_dive_profile(&self) -> DiveStage {
        let cylinder = self
            .dive_stage
            .cylinder
            .update_gas_management(&self.dive_stage.dive_step);

        let mut dive_model = self.dive_stage.dive_model.clone();

        dive_model.dive_profile.ambient_pressure = calculate_ambient_pressures(
            &self.dive_stage.dive_step,
            &self.dive_stage.cylinder.gas_mixture,
        );

        for compartment in 0..self
            .dive_stage
            .dive_model
            .dive_profile
            .number_of_compartments
        {
            // calculate ambient pressure
            dive_model.dive_profile.ambient_pressure = calculate_ambient_pressures(
                &self.dive_stage.dive_step,
                &self.dive_stage.cylinder.gas_mixture,
            );

            // calculate tissue pressures
            dive_model
                .dive_profile
                .tissue_pressure
                .nitrogen_tissue_pressures[compartment] = calculate_nitrogen_tissue_pressures(
                compartment,
                &dive_model,
                &self.dive_stage.dive_step,
            );

            dive_model
                .dive_profile
                .tissue_pressure
                .helium_tissue_pressures[compartment] = calculate_helium_tissue_pressures(
                compartment,
                &dive_model,
                &self.dive_stage.dive_step,
            );

            dive_model
                .dive_profile
                .tissue_pressure
                .total_tissue_pressures[compartment] =
                calculate_total_tissue_pressure(compartment, &dive_model.dive_profile);

            // calculate tolerated ambient pressures

            // calculate tolerated surface pressures

            // TODO REMOVE OLD
            // dive_model.dive_profile.nitrogen_tissue_pressures[compartment] =
            //     calculate_nitrogen_tissue_pressures(
            //         compartment,
            //         dive_stage.dive_model,
            //         dive_stage.dive_step,
            //     );
            // dive_stage.dive_model.dive_profile.helium_tissue_pressures[compartment] =
            //     calculate_helium_tissue_pressures(
            //         compartment,
            //         dive_stage.dive_model,
            //         dive_stage.dive_step,
            //     );
            // dive_stage.dive_model.dive_profile.total_tissue_pressures[compartment] =
            //     calculate_total_tissue_pressure(compartment, dive_stage.dive_model.dive_profile);
            // dive_stage.dive_model.dive_profile.a_values[compartment] =
            //     calculate_a_values(compartment, dive_stage.dive_model);
            // dive_stage.dive_model.dive_profile.b_values[compartment] =
            //     calculate_b_values(compartment, dive_stage.dive_model);
            // dive_stage
            //     .dive_model
            //     .dive_profile
            //     .tolerated_ambient_pressures[compartment] = calculate_tolerated_ambient_pressure(
            //     compartment,
            //     dive_stage.dive_model.dive_profile,
            // );
            // dive_stage.dive_model.dive_profile.maximum_surface_pressures[compartment] =
            //     calculate_max_surface_pressures(compartment, dive_stage.dive_model.dive_profile);
            // dive_stage.dive_model.dive_profile.compartment_loads[compartment] =
            //     calculate_compartment_loads(compartment, dive_stage.dive_model.dive_profile);
        }

        DiveStage::new(
            // TODO update dive profile in here
            dive_model,
            self.dive_stage.dive_step.clone(),
            cylinder,
        )
    }
}
