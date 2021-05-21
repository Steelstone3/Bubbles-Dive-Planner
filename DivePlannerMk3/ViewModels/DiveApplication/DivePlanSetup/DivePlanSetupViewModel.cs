using DivePlannerMk3.Contracts;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DivePlan
{
    public class DivePlanSetupViewModel : ViewModelBase
    {
        private IDiveProfileService _diveProfileService;
        
        public DivePlanSetupViewModel(IDiveProfileService diveProfileService)
        {
            _diveProfileService = diveProfileService;
            _diveModelSelector = new DiveModelSelectorViewModel(_diveProfileService);
        }

        private GasMixtureSelectorViewModel _gasMixture = new GasMixtureSelectorViewModel();
        public GasMixtureSelectorViewModel GasMixture
        {
            get => _gasMixture;
            set => this.RaiseAndSetIfChanged(ref _gasMixture, value);
        }

        private DiveModelSelectorViewModel _diveModelSelector;
        public DiveModelSelectorViewModel DiveModelSelector
        {
            get => _diveModelSelector;
            set => this.RaiseAndSetIfChanged(ref _diveModelSelector, value);
        }

        private DiveStepViewModel _diveStep = new DiveStepViewModel();
        public DiveStepViewModel DiveStep
        {
            get => _diveStep;
            set => this.RaiseAndSetIfChanged(ref _diveStep, value);
        }

        private GasManagementViewModel _gasManagement = new GasManagementViewModel();
        public GasManagementViewModel GasManagement
        {
            get => _gasManagement;
            set => this.RaiseAndSetIfChanged(ref _gasManagement, value);
        }
    }
}
