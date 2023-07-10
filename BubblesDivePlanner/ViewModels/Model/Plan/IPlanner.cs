using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Models.Plans;

namespace BubblesDivePlanner.ViewModels.Models.Plan
{
    public interface IPlanner
    {
        // DiveModels
        ICylinderSelection CylinderSelection { get; set; }
        IDiveStep DiveStep { get; set; }
        ICylinder Cylinder { get; set; }
    }
}