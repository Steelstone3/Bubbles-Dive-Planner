using BubblesDivePlanner.ViewModels.Model.Planner.Cylinders;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan.Stage;

namespace BubblesDivePlanner.ViewModels.Model.Plan
{
    public interface IDiveStage
    {
        IDiveModel DiveModel { get; set; }
        IDiveStep DiveStep { get; set; }
        ICylinder Cylinder { get; set; }
    }
}