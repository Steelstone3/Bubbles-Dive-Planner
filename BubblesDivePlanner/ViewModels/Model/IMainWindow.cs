using BubblesDivePlanner.ViewModels.Model.Headers;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan;
using BubblesDivePlanner.ViewModels.Planner.Plan;

namespace BubblesDivePlanner.ViewModels.Model
{
    public interface IMainWindow
    {
        IHeader Header { get; set; }
        IDivePlannerVM Planner { get; set; }
    }
}