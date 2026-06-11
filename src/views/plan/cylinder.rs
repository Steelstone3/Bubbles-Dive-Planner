use crate::{
    application::messages::message::Message, models::application::dive_planner::DivePlanner,
};
use iced::widget::{Column, column, row, text, text_input};
use iced_aw::Card;

impl DivePlanner {
    pub fn cylinder_view(&self) -> Column<'_, Message> {
        match self.dive_planning.is_planning {
            true => {
                let contents = column!()
                    // Mutable View
                    .push(text("Volume (l)"))
                    .spacing(10)
                    .push(
                        text_input(
                            "Enter Cylinder Volume",
                            &self.dive_stage.cylinder.get_volume().to_string(),
                        )
                        .on_input(Message::CylinderVolumeOnChanged),
                    )
                    .spacing(10)
                    .push(text("Pressure (bar)"))
                    .spacing(10)
                    .push(
                        text_input(
                            "Enter Cylinder Pressure (bar)",
                            &self.dive_stage.cylinder.get_pressure().to_string(),
                        )
                        .on_input(Message::CylinderPressureOnChanged),
                    )
                    .spacing(10)
                    .push(text("Pressurised Volume (l)"))
                    .spacing(10)
                    .push(text(
                        self.dive_stage
                            .cylinder
                            .get_initial_pressurised_cylinder_volume(),
                    ))
                    .push(self.gas_mixture_view())
                    .spacing(10)
                    .push(self.gas_management_view())
                    .spacing(10);

                column!().push(Card::new("Cylinder", contents))
            }
            false => {
                let contents = column!()
                    // Read Only View
                    .push(
                        row!()
                            .push(text("Volume (l):"))
                            .spacing(10)
                            .push(text(self.dive_stage.cylinder.get_volume().to_string()))
                            .spacing(10),
                    )
                    .spacing(10)
                    .push(
                        row!()
                            .push(text("Pressure (bar):"))
                            .spacing(10)
                            .push(text(self.dive_stage.cylinder.get_pressure().to_string()))
                            .spacing(10),
                    )
                    .spacing(10)
                    .push(
                        row!()
                            .push(text("Used (l):"))
                            .spacing(10)
                            .push(text(
                                self.dive_stage
                                    .cylinder
                                    .gas_management
                                    .get_used()
                                    .to_string(),
                            ))
                            .spacing(10),
                    )
                    .spacing(10)
                    .push(
                        row!()
                            .push(text("Remaining (l):"))
                            .spacing(10)
                            .push(text(format!(
                                "{} {} {}",
                                self.dive_stage.cylinder.gas_management.get_remaining(),
                                "/",
                                self.dive_stage
                                    .cylinder
                                    .get_initial_pressurised_cylinder_volume()
                            ))),
                    )
                    .spacing(10)
                    .push(self.gas_mixture_read_only_view())
                    .spacing(10);

                column!().push(Card::new("Cylinder", contents))
            }
        }
    }

    fn gas_mixture_view(&self) -> Column<'_, Message> {
        let contents = column!()
            .push(text("Oxygen (%)"))
            .spacing(10)
            .push(
                text_input(
                    "Enter Oxygen (%)",
                    &self.dive_stage.cylinder.gas_mixture.oxygen.to_string(),
                )
                .on_input(Message::OxygenOnChanged),
            )
            .push(text("Helium (%)"))
            .spacing(10)
            .push(
                text_input(
                    "Enter Helium (%)",
                    &self.dive_stage.cylinder.gas_mixture.helium.to_string(),
                )
                .on_input(Message::HeliumOnChanged),
            )
            .spacing(10)
            .push(text("Nitrogen (%)"))
            .spacing(10)
            .push(text(self.dive_stage.cylinder.gas_mixture.get_nitrogen()))
            .spacing(10);

        column!().push(Card::new("Gas Mixture", contents))
    }

    fn gas_mixture_read_only_view(&self) -> Column<'_, Message> {
        let contents = column!()
            .push(
                row!()
                    .push(text("Oxygen (%):"))
                    .spacing(10)
                    .push(text(
                        self.dive_stage.cylinder.gas_mixture.oxygen.to_string(),
                    ))
                    .spacing(10),
            )
            .spacing(10)
            .push(
                row!()
                    .push(text("Helium (%):"))
                    .spacing(10)
                    .push(text(
                        self.dive_stage.cylinder.gas_mixture.helium.to_string(),
                    ))
                    .spacing(10),
            )
            .spacing(10)
            .push(
                row!()
                    .push(text("Nitrogen (%):"))
                    .spacing(10)
                    .push(text(self.dive_stage.cylinder.gas_mixture.get_nitrogen()))
                    .spacing(10),
            )
            .spacing(10);

        column!().push(Card::new("Gas Mixture", contents))
    }

    fn gas_management_view(&self) -> Column<'_, Message> {
        let contents = column!()
            .push(text("S.A.C Rate (l/min)"))
            .spacing(10)
            .push(
                text_input(
                    "Enter S.A.C Rate (l/min)",
                    &self
                        .dive_stage
                        .cylinder
                        .gas_management
                        .get_surface_air_consumption_rate()
                        .to_string(),
                )
                .on_input(Message::SurfaceAirConsumptionOnChanged),
            )
            .spacing(10);

        column!().push(Card::new("Gas Management", contents))
    }
}
