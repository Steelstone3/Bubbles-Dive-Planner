using System.Collections.ObjectModel;

namespace BubblesDivePlanner.ViewModels.Model.Plan.DiveModels
{
    public interface IDiveModelSelection
    {
        ObservableCollection<IDiveModel> DiveModels { get; }
    }
}