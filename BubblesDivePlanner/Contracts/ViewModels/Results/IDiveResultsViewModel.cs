using System.Collections.ObjectModel;
using BubblesDivePlanner.Contracts.Models.Results;
using BubblesDivePlanner.Models.Results;
using BubblesDivePlanner.ViewModels.Result;

namespace BubblesDivePlanner.Contracts.ViewModels.Results
{
    public interface IDiveResultsViewModel : IVisibility
    {
        ObservableCollection<IDiveResultsStepOutputModel> DiveProfileResults { get; }

        IDiveParametersResultViewModel DiveParametersResult { get; set; }
    }
}