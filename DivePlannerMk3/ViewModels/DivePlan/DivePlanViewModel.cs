using DivePlannerMk3.Contracts;
using DivePlannerMk3.ViewModels.DiveResult;
using ReactiveUI;
using DivePlannerMk3.Controllers.ModelConverters;

namespace DivePlannerMk3.ViewModels.DivePlan
{
    public class DivePlanViewModel : ViewModelBase
    {
        private PlanGasMixtureViewModel _gasMixture = new PlanGasMixtureViewModel();
        public PlanGasMixtureViewModel GasMixture
        {
            get => _gasMixture;
            set => this.RaiseAndSetIfChanged(ref _gasMixture, value);
        }

        private PlanDiveModelSelectorViewModel _diveModelSelector;
        public PlanDiveModelSelectorViewModel DiveModelSelector
        {
            get => _diveModelSelector;
            set => this.RaiseAndSetIfChanged(ref _diveModelSelector, value);
        }

        private PlanDiveStepViewModel _diveStep = new PlanDiveStepViewModel();
        public PlanDiveStepViewModel DiveStep
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

        private IDiveProfileService _diveProfileService;
        public DivePlanViewModel(IDiveProfileService diveProfileService)
        {
            _diveProfileService = diveProfileService;
            _diveModelSelector = new PlanDiveModelSelectorViewModel(_diveProfileService);
        }

        public void CalculateDiveStep(DiveResultsViewModel diveResults, DiveParametersResultViewModel diveParametersResult)
        {
            CalculateDiveSteps(diveResults);
            UpdateUsedParameters(diveParametersResult);
            UpdateUiVisibility();
        }

        private void CalculateDiveSteps(DiveResultsViewModel diveResults)
        {
            diveResults.DiveProfileResults.Add(_diveProfileService.RunDiveStep(DiveStep, GasMixture.SelectedGasMixture) );
        }

        private void UpdateUsedParameters(DiveParametersResultViewModel diveParameterResults)
        {
            var converter = new DiveParametersResultModelConverter();
            diveParameterResults = converter.ConvertToViewModel(_diveProfileService.UpdateParametersUsed(DiveStep, GasMixture.SelectedGasMixture, GasManagement));
        }

        private void UpdateUiVisibility()
        {
            GasManagement.IsGasUsageVisible = true;

            DiveModelSelector.IsUiVisible = false;
            DiveModelSelector.IsUiEnabled = false;

            GasManagement.IsUiVisible = false;
            GasManagement.IsUiEnabled = false;
        }
    }
}
