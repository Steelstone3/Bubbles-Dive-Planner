using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlanner.ViewModels.Planner.Cylinders;

namespace BubblesDivePlanner.ViewModels.Model.Planner.Setup
{
    public interface IDiveSetup
    {
        public IDiveModelSelection DiveModelSelection { get; set; }
        public ICylinderSelectionVM CylinderSelection { get; set; }
    }
}