using BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage;

namespace BubblesDivePlanner.Cylinders.CylinderSetup
{
    public interface ICylinderSetupModel
    {
        string CylinderName { get; set; }
        int CylinderVolume { get; set; }
        int CylinderPressure { get; set; }
        IGasMixtureModel GasMixture { get; set; }
        IGasUsageModel GasUsage { get; set; }
    }
}