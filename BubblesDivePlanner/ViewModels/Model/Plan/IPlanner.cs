using BubblesDivePlanner.ViewModels.Model.Plan.Cylinders;

namespace BubblesDivePlanner.ViewModels.Model.Plan
{
    public interface IPlanner
    {
        // DiveModels
        ICylinderSelection CylinderSelection { get; set; }
        IDiveStep DiveStep { get; set; }
        ICylinder Cylinder { get; set; }
    }
}