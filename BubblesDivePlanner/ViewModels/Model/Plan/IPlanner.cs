using BubblesDivePlanner.ViewModels.Model.Plan.Cylinders;
using BubblesDivePlanner.ViewModels.Model.Plan.DiveModels;
using BubblesDivePlanner.ViewModels.Model.Plan.Information;
using BubblesDivePlanner.ViewModels.Plan.Cylinders;

namespace BubblesDivePlanner.ViewModels.Model.Plan
{
    public interface IPlanner
    {
        // TODO Add commands to this for calculating the dive
        IDiveModelSelection DiveModelSelection { get; set; }
        ICylinderSelectionVM CylinderSelection { get; set; }
        IDiveInformation Information { get; set; }
        IDiveStage DiveStage { get; set; }
    }
}