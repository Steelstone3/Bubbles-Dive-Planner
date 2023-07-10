using BubblesDivePlanner.ViewModels.Models.Headers;
using BubblesDivePlanner.ViewModels.Models.Informations;
using BubblesDivePlanner.ViewModels.Models.Plan;

namespace BubblesDivePlanner.ViewModels.Models
{
    public interface IMainWindow
    {
        IHeader Header { get; set; }
        IPlanner Planner { get; set; }
        IDiveInformation Information { get; set; }
    }
}