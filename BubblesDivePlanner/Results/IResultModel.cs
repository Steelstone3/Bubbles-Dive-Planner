using BubblesDivePlanner.DiveModels.DiveProfile;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Cylinders.CylinderSetup;

namespace BubblesDivePlanner.Results
{
    public interface IResultModel
    {
        IDiveStepModel DiveStepModel { get; set; }
        IDiveProfileModel DiveProfileModel { get; set; }
        ICylinderSetupModel CylinderSetupModel { get; set; }
    }
}