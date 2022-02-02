using BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage;
using BubblesDivePlanner.Visibility;

namespace BubblesDivePlanner.Cylinders.CylinderSetup
{
    public interface ICylinderSetupModel : IVisibility
    {
        string CylinderName { get; set; }
        int CylinderVolume { get; set; }
        int CylinderPressure { get; set; }
        IGasMixtureModel GasMixture { get; set; }
        IGasUsageModel GasUsage { get; set; }
    }
}