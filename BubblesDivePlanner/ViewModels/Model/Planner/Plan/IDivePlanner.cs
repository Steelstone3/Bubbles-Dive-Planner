using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan.Information;
using BubblesDivePlanner.ViewModels.Planner.Cylinders;

namespace BubblesDivePlanner.ViewModels.Model.Planner.Plan
{
    public interface IDivePlanner
    {
        // TODO Add commands to this for calculating the dive
        IDiveModelSelection DiveModelSelection { get; set; }
        ICylinderSelectionVM CylinderSelection { get; set; }
        IDiveInformation Information { get; set; }
        IDiveStage DiveStage { get; set; }
    }
}