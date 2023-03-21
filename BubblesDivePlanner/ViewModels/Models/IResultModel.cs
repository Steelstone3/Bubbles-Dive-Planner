using BubblesDivePlanner.ViewModels.Models;

namespace BubblesDivePlanner.Results
{
    public interface IResultModel
    {
        IDiveStepModel DiveStep { get; set; }
        IDiveProfileModel DiveProfile { get; set; }
        ICylinderSetupModel SelectedCylinder { get; set; }
    }
}