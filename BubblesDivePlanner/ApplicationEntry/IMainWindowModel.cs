using BubblesDivePlanner.Cylinders.CylinderSelector;
using BubblesDivePlanner.DiveStep;

namespace BubblesDivePlanner.ApplicationEntry
{
    public interface IMainWindowModel
    {
        IDiveStepModel DiveStep { get; set; }
        ICylinderSelectorModel GasManagement { get; set; }
    }
}