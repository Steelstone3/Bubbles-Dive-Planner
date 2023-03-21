using System.Collections.ObjectModel;
using BubblesDivePlanner.ViewModels.Models;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.DiveStages
{
    public class DecompressionProfileViewModel : ReactiveObject, IDecompressionProfileModel
    {
        public ObservableCollection<IDiveStepModel> DecompressionDiveSteps
        {
            get;
        } = new ObservableCollection<IDiveStepModel>();
    }
}