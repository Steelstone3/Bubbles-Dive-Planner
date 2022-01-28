namespace BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage
{
    public interface IGasUsageModel
    {
        int InitialPressurisedCylinderVolume { get; set; }
        int GasUsed { get; set; }
        int GasRemaining { get; set; }
        int SurfaceAirConsumptionRate { get; set; }
    }
}