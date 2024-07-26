use crate::commands::messages::Message;
use crate::view_models::dive_planner::DivePlanner;
use iced::{
    widget::{column, Scrollable},
    Element, Sandbox,
};

// pub enum PaneState {
//     PlanPane,
//     InformationPane,
//     ResultsPane,
// }

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
            is_planning: Default::default(),
        }
    }

    fn title(&self) -> String {
        String::from("Bubbles Dive Planner")
    }

    fn update(&mut self, message: Message) {
        match message {
            // Message::PaneDragged(_) => {}
            // Message::PaneResized(_) => {}
            Message::MenuBar => {}
            Message::FileNew => self.file_new(),
            Message::FileSave => self.file_save(),
            Message::FileLoad => self.file_load(),
            Message::EditUndo => self.edit_undo(),
            Message::EditRedo => self.edit_redo(),
            // Message::ViewToggleSelectCylinderVisibility => {
            //     self.view_toggle_select_cylinder_visibility();
            // }
            Message::DiveModelSelected(selectable_dive_model) => {
                self.dive_model_selected(selectable_dive_model)
            }
            Message::DepthChanged(depth) => self.dive_stage.dive_step.update_depth(depth),
            Message::TimeChanged(time) => self.dive_stage.dive_step.update_time(time),
            Message::CylinderVolumeChanged(cylinder_volume) => {
                self.dive_stage
                    .cylinder
                    .update_cylinder_volume(cylinder_volume);
            }
            Message::CylinderPressureChanged(cylinder_pressure) => {
                self.dive_stage
                    .cylinder
                    .update_cylinder_pressure(cylinder_pressure);
            }
            Message::SurfaceAirConsumptionChanged(surface_air_consumption) => self
                .dive_stage
                .cylinder
                .gas_management
                .update_surface_air_consumption_rate(surface_air_consumption),
            Message::OxygenChanged(oxygen) => {
                self.dive_stage.cylinder.gas_mixture.update_oxygen(oxygen)
            }
            Message::HeliumChanged(helium) => {
                self.dive_stage.cylinder.gas_mixture.update_helium(helium)
            } // Message::CylinderSelected(selectable_cylinder) => {
            //     self.cylinder_selected(selectable_cylinder);
            // }
            // Message::UpdateCylinderSelected(selectable_cylinder) => {
            //     self.update_cylinder_selected(selectable_cylinder)
            // }
            Message::UpdateDiveProfile => {
                self.update_dive_profile();
            }
            Message::DecompressionUpdateDiveProfile => {
                self.decompression_update_dive_profile();
            }
        }
    }

    fn view(&self) -> Element<Message> {
        // TODO AH Consider a pane_grid for flexible user centric layout https://docs.rs/iced/latest/iced/widget/pane_grid/index.html
        let mut column = column!();

        column = column.push(self.menu_view());

        let scrollable = Scrollable::new(
            column!()
                .push(self.plan_view())
                .push(self.information_view())
                .push(self.results_view()),
        );

        column = column.push(scrollable);

        // let (state, _) = pane_grid::State::new(PaneState::PlanPane);

        // let pane_grid = PaneGrid::new(&state, |pane, state, is_maximized| {
        //     pane_grid::Content::new(match state {
        //         PaneState::PlanPane => self.plan_view(),
        //         PaneState::InformationPane => self.information_view(),
        //         PaneState::ResultsPane => self.results_view(),
        //     })
        // })
        // .on_drag(Message::PaneDragged)
        // .on_resize(10, Message::PaneResized);

        // column = column.push(pane_grid);

        column.into()
    }
}
