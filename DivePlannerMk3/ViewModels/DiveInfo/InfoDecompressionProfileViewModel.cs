using System.Collections.Generic;
using System.Collections.ObjectModel;
using DivePlannerMk3.ViewModels.DivePlan;

namespace DivePlannerMk3.ViewModels.DiveInfo
{
    public class InfoDecompressionProfileViewModel : ViewModelBase
    {
        public ObservableCollection<PlanDiveStepViewModel> DecoDiveSteps
        {
            get;
        } = new ObservableCollection<PlanDiveStepViewModel>();
    }
}