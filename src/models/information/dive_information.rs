use serde::{Deserialize, Serialize};

use crate::models::information::{
    central_nervous_system_toxicity::CentralNervousSystemToxicity,
    decompression_steps::DecompressionSteps,
};

#[derive(Debug, Default, Clone, PartialEq, Serialize, Deserialize)]
pub struct DiveInformation {
    pub decompression_steps: DecompressionSteps,
    pub cns_toxicity: CentralNervousSystemToxicity,
}
