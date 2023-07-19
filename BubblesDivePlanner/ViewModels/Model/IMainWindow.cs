using BubblesDivePlanner.ViewModels.Model.Headers;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan;

namespace BubblesDivePlanner.ViewModels.Model
{
    public interface IMainWindow
    {
        IHeader Header { get; set; }
        IDivePlanner Planner { get; set; }
    }
}