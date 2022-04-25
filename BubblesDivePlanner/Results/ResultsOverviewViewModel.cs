using System.Collections.ObjectModel;
using System.Linq;

namespace BubblesDivePlanner.Results
{
    public class ResultsOverviewViewModel : IResultsOverviewModel
    {
        public ObservableCollection<IResultModel> Results
        {
            get;
        } = new ObservableCollection<IResultModel>();

        public IResultModel LatestResult => Results.Last();
    }
}