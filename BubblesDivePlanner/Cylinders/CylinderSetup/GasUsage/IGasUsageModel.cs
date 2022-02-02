using BubblesDivePlanner.Visibility;

namespace BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage
{
    public interface IGasUsageModel : IVisibility
    {
        int InitialPressurisedCylinderVolume { get; set; }
        int GasUsed { get; set; }
        int GasRemaining { get; set; }
        int SurfaceAirConsumptionRate { get; set; }
    }
}