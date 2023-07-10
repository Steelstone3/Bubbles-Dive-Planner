using BubblesDivePlanner.ViewModels.Models.Plan;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Plan
{
    public class GasUsage : ReactiveObject, IGasUsage
    {
        private ushort gasRemaining;
        public ushort GasRemaining
        {
            get => gasRemaining;
            set => this.RaiseAndSetIfChanged(ref gasRemaining, value);
        }

        private ushort gasUsed;
        public ushort GasUsed
        {
            get => gasUsed;
            set => this.RaiseAndSetIfChanged(ref gasUsed, value);
        }

        private ushort surfaceAirConsumptionRate;
        public ushort SurfaceAirConsumptionRate
        {
            get => surfaceAirConsumptionRate;
            set => this.RaiseAndSetIfChanged(ref surfaceAirConsumptionRate, value);
        }
    }
}