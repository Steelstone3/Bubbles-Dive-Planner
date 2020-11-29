using System.Collections.ObjectModel;
using DivePlannerMk3.ViewModels.DivePlan;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DiveInfo
{
    public class InfoDecompressionProfileViewModel : ViewModelBase
    {
        //TODO true when deco steps are populated false otherwise behaviour
        //TODO decompression behaviour
        public InfoDecompressionProfileViewModel()
        {
            UiEnabled = false;
        }

        public ObservableCollection<PlanDiveStepViewModel> DecoDiveSteps
        {
            get;
        } = new ObservableCollection<PlanDiveStepViewModel>();
    }
}