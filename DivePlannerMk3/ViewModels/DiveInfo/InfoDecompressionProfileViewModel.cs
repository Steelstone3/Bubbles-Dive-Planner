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
            IsUiVisible = false;
        }

        public ObservableCollection<DiveStepViewModel> DecoDiveSteps
        {
            get;
        } = new ObservableCollection<DiveStepViewModel>();
    }
}