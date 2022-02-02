using ReactiveUI;

namespace BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage
{
    public class GasUsageViewModel : ReactiveObject, IGasUsageModel
    {
        private int _initialPressurisedCylinderVolume;
        public int InitialPressurisedCylinderVolume
        {
            get => _initialPressurisedCylinderVolume;
            set => this.RaiseAndSetIfChanged(ref _initialPressurisedCylinderVolume, value);
        }

        private int _gasRemaining;
        public int GasRemaining
        {
            get => _gasRemaining;
            set => this.RaiseAndSetIfChanged(ref _gasRemaining, value);
        }

        private int _gasUsed;
        public int GasUsed
        {
            get => _gasUsed;
            set => this.RaiseAndSetIfChanged(ref _gasUsed, value);
        }

        private int _surfaceAirConsumptionRate;
        public int SurfaceAirConsumptionRate
        {
            get => _surfaceAirConsumptionRate;
            set => this.RaiseAndSetIfChanged(ref _surfaceAirConsumptionRate, value);
        }

        private bool _isVisible = false;
        public bool IsVisible
        {
            get => _isVisible;
            set => this.RaiseAndSetIfChanged(ref _isVisible, value);
        }
    }
}