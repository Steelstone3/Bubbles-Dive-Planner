using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.GasManagement;

namespace BubblesDivePlanner.ApplicationEntry
{
    public interface IMainWindowModel
    {
        IDiveStepModel DiveStep { get; set; }
        IGasManagementModel GasManagement { get; set; }
    }
}