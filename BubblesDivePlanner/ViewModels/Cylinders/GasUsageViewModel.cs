using BubblesDivePlanner.Controllers.Cylinders;
using BubblesDivePlanner.ViewModels.Models;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Cylinders
{
    public class GasUsageViewModel : ReactiveObject, IGasUsageModel
    {
        private ushort _gasRemaining;
        public ushort GasRemaining
        {
            get => _gasRemaining;
            set => this.RaiseAndSetIfChanged(ref _gasRemaining, value);
        }

        private ushort _gasUsed;
        public ushort GasUsed
        {
            get => _gasUsed;
            set => this.RaiseAndSetIfChanged(ref _gasUsed, value);
        }

        private byte _surfaceAirConsumptionRate;
        public byte SurfaceAirConsumptionRate
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

        public void UpdateGasRemaining()
        {
            GasRemaining = new GasUsageController().CalculateRemainingPressurisedCylinderVolume(_gasRemaining, _gasUsed);
        }
    }
}