using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DiveInfo
{
    public class DiveInfoViewModel : ViewModelBase
    {
        public DiveInfoViewModel()
        {
        }

        private InfoDecompressionProfileViewModel _decompressionProfile = new InfoDecompressionProfileViewModel();
        public InfoDecompressionProfileViewModel DecompressionProfile
        {
            get => _decompressionProfile;
            set => this.RaiseAndSetIfChanged(ref _decompressionProfile, value);
        }

        private InfoDiveBoundariesViewModel _diveBoundaries = new InfoDiveBoundariesViewModel();
        public InfoDiveBoundariesViewModel DiveBoundaries
        {
            get => _diveBoundaries;
            set => this.RaiseAndSetIfChanged(ref _diveBoundaries, value);
        }
    }
}
