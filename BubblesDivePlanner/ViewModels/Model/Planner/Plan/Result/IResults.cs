using System.Collections.ObjectModel;
using BubblesDivePlanner.ViewModels.Model.Plan;

namespace BubblesDivePlanner.ViewModels.Model.Planner.Plan.Result
{
    public interface IResults
    {
        ObservableCollection<IDiveStage> HistoricResults { get; }
        IDiveStage Result { get; set; }
    }
}