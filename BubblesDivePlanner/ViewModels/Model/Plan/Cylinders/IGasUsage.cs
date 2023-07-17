namespace BubblesDivePlanner.ViewModels.Model.Plan.Cylinders
{
    public interface IGasUsage
    {
        ushort GasRemaining { get; set; }
        ushort GasUsed { get; set; }
        ushort SurfaceAirConsumptionRate { get; set; }
    }
}