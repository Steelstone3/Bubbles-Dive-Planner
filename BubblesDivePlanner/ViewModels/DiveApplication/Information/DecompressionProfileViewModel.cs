using System.Collections.ObjectModel;
using BubblesDivePlanner.ViewModels.DiveApplication.Plan;

namespace BubblesDivePlanner.ViewModels.DiveApplication.Information
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