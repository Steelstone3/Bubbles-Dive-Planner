using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DiveInfo
{
    public class DiveBoundariesViewModel : ViewModelBase
    {
        public DiveBoundariesViewModel()
        {
            IsUiVisible = false;
        }

        private int _diveCeiling;
        public int DiveCeiling
        {
            get => _diveCeiling;
            set => this.RaiseAndSetIfChanged(ref _diveCeiling, value);
        }
    }
}
