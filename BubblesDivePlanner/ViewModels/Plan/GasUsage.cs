using BubblesDivePlanner.ViewModels.Models.Plan;

namespace BubblesDivePlanner.ViewModels.Plan
{
    public class GasUsage : IGasUsage
    {
        public ushort GasRemaining
        {
            get;
            set;
        }

        public ushort GasUsed
        {
            get;
            set;
        }

        public ushort SurfaceAirConsumptionRate
        {
            get;
            set;
        }
    }
}