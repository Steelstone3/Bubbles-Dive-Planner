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

        private InfoDiveBoundariesViewModel _diveBoundaries = new InfoDiveBoundariesViewModel();
        public InfoDiveBoundariesViewModel DiveBoundaries
        {
            get => _diveBoundaries;
            set => this.RaiseAndSetIfChanged(ref _diveBoundaries, value);
        }

        private InfoGasUsageViewModel _infoGasUsage = new InfoGasUsageViewModel();
        public InfoGasUsageViewModel InfoGasUsage
        {
            get => _infoGasUsage;
            set => this.RaiseAndSetIfChanged(ref _infoGasUsage, value);
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
            InfoGasUsage.UiEnabled = true;
            InfoDiveModelSelectedReadOnly.UiEnabled = true;
            DiveBoundaries.UiEnabled = true;

            //TODO AH complexity to be added later true when user needs to decompress
            DecompressionProfile.UiEnabled = true;
        }
    }
}
