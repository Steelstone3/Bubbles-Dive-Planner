using BubblesDivePlanner.ViewModels.Model.Plan.Cylinders;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Plan.Cylinders
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