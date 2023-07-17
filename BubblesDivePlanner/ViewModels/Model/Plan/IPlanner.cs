using BubblesDivePlanner.ViewModels.Model.Plan.Cylinders;
using BubblesDivePlanner.ViewModels.Model.Plan.DiveModels;

namespace BubblesDivePlanner.ViewModels.Model.Plan
{
    public interface IPlanner
    {
        // TODO Add commands to this for calculating the dive

        IDiveModelSelection DiveModelSelection { get; set; }
        ICylinderSelection CylinderSelection { get; set; }

        // TODO combine these three to make it easy to serialise
        IDiveModel DiveModel { get; set; }
        IDiveStep DiveStep { get; set; }
        ICylinder Cylinder { get; set; }
    }
}