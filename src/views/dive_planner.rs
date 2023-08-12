use super::cylinder::CylinderView;
use super::dive_step::DiveStepView;
use crate::commands::messages::Message;
use crate::models::dive_profile::DiveProfile;
use crate::{models::dive_stage::DiveStage, view_models::dive_planner::DivePlanner};
use iced::widget::{button, column, scrollable, text};
use iced::{Alignment, Element, Length, Sandbox};

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
                self.dive_stage = DiveProfile::update_dive_profile(self.dive_stage);
            }
            Message::DepthChanged(depth) => {
                self.dive_stage.dive_step.depth = DiveStepView::update_depth(depth);
            }
            Message::TimeChanged(time) => {
                self.dive_stage.dive_step.time = DiveStepView::update_time(time);
            }
            Message::OxygenChanged(oxygen) => {
                self.dive_stage.cylinder.gas_mixture = CylinderView::update_oxygen(
                    oxygen,
                    self.dive_stage.cylinder.gas_mixture.helium,
                );
            }
            Message::HeliumChanged(helium) => {
                self.dive_stage.cylinder.gas_mixture = CylinderView::update_helium(
                    helium,
                    self.dive_stage.cylinder.gas_mixture.oxygen,
                );
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
            .width(200)
            .spacing(10)
            .padding(10)
        )]
        .spacing(10)
        .push(scrollable(
            column![text(self.dive_stage.dive_model.dive_profile)]
                .align_items(Alignment::Start)
                .spacing(10)
                .padding(10)
                .width(Length::FillPortion(4))
        )),]
        .align_items(Alignment::Center)
        .width(Length::FillPortion(1))
        .spacing(10)
        .into()
    }
}
