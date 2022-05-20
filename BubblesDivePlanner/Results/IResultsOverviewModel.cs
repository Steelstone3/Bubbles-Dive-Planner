using System.Collections.ObjectModel;
using BubblesDivePlanner.Visibility;

namespace BubblesDivePlanner.Results
{
    public interface IResultsOverviewModel : IVisibility
    {
        ObservableCollection<IResultModel> Results { get; }

        IResultModel LatestResult { get; set; }
    }
}