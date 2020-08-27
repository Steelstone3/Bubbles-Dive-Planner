using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DivePlan
{
    public class DivePlanViewModel : ViewModelBase
    {
        public DivePlanViewModel()
        {
            DiveModelSelector = new PlanDiveModelSelectorViewModel();
            DiveStep = new PlanDiveStepViewModel();
            GasManagement = new PlanGasManagementViewModel();
            GasMixture = new PlanGasMixtureViewModel();
            AddGasMixture = new PlanAddGasMixtureViewModel();
        }

        private PlanAddGasMixtureViewModel _addGasMixture;
        public PlanAddGasMixtureViewModel AddGasMixture
        {
            get => _addGasMixture;
            set => this.RaiseAndSetIfChanged( ref _addGasMixture, value );
        }

        private PlanGasMixtureViewModel _gasMixture;
        public PlanGasMixtureViewModel GasMixture
        {
            get => _gasMixture;
            set => this.RaiseAndSetIfChanged( ref _gasMixture, value );
        }

        private PlanDiveModelSelectorViewModel _diveModelSelector;
        public PlanDiveModelSelectorViewModel DiveModelSelector
        {
            get => _diveModelSelector;
            set => this.RaiseAndSetIfChanged( ref _diveModelSelector, value );
        }

        private PlanDiveStepViewModel _diveStep;
        public PlanDiveStepViewModel DiveStep
        {
            get => _diveStep;
            set => this.RaiseAndSetIfChanged( ref _diveStep, value );
        }

        private PlanGasManagementViewModel _gasManagement;
        public PlanGasManagementViewModel GasManagement
        {
            get => _gasManagement;
            set => this.RaiseAndSetIfChanged( ref _gasManagement, value );
        }
    }
}
