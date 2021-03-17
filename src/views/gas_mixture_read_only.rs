use iced::widget::{text, Text};

use crate::view_models::dive_planner::DivePlanner;

pub struct GasMixtureReadOnlyView<'a> {
    pub gas_mixture_read_only_text: Text<'a>,
    pub oxygen_read_only_text: Text<'a>,
    pub oxygen_read_only_text_value: Text<'a>,
    pub helium_read_only_text: Text<'a>,
    pub helium_read_only_text_value: Text<'a>,
    pub nitrogen_read_only_text: Text<'a>,
    pub nitrogen_read_only_text_value: Text<'a>,
}

impl GasMixtureReadOnlyView<'_> {
    pub fn new(dive_planner: &DivePlanner) -> Self {
        Self {
            gas_mixture_read_only_text: text("Gas Mixture"),
            oxygen_read_only_text: text("Oxygen (%)"),
            oxygen_read_only_text_value: text(dive_planner.dive_stage.cylinder.gas_mixture.oxygen),
            helium_read_only_text: text("Helium (%)"),
            helium_read_only_text_value: text(dive_planner.dive_stage.cylinder.gas_mixture.helium),
            nitrogen_read_only_text: text("Nitrogen (%)"),
            nitrogen_read_only_text_value: text(
                dive_planner.dive_stage.cylinder.gas_mixture.nitrogen,
            ),
        }
    }
}
