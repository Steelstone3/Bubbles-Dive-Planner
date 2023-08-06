use crate::commands::messages::Message;
use crate::controllers::dive_stage::update_dive_profile;

use crate::models::dive_step::DiveStep;
use crate::models::gas_mixture::GasMixture;
use crate::{models::dive_stage::DiveStage, view_models::dive_planner::DivePlanner};
use iced::widget::{button, column, scrollable, text};
use iced::{Alignment, Element, Length, Sandbox};

use super::cylinder::CylinderView;
use super::dive_step::DiveStepView;
use super::input_parser::parse_input_u32;

impl Sandbox for DivePlanner {
    type Message = Message;

    fn new() -> Self {
        Self {
            dive_stage: DiveStage::default(),
        }
    }

    fn title(&self) -> String {
        String::from("Bubbles Dive Planner")
    }

    fn update(&mut self, message: Message) {
        match message {
            Message::CalculateDivePlan => {
                // TODO consider dive profile assosiated function
                self.dive_stage = update_dive_profile(self.dive_stage);
            }
            Message::DepthChanged(depth) => {
                self.dive_stage.dive_step.depth = DiveStepView::update_depth(depth);
            }
            Message::TimeChanged(time) => {
                self.dive_stage.dive_step.time = DiveStepView::update_time(time);
            }
            Message::OxygenChanged(oxygen) => {
                self.dive_stage.cylinder.gas_mixture = CylinderView::update_oxygen(oxygen, self);
            }
            Message::HeliumChanged(helium) => {
                self.dive_stage.cylinder.gas_mixture = CylinderView::update_helium(helium, self);
            }
        }
    }

    fn view(&self) -> Element<Message> {
        let dive_step = DiveStepView::new(self);
        let cylinder = CylinderView::new(self);

        column![iced::widget::row![scrollable(
            column![
                dive_step.depth_text,
                dive_step.depth_input,
                dive_step.time_text,
                dive_step.time_input,
                cylinder.oxygen_text,
                cylinder.oxygen_input,
                cylinder.helium_text,
                cylinder.helium_input,
                cylinder.nitrogen_text,
                cylinder.nitrogen_text_value,
                button("Calculate").on_press(Self::Message::CalculateDivePlan)
            ]
            .align_items(Alignment::Start)
            .spacing(10),
        )]
        .spacing(10)
        .push(scrollable(
            column![text(self.dive_stage.dive_model.dive_profile)]
                .align_items(Alignment::Start)
                .spacing(10)
                .width(Length::FillPortion(4))
        )),]
        .align_items(Alignment::Center)
        .width(Length::FillPortion(1))
        .spacing(10)
        .into()
    }
}
