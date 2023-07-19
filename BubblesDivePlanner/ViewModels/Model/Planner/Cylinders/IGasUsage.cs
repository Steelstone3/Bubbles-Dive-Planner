namespace BubblesDivePlanner.ViewModels.Model.Planner.Cylinders
{
    public interface IGasUsage : IVisibility
    {
        ushort GasRemaining { get; set; }
        ushort GasUsed { get; set; }
        ushort SurfaceAirConsumptionRate { get; set; }
    }
}