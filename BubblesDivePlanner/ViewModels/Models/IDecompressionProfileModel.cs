using System.Collections.ObjectModel;

namespace BubblesDivePlanner.ViewModels.Models
{
    public interface IDecompressionProfileModel
    {
        ObservableCollection<IDiveStepModel> DecompressionDiveSteps { get; }
    }
}