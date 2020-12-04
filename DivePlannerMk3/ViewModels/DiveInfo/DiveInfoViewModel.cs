using System;
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

        private DiveBoundariesViewModel _diveBoundaries = new DiveBoundariesViewModel();
        public DiveBoundariesViewModel DiveBoundaries
        {
            get => _diveBoundaries;
            set => this.RaiseAndSetIfChanged(ref _diveBoundaries, value);
        }

        private InfoDiveModelSelectedReadOnlyViewModel _infoDiveModelSelectedReadOnly = new InfoDiveModelSelectedReadOnlyViewModel();
        public InfoDiveModelSelectedReadOnlyViewModel InfoDiveModelSelectedReadOnly
        {
            get => _infoDiveModelSelectedReadOnly;
            set => this.RaiseAndSetIfChanged(ref _infoDiveModelSelectedReadOnly, value);
        }

        public void CalculateDiveStep()
        {
            UpdateUiVisibility();
        }

        private void UpdateUiVisibility()
        {
            InfoDiveModelSelectedReadOnly.IsUiVisible = true;
            DiveBoundaries.IsUiVisible = true;

            //TODO AH complexity to be added later true when user needs to decompress
            DecompressionProfile.IsUiVisible = true;
        }
    }
}
