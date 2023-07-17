using BubblesDivePlanner.ViewModels.Model.Plan.Cylinders;
using BubblesDivePlanner.ViewModels.Model.Plan.DiveModels;
using BubblesDivePlanner.ViewModels.Model.Plan.Information;

namespace BubblesDivePlanner.ViewModels.Model.Plan
{
    public interface IPlanner
    {
        // TODO Add commands to this for calculating the dive
        IDiveModelSelection DiveModelSelection { get; set; }
        ICylinderSelection CylinderSelection { get; set; }
        IDiveInformation Information { get; set; }
        IDiveStage DiveStage { get; set; }
    }
}