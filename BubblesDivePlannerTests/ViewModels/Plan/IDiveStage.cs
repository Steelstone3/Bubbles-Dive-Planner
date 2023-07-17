using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Model.Plan.Cylinders;
using BubblesDivePlanner.ViewModels.Model.Plan.DiveModels;

namespace BubblesDivePlannerTests.ViewModels.Plan
{
    public interface IDiveStage
    {
        IDiveModel DiveModel { get; set; }
        IDiveStep DiveStep { get; set; }
        ICylinder Cylinder { get; set; }
    }
}