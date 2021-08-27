using BubblesDivePlanner.Contracts.Models.Plan;

namespace BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan
{
    public interface IGasManagementViewModel : IGasManagementModel, IVisibility
    {
        bool IsGasUsageVisible { get; set; }

        bool ValidateGasManagement(int cylinderVolume, int cylinderPressure, int sacRate);
    }
}