using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan.Information;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan.Result;
using BubblesDivePlanner.ViewModels.Model.Planner.Setup;

namespace BubblesDivePlanner.ViewModels.Model.Planner.Plan
{
    public interface IDivePlanner
    {
        // TODO Add commands to this for calculating the dive
        IDiveSetup DiveSetup { get; set; }
        IDiveInformation Information { get; set; }
        IDiveStage DiveStage { get; set; }
        IResults Results { get; set; }
    }
}