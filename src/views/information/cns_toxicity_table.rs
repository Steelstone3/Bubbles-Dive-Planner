use crate::{
    application::messages::message::Message,
    models::information::central_nervous_system_toxicity::{
        CNS_COLUMNS, CentralNervousSystemToxicity,
    },
};
use iced::{Color, Element};
use iced_table::Table;

pub fn cns_toxicity_table(
    cns: &CentralNervousSystemToxicity,
    color: Color,
) -> Element<'_, Message> {
    let mut table = Table::default();

    table.add_headers(vec![
        "Compartment",
        "O2 Partial Pressure (%)",
        "Exposure Per Dive (min)",
        "Exposure Per Day (min)",
    ]);

    for compartment in 0..CNS_COLUMNS {
        table.add_row(vec![
            (compartment + 1).to_string(),
            cns.oxygen_partial_pressure[compartment].to_string(),
            cns.maximum_single_dive_duration[compartment].to_string(),
            cns.maximum_total_dive_duration[compartment].to_string(),
        ]);
    }

    Table::build(table, Some(color), None, None, None)
}
