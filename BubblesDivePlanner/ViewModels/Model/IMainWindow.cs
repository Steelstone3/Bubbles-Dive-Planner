using BubblesDivePlanner.ViewModels.Model.Headers;
using BubblesDivePlanner.ViewModels.Model.Information;
using BubblesDivePlanner.ViewModels.Model.Plan;

namespace BubblesDivePlanner.ViewModels.Model
{
    public interface IMainWindow
    {
        IHeader Header { get; set; }
        IPlanner Planner { get; set; }
        IDiveInformation Information { get; set; }
    }
}