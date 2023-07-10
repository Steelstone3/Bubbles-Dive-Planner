namespace BubblesDivePlanner.ViewModels.Model.Plan
{
    public interface IGasUsage
    {
        ushort GasRemaining { get; set; }
        ushort GasUsed { get; set; }
        ushort SurfaceAirConsumptionRate { get; set; }
    }
}