namespace BubblesDivePlanner.ViewModels.Models
{
    public interface IDivePlanModel
    {
        IDiveStepModel DiveStep { get; }
        IDiveModel DiveModel { get; }
        ICylinderSetupModel SelectedCylinder { get; }
    }
}