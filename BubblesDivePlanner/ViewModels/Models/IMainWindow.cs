using BubblesDivePlanner.ViewModels.Models.DiveInformation;
using BubblesDivePlanner.ViewModels.Models.DivePlan;
using BubblesDivePlanner.ViewModels.Models.Header;

namespace BubblesDivePlanner.ViewModels.Models
{
    public interface IMainWindow
    {
        IHeader Header { get; set; }
        IPlanner Planner { get; set; }
        IInformation Information { get; set; }
    }
}