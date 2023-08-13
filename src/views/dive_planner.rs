use super::cylinder::CylinderView;
use super::cylinder_read_only::CylinderReadOnlyView;
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
        // TODO Move to a dive stage view
        let dive_step = DiveStepView::new(self);
        let cylinder = CylinderView::new(self);
        let cylinder_read_only = CylinderReadOnlyView::new(self);

        let dive_stage_view = determine_view(
            self.dive_stage.cylinder.is_read_only,
            dive_step,
            cylinder,
            cylinder_read_only,
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

// TODO move to dive stage view
fn determine_view<'a>(
    is_read_only: bool,
    dive_step: DiveStepView<'a>,
    cylinder: CylinderView<'a>,
    cylinder_read_only: CylinderReadOnlyView<'a>,
) -> iced::widget::Column<'a, Message> {
    if is_read_only {
        create_read_only_view(dive_step, cylinder_read_only)
    } else {
        create_setup_view(dive_step, cylinder)
    }
}

fn create_setup_view<'a>(
    dive_step: DiveStepView<'a>,
    cylinder: CylinderView<'a>,
) -> iced::widget::Column<'a, Message> {
    column![
        dive_step.dive_step_text,
        dive_step.depth_text,
        dive_step.depth_input,
        dive_step.time_text,
        dive_step.time_input,
        cylinder.cylinder_setup_text,
        cylinder.cylinder_volume_text,
        cylinder.cylinder_volume_input,
        cylinder.cylinder_pressure_text,
        cylinder.cylinder_pressure_input,
        cylinder.gas_management.surface_air_consumption_text,
        cylinder.gas_management.surface_air_consumption_input,
        cylinder.gas_mixture.gas_mixture_text,
        cylinder.gas_mixture.oxygen_text,
        cylinder.gas_mixture.oxygen_input,
        cylinder.gas_mixture.helium_text,
        cylinder.gas_mixture.helium_input,
        cylinder.gas_mixture.nitrogen_text,
        cylinder.gas_mixture.nitrogen_text_value,
    ]
    .spacing(10)
}

fn create_read_only_view<'a>(
    dive_step: DiveStepView<'a>,
    cylinder_read_only: CylinderReadOnlyView<'a>,
) -> iced::widget::Column<'a, Message> {
    column![
        dive_step.dive_step_text,
        dive_step.depth_text,
        dive_step.depth_input,
        dive_step.time_text,
        dive_step.time_input,
        cylinder_read_only.cylinder_read_only_text,
        cylinder_read_only.cylinder_read_only_volume_text,
        cylinder_read_only.cylinder_read_only_volume_text_value,
        cylinder_read_only.cylinder_read_only_pressure_text,
        cylinder_read_only.cylinder_read_only_pressure_text_value,
        cylinder_read_only.cylinder_read_only_initial_pressurised_cylinder_volume_text,
        cylinder_read_only.cylinder_read_only_initial_pressurised_cylinder_volume_text_value,
        cylinder_read_only
            .gas_mixture_read_only
            .gas_mixture_read_only_text,
        cylinder_read_only
            .gas_mixture_read_only
            .oxygen_read_only_text,
        cylinder_read_only
            .gas_mixture_read_only
            .oxygen_read_only_text_value,
        cylinder_read_only
            .gas_mixture_read_only
            .helium_read_only_text,
        cylinder_read_only
            .gas_mixture_read_only
            .helium_read_only_text_value,
        cylinder_read_only
            .gas_mixture_read_only
            .nitrogen_read_only_text,
        cylinder_read_only
            .gas_mixture_read_only
            .nitrogen_read_only_text_value,
        cylinder_read_only
            .gas_management_read_only
            .gas_management_read_only_text,
        cylinder_read_only
            .gas_management_read_only
            .remaining_read_only_text,
        cylinder_read_only
            .gas_management_read_only
            .remaining_read_only_text_value,
        cylinder_read_only
            .gas_management_read_only
            .used_read_only_text,
        cylinder_read_only
            .gas_management_read_only
            .used_read_only_text_value,
        cylinder_read_only
            .gas_management_read_only
            .surface_air_consumption_rate_read_only_text,
        cylinder_read_only
            .gas_management_read_only
            .surface_air_consumption_rate_read_only_text_value,
    ]
    .spacing(10)
}
