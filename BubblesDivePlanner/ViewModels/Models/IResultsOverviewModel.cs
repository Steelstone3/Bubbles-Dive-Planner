using System.Collections.ObjectModel;
using BubblesDivePlanner.Results;

namespace BubblesDivePlanner.ViewModels.Models
{
    public interface IResultsOverviewModel : IVisibility
    {
        ObservableCollection<IResultModel> Results { get; }

        IResultModel LatestResult { get; set; }
    }
}