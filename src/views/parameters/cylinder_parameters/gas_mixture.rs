use crate::{commands::messages::Message, models::gas_mixture::GasMixture};
use iced::widget::{column, text, text_input, Column, Text, TextInput};

pub struct GasMixtureView<'a> {
    gas_mixture_text: Text<'a>,
    oxygen_text: Text<'a>,
    oxygen_input: TextInput<'a, Message>,
    helium_text: Text<'a>,
    helium_input: TextInput<'a, Message>,
    nitrogen_text: Text<'a>,
    nitrogen_text_value: Text<'a>,
}

impl GasMixtureView<'_> {
    pub fn build_view<'a>(gas_mixture: &GasMixture) -> Column<'a, Message> {
        let gas_mixture = GasMixtureView::new(gas_mixture);

        column![
            gas_mixture.gas_mixture_text,
            gas_mixture.oxygen_text,
            gas_mixture.oxygen_input,
            gas_mixture.helium_text,
            gas_mixture.helium_input,
            gas_mixture.nitrogen_text,
            gas_mixture.nitrogen_text_value,
        ]
    }

    fn new<'a>(gas_mixture: &GasMixture) -> GasMixtureView<'a> {
        GasMixtureView {
            gas_mixture_text: text("Gas Mixture"),
            oxygen_text: text("Oxygen (%)"),
            oxygen_input: text_input("Enter Oxygen", &gas_mixture.oxygen.to_string())
                .on_input(Message::OxygenChanged),

            helium_text: text("Helium (%)"),
            helium_input: text_input("Enter Helium", &gas_mixture.helium.to_string())
                .on_input(Message::HeliumChanged),

            nitrogen_text: text("Nitrogen (%)"),
            nitrogen_text_value: text(gas_mixture.nitrogen),
        }
    }
}
