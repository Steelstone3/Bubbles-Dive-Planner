using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DiveInfo
{
    public class InfoGasManagementReadOnlyViewModel : ViewModelBase
    {
        private bool _uiEnabled = false;
        public bool UiEnabled
        {
            get => _uiEnabled;
            set
            {
                if (_uiEnabled != value)
                {
                    _uiEnabled = value;
                    this.RaisePropertyChanged(nameof(UiEnabled));
                }
            }
        }

        private int _gasUsedForStep;
        public int GasUsedForStep
        {
            get => _gasUsedForStep;
            set
            {
                if (_gasUsedForStep != value)
                {
                    _gasUsedForStep = value;
                    this.RaisePropertyChanged(nameof(GasUsedForStep));
                }
            }
        }

        private int _gasRemaining;
        public int GasRemaining
        {
            get => _gasRemaining;
            set
            {
                if (_gasRemaining != value)
                {
                    _gasRemaining = value;
                    this.RaisePropertyChanged(nameof(GasRemaining));
                }
            }
        }
    }
}