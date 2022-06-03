using BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage;
using BubblesDivePlanner.Visibility;

namespace BubblesDivePlanner.Cylinders.CylinderSetup
{
    public interface ICylinderSetupModel : IVisibility
    {
        string CylinderName { get; set; }
        byte CylinderVolume { get; set; }
        ushort CylinderPressure { get; set; }
        IGasMixtureModel GasMixture { get; set; }
        IGasUsageModel GasUsage { get; set; }
    }
}