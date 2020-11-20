using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DiveInfo
{
    public class InfoDiveBoundariesViewModel : ViewModelBase
    {
        private int _maximumOperatingDepth;
        public int MaximumOperatingDepth
        {
            get => _maximumOperatingDepth;
            set => this.RaiseAndSetIfChanged( ref _maximumOperatingDepth, value );
        }

        private int _diveCeiling;
        public int DiveCeiling
        {
            get => _diveCeiling;
            set => this.RaiseAndSetIfChanged( ref _diveCeiling, value );
        }
    }
}
