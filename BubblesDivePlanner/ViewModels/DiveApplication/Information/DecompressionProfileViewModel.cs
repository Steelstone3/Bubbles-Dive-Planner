using System.Collections.ObjectModel;
using BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Information;
using BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan;
using BubblesDivePlanner.ViewModels.DiveApplication.Plan;

namespace BubblesDivePlanner.ViewModels.DiveApplication.Information
{
    public class DecompressionProfileViewModel : ViewModelBase, IDecompressionProfileViewModel
    {
        //TODO true when deco steps are populated false otherwise behaviour
        //TODO decompression behaviour
        public DecompressionProfileViewModel()
        {
            IsUiVisible = false;
        }

        public ObservableCollection<IDiveStepViewModel> DecoDiveSteps
        {
            get;
        } = new ObservableCollection<IDiveStepViewModel>();
    }
}