use super::cylinder::CylinderView;
use super::dive_stage_view::DiveStageView;
use super::dive_step::DiveStepView;
use super::gas_management::GasManagementView;
use super::gas_mixture::GasMixtureView;
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
            Message::DepthChanged(depth) => {
                self.dive_stage.dive_step.depth = DiveStepView::update_depth(depth);
            }
            Message::TimeChanged(time) => {
                self.dive_stage.dive_step.time = DiveStepView::update_time(time);
            }
            Message::CylinderVolumeChanged(cylinder_volume) => {
                self.dive_stage.cylinder =
                    CylinderView::update_cylinder_volume(cylinder_volume, self.dive_stage.cylinder);
            }
            Message::CylinderPressureChanged(cylinder_pressure) => {
                self.dive_stage.cylinder = CylinderView::update_cylinder_pressure(
                    cylinder_pressure,
                    self.dive_stage.cylinder,
                );
            }
            Message::SurfaceAirConsumptionChanged(surface_air_consumption) => {
                self.dive_stage
                    .cylinder
                    .gas_management
                    .surface_air_consumption_rate =
                    GasManagementView::update_surface_air_consumption_rate(surface_air_consumption);
            }
            Message::OxygenChanged(oxygen) => {
                self.dive_stage.cylinder.gas_mixture = GasMixtureView::update_oxygen(
                    oxygen,
                    self.dive_stage.cylinder.gas_mixture.helium,
                );
            }
            Message::HeliumChanged(helium) => {
                self.dive_stage.cylinder.gas_mixture = GasMixtureView::update_helium(
                    helium,
                    self.dive_stage.cylinder.gas_mixture.oxygen,
                );
            }
            Message::CalculateDivePlan => {
                self.dive_stage = DiveProfile::update_dive_profile(self.dive_stage);
                self.dive_stage.cylinder.is_read_only = true;
            }
        }
    }

    fn view(&self) -> Element<Message> {
        let dive_stage = DiveStageView::new(self);

        let dive_stage_view = DiveStageView::determine_view(
            self.dive_stage.cylinder.is_read_only,
            dive_stage.dive_step,
            dive_stage.cylinder,
            dive_stage.cylinder_read_only,
        );

        column![iced::widget::row![scrollable(
            column![
                dive_stage_view,
                button("Calculate")
                    .on_press(Self::Message::CalculateDivePlan)
                    .width(Length::FillPortion(1))
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
