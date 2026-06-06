use serde::{Deserialize, Serialize};

use crate::models::information::central_nervous_system_toxicity::CentralNervousSystemToxicity;

#[derive(Debug, Default, Clone, PartialEq, Serialize, Deserialize)]
pub struct DiveInformation {
    #[serde(skip)]
    pub cns_toxicity: CentralNervousSystemToxicity,
}
