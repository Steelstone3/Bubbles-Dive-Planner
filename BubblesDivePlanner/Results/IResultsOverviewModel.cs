using System.Collections.ObjectModel;

namespace BubblesDivePlanner.Results
{
    public interface IResultsHistoryModel
    {
        ObservableCollection<IResultModel> Results { get; }
        IResultModel LatestResult { get; }
    }
}