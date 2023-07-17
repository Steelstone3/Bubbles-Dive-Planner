using BubblesDivePlanner.ViewModels.Model.Headers;
using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Model.Plan.Information;

namespace BubblesDivePlanner.ViewModels.Model
{
    public interface IMainWindow
    {
        IHeader Header { get; set; }
        IPlanner Planner { get; set; }
    }
}