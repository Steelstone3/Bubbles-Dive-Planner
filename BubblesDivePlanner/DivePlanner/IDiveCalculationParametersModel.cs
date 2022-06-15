using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveStep;

namespace BubblesDivePlanner.DiveCalculationParameters
{
    public interface IDiveCalculationParametersModel
    {
        IDiveStepModel DiveStep { get; set; }
        IDiveModel DiveModel { get; }
        ICylinderSetupModel SelectedCylinder { get; }
    }
}