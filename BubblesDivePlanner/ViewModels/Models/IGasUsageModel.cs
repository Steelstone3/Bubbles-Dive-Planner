using BubblesDivePlanner.Visibility;

namespace BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage
{
    public interface IGasUsageModel : IVisibility
    {
        ushort GasUsed { get; set; }
        ushort GasRemaining { get; set; }
        byte SurfaceAirConsumptionRate { get; set; }
        void UpdateGasRemaining();
    }
}