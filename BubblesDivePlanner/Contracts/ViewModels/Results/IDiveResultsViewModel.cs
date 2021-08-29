using System.Collections.ObjectModel;
using BubblesDivePlanner.Contracts.Models.Results;

namespace BubblesDivePlanner.Contracts.ViewModels.Results
{
    public interface IDiveResultsViewModel : IVisibility
    {
        ObservableCollection<IDiveResultsStepOutputModel> DiveProfileResults { get; }

        IDiveParametersResultViewModel DiveParametersResult { get; set; }
    }
}