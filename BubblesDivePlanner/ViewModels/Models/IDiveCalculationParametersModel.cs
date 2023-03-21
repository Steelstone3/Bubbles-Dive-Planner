namespace BubblesDivePlanner.ViewModels.Models
{
    public interface IDiveCalculationParametersModel
    {
        IDiveStepModel DiveStep { get; set; }
        IDiveModel DiveModel { get; }
        ICylinderSetupModel SelectedCylinder { get; }
    }
}