using System.Collections.ObjectModel;

namespace BubblesDivePlanner.Results
{
    public interface IResultsOverviewModel
    {
        ObservableCollection<IResultModel> Results { get; }

        IResultModel LatestResult { get; set; }
    }
}