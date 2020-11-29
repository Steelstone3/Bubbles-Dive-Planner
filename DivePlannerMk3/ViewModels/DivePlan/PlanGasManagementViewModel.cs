using DivePlannerMk3.Models;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DivePlan
{
    public class PlanGasManagementViewModel : ViewModelBase
    {
        public PlanGasManagementViewModel()
        {
            UiEnabled = true;
        }

        public GasManagementSetupModel GasManagementModel
        {
            get; set;
        }
    }
}
