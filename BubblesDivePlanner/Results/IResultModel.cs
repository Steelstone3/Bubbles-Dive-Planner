using BubblesDivePlanner.DiveModels.DiveProfile;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Cylinders.CylinderSetup;

namespace BubblesDivePlanner.Results
{
    public interface IResultModel
    {
        IDiveStepModel DiveStep { get; set; }
        IDiveProfileModel DiveProfile { get; set; }
        ICylinderSetupModel SelectedCylinder { get; set; }
    }
}