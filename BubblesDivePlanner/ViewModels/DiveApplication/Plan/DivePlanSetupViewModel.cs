using BubblesDivePlanner.Contracts.Services;
using BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.DiveApplication.Plan
{
    public class DivePlanSetupViewModel : ViewModelBase, IDivePlanSetupViewModel
    {
        private IDiveProfileService _diveProfileService;
        
        public DivePlanSetupViewModel(IDiveProfileService diveProfileService)
        {
            _diveProfileService = diveProfileService;
            _diveModelSelector = new DiveModelSelectorViewModel(_diveProfileService);
        }

        private IGasMixtureSelectorViewModel _gasMixture = new GasMixtureSelectorViewModel();
        public IGasMixtureSelectorViewModel GasMixture
        {
            get => _gasMixture;
            set => this.RaiseAndSetIfChanged(ref _gasMixture, value);
        }

        private IDiveModelSelectorViewModel _diveModelSelector;
        public IDiveModelSelectorViewModel DiveModelSelector
        {
            get => _diveModelSelector;
            set => this.RaiseAndSetIfChanged(ref _diveModelSelector, value);
        }

        private IDiveStepViewModel _diveStep = new DiveStepViewModel();
        public IDiveStepViewModel DiveStep
        {
            get => _diveStep;
            set => this.RaiseAndSetIfChanged(ref _diveStep, value);
        }

        private IGasManagementViewModel _gasManagement = new GasManagementViewModel();
        public IGasManagementViewModel GasManagement
        {
            get => _gasManagement;
            set => this.RaiseAndSetIfChanged(ref _gasManagement, value);
        }
    }
}
