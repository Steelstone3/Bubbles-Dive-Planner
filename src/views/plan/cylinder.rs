use crate::{commands::messages::Message, view_models::dive_planner::DivePlanner};
use iced::widget::{column, text, text_input, Column};

impl DivePlanner {
    pub fn cylinder_view(&self) -> Column<Message> {
        match self.is_planning {
            true => column!()
                // Read Only View
                .push(text("Cylinder"))
                .spacing(10)
                .push(text("Volume (l)"))
                .spacing(10)
                .push(text(self.dive_stage.cylinder.volume.to_string()))
                .spacing(10)
                .push("Pressure (bar)")
                .spacing(10)
                .push(text(self.dive_stage.cylinder.pressure.to_string()))
                .spacing(10)
                .push(text("Used (l)"))
                .spacing(10)
                .push(text(
                    self.dive_stage.cylinder.gas_management.used.to_string(),
                ))
                .push(text("Remaining (l)"))
                .spacing(10)
                .push(text(format!(
                    "{} {} {}",
                    self.dive_stage.cylinder.gas_management.remaining,
                    "/",
                    self.dive_stage.cylinder.initial_pressurised_cylinder_volume
                )))
                .spacing(10)
                .push(self.gas_mixture_read_only_view())
                .spacing(10),
            false => column!()
                // Mutable View
                .push(text("Cylinder Setup"))
                .spacing(10)
                .push(text("Volume (l)"))
                .spacing(10)
                .push(
                    text_input(
                        "Enter Cylinder Volume",
                        &self.dive_stage.cylinder.volume.to_string(),
                    )
                    .on_input(Message::CylinderVolumeChanged),
                )
                .spacing(10)
                .push(text("Pressure (bar)"))
                .spacing(10)
                .push(
                    text_input(
                        "Enter Cylinder Pressure (bar)",
                        &self.dive_stage.cylinder.pressure.to_string(),
                    )
                    .on_input(Message::CylinderPressureChanged),
                )
                .spacing(10)
                .push(text("Pressurised Volume (l)"))
                .spacing(10)
                .push(text(
                    self.dive_stage.cylinder.initial_pressurised_cylinder_volume,
                ))
                .push(self.gas_mixture_view())
                .spacing(10)
                .push(self.gas_management_view())
                .spacing(10),
        }
    }

    fn gas_mixture_view(&self) -> Column<Message> {
        column!()
            .push(text("Gas Mixture"))
            .spacing(10)
            .push(text("Oxygen (%)"))
            .spacing(10)
            .push(
                text_input(
                    "Enter Oxygen (%)",
                    &self.dive_stage.cylinder.gas_mixture.oxygen.to_string(),
                )
                .on_input(Message::OxygenChanged),
            )
            .push(text("Helium (%)"))
            .spacing(10)
            .push(
                text_input(
                    "Enter Helium (%)",
                    &self.dive_stage.cylinder.gas_mixture.helium.to_string(),
                )
                .on_input(Message::HeliumChanged),
            )
            .spacing(10)
            .push(text("Nitrogen (%)"))
            .spacing(10)
            .push(text(self.dive_stage.cylinder.gas_mixture.nitrogen))
            .spacing(10)
    }

    fn gas_mixture_read_only_view(&self) -> Column<Message> {
        column!()
            .push(text("Gas Mixture"))
            .spacing(10)
            .push(text("Oxygen (%)"))
            .spacing(10)
            .push(text(
                self.dive_stage.cylinder.gas_mixture.oxygen.to_string(),
            ))
            .push(text("Helium (%)"))
            .spacing(10)
            .push(text(
                self.dive_stage.cylinder.gas_mixture.helium.to_string(),
            ))
            .spacing(10)
            .push(text("Nitrogen (%)"))
            .spacing(10)
            .push(text(self.dive_stage.cylinder.gas_mixture.nitrogen))
            .spacing(10)
    }

    fn gas_management_view(&self) -> Column<Message> {
        column!()
            .push(text("Gas Management"))
            .spacing(10)
            .push(text("S.A.C Rate (l/min)"))
            .spacing(10)
            .push(
                text_input(
                    "Enter S.A.C Rate (l/min)",
                    &self
                        .dive_stage
                        .cylinder
                        .gas_management
                        .surface_air_consumption_rate
                        .to_string(),
                )
                .on_input(Message::SurfaceAirConsumptionChanged),
            )
            .spacing(10)
    }
}
