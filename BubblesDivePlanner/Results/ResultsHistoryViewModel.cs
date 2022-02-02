using System.Collections.ObjectModel;

namespace BubblesDivePlanner.Results
{
    public class ResultsHistoryViewModel : IResultsHistoryModel
    {
        public ObservableCollection<IResultModel> Results
        {
            get;
        } = new ObservableCollection<IResultModel>();
    }
}