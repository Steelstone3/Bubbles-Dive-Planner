namespace BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan
{
    public interface IGasManagementViewModel : IVisibility
    {
        #region Gas Setup

        int CylinderVolume { get; set; }

        int CylinderPressure { get; set; }

        int SacRate { get; set; }

        int InitialCylinderTotalVolume { get; set; }

        #endregion

        #region Gas Usage
        
        bool IsGasUsageVisible { get; set; }

        int GasUsedForStep { get; set; }

        int GasRemaining { get; set; }

        #endregion

        bool ValidateGasManagement(int cylinderVolume, int cylinderPressure, int sacRate);
    }
}