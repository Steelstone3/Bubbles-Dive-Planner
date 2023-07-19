using System.Collections.ObjectModel;

namespace BubblesDivePlanner.ViewModels.Model.Planner.DiveModels
{
    public interface IDiveModelSelection
    {
        ObservableCollection<IDiveModel> DiveModels { get; }
    }
}