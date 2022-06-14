using System.Collections.ObjectModel;
using BubblesDivePlanner.DiveStep;

namespace BubblesDivePlanner.DecompressionProfile
{
    public interface IDecompressionProfileModel
    {
        ObservableCollection<IDiveStepModel> DecompressionDiveSteps { get; }
    }
}