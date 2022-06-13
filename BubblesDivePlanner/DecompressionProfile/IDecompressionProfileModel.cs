using System.Collections.Generic;
using System.Collections.ObjectModel;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Visibility;

namespace BubblesDivePlanner.DecompressionProfile
{
    public interface IDecompressionProfileModel
    {
        ObservableCollection<IDiveStepModel> DecompressionDiveSteps { get; }
    }
}