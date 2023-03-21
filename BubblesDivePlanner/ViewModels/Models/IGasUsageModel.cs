namespace BubblesDivePlanner.ViewModels.Models
{
    public interface IGasUsageModel : IVisibility
    {
        ushort GasUsed { get; set; }
        ushort GasRemaining { get; set; }
        byte SurfaceAirConsumptionRate { get; set; }
        void UpdateGasRemaining();
    }
}