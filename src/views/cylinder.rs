use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner};
use iced::{
    widget::{Text, TextInput, text, text_input},
    Renderer,
};

pub struct CylinderView<'a> {
    pub oxygen_text: Text<'a, Renderer>,
    pub oxygen_input: TextInput<'a, Message, Renderer>,
    pub helium_text: Text<'a>,
    pub helium_input: TextInput<'a, Message>,
    pub nitrogen_text: Text<'a, Renderer>,
    pub nitrogen_text_value: Text<'a, Renderer>,
}

impl CylinderView<'_> {
    pub fn new(dive_planner: &DivePlanner) -> Self {
        Self {
            oxygen_text: text("Oxygen"),
            oxygen_input: text_input(
                "Enter Oxygen",
                &dive_planner.dive_stage.cylinder.gas_mixture.oxygen.to_string(),
            )
            .width(100)
            .on_input(Message::OxygenChanged),

            helium_text: text("Helium"),
            helium_input: text_input(
                "Enter Helium",
                &dive_planner.dive_stage.cylinder.gas_mixture.helium.to_string(),
            )
            .width(100)
            .on_input(Message::HeliumChanged),

            nitrogen_text: text("Nitrogen"),
            nitrogen_text_value: text(dive_planner.dive_stage.cylinder.gas_mixture.nitrogen),
        }
    }
}
