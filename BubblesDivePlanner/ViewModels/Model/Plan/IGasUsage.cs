using System.Collections.Generic;

namespace BubblesDivePlanner.ViewModels.Models.Plan
{
    public interface IGasUsage
    {
        ushort GasRemaining { get; set; }
        ushort GasUsed { get; set; }
        ushort SurfaceAirConsumptionRate { get; set; }
    }
}