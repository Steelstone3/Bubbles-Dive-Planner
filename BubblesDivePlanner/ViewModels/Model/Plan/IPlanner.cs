using BubblesDivePlanner.ViewModels.Models.Plans;

namespace BubblesDivePlanner.ViewModels.Models.Plan
{
    public interface IPlanner
    {
        // DiveModels
        // DiveModel
        // Cylinders
        // Cylinder
        IDiveStep DiveStep { get; set; }
        ICylinder Cylinder { get; set; }
    }
}