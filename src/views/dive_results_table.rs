use crate::{commands::messages::Message, models::result::dive_profile::DiveProfile};
use iced::{Color, Element};
use iced_table::Table;

pub fn dive_results_table(dive_profile: &DiveProfile, color: Color) -> Element<'_, Message> {
    let mut table = Table::default();
    table.add_headers(vec![
        "Compartment",
        "Tolerate Tissue Pressures",
        "Tolerated Ambient Pressures",
        "Maximum Surface Pressures",
        "Compartment Loads",
    ]);

    for compartment in 0..dive_profile.number_of_compartments {
        let total_tissue_pressures =
            format!("{:.3}", dive_profile.total_tissue_pressures[compartment]);
        let tolerated_ambient_pressures = format!(
            "{:.3}",
            dive_profile.tolerated_ambient_pressures[compartment]
        );
        let maximum_surface_pressures =
            format!("{:.3}", dive_profile.maximum_surface_pressures[compartment]);
        let compartment_loads = format!("{:.3}", dive_profile.compartment_loads[compartment]);

        table.add_row(vec![
            &(compartment + 1).to_string(),
            &total_tissue_pressures,
            &tolerated_ambient_pressures,
            &maximum_surface_pressures,
            &compartment_loads,
        ]);
    }

    Table::build(table, Some(color), None, None, None)
}
