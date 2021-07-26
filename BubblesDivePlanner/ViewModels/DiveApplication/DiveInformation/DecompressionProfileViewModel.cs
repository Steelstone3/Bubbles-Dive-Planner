using System.Collections.ObjectModel;
using DivePlannerMk3.ViewModels.DivePlan;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DiveInformation
{
    public class DecompressionProfileViewModel : ViewModelBase
    {
        //TODO true when deco steps are populated false otherwise behaviour
        //TODO decompression behaviour
        public DecompressionProfileViewModel()
        {
            IsUiVisible = false;
        }

        public ObservableCollection<DiveStepViewModel> DecoDiveSteps
        {
            get;
        } = new ObservableCollection<DiveStepViewModel>();
    }
}