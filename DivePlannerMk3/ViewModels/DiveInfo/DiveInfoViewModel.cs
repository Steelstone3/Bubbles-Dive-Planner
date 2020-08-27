using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DiveInfo
{
    public class DiveInfoViewModel : ViewModelBase
    {
        public DiveInfoViewModel()
        {
            DecompressionProfile = new InfoDecompressionProfileViewModel();
            DiveBoundaries = new InfoDiveBoundariesViewModel();
        }

        private InfoDecompressionProfileViewModel _decompressionProfile;
        public InfoDecompressionProfileViewModel DecompressionProfile
        {
            get => _decompressionProfile;
            set => this.RaiseAndSetIfChanged( ref _decompressionProfile, value );
        }

        private InfoDiveBoundariesViewModel _diveBoundaries;
        public InfoDiveBoundariesViewModel DiveBoundaries
        {
            get => _diveBoundaries;
            set => this.RaiseAndSetIfChanged( ref _diveBoundaries, value );
        }
    }
}
