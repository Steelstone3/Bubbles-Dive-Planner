use crate::commands::messages::Message;
use crate::controllers::file::{
    read_dive_planner_state, upsert_dive_planner_state, upsert_dive_results,
};
use crate::models::dive_profile::DiveProfile;
use crate::view_models::dive_planner::DivePlanner;
use crate::views::dive_results::results::ResultsView;
use crate::views::information::dive_information::DiveInformationView;
use crate::views::parameters::cylinder_parameters::cylinder::CylinderView;
use crate::views::parameters::cylinder_parameters::gas_management::GasManagementView;
use crate::views::parameters::cylinder_parameters::gas_mixture::GasMixtureView;
use crate::views::parameters::dive_stage::DiveStageView;
use crate::views::parameters::dive_step::DiveStepView;
use iced::widget::{column, row, scrollable};
use iced::{Element, Sandbox};
use iced_aw::Grid;

use super::menu_bar::MenuBarView;

const DIVE_PLANNER_STATE_FILE_NAME: &str = "dive_planner_state.json";
const DIVE_PLAN: &str = "dive_plan.json";

impl Sandbox for DivePlanner {
    type Message = Message;

    fn new() -> Self {
        Self {
            select_dive_model: Default::default(),
            select_cylinder: Default::default(),
            dive_stage: Default::default(),
            dive_results: Default::default(),
            decompression_steps: Default::default(),
            cns_toxicity: Default::default(),
            redo_buffer: Default::default(),
        }
    }

    fn title(&self) -> String {
        String::from("Bubbles Dive Planner")
    }

    fn update(&mut self, message: Message) {
        match message {
            Message::MenuBar => {}
            Message::FileNew => self.reset(),
            Message::FileSave => {
                upsert_dive_planner_state(DIVE_PLANNER_STATE_FILE_NAME, self);
                upsert_dive_results(DIVE_PLAN, &self.dive_results.results);
            }
            Message::FileLoad => *self = read_dive_planner_state(DIVE_PLANNER_STATE_FILE_NAME),
            Message::EditUndo => self.undo(),
            Message::EditRedo => self.redo(),
            Message::ViewToggleCentralNervousSystemToxicityVisibility => {
                self.cns_toxicity.is_visible = self.cns_toxicity.toggle_visibility();
            }
            Message::ViewToggleSelectCylinderVisibility => {
                self.select_cylinder.is_visible = self.select_cylinder.toggle_visibility();
            }
            Message::DiveModelSelected(selectable_dive_model) => self
                .select_dive_model
                .select_dive_model(selectable_dive_model, &mut self.dive_stage.dive_model),
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
            Message::CylinderSelected(selectable_cylinder) => self
                .select_cylinder
                .on_cylinder_selected(selectable_cylinder, &mut self.dive_stage.cylinder),
            Message::UpdateCylinderSelected(selectable_cylinder) => self
                .select_cylinder
                .update_cylinder_selected(selectable_cylinder, self.dive_stage.cylinder),
            Message::UpdateDiveProfile => {
                self.select_cylinder
                    .assign_cylinder(self.dive_stage.cylinder);

                self.dive_stage = DiveProfile::update_dive_profile(self.dive_stage);
                self.add_result();

                self.select_cylinder
                    .assign_cylinder(self.dive_stage.cylinder);

                self.select_cylinder.is_read_only();
            }
        }
    }

    fn view(&self) -> Element<Message> {
        let menu_bar = MenuBarView::new(self);
        let dive_stage = DiveStageView::new(self);
        let dive_information = DiveInformationView::new(self);
        let results = ResultsView::new(&self.dive_results.results);

        let dive_stage_view = DiveStageView::determine_view(
            self,
            dive_stage.select_dive_model,
            dive_stage.dive_step,
            dive_stage.cylinder,
            dive_stage.select_cylinder,
            dive_stage.cylinder_read_only,
        );

        column![]
            .push(
                Grid::with_columns(1).push(
                    row!(menu_bar.file, menu_bar.edit, menu_bar.view)
                        .spacing(10)
                        .padding(10),
                ),
            )
            .push(
                Grid::with_columns(2)
                    .push(scrollable(
                        column![dive_stage_view.spacing(10),]
                            .width(300)
                            .spacing(10)
                            .padding(10),
                    ))
                    .push(scrollable(
                        column![
                            results.result_title_text,
                            dive_information.dive_information_text,
                            results.results_text.spacing(10)
                        ]
                        .spacing(10)
                        .padding(10),
                    )),
            )
            .into()
    }
}
