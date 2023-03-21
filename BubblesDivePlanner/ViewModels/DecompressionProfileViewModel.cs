using System.Collections.Generic;
using System.Collections.ObjectModel;
using BubblesDivePlanner.ViewModels.Models;
using ReactiveUI;

namespace BubblesDivePlanner.DecompressionProfile
{
    public class DecompressionProfileViewModel : ReactiveObject, IDecompressionProfileModel
    {
        public ObservableCollection<IDiveStepModel> DecompressionDiveSteps
        {
            get;
        } = new ObservableCollection<IDiveStepModel>();
    }
}