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

        private PlanGasManagementViewModel _gasManagement = new PlanGasManagementViewModel();
        public PlanGasManagementViewModel GasManagement
        {
            get => _gasManagement;
            set => this.RaiseAndSetIfChanged(ref _gasManagement, value);
        }

        private IDiveProfileService _diveProfileController;
        public DivePlanViewModel(IDiveProfileService diveProfileController)
        {
            _diveProfileController = diveProfileController;
            _diveModelSelector = new PlanDiveModelSelectorViewModel(_diveProfileController);
        }

        public void CalculateDiveStep(DiveResultsViewModel diveResults, DiveParametersResultViewModel diveParametersResult)
        {
            CalculateDiveSteps(diveResults);
            UpdateUsedParameters(diveParametersResult);
            UpdateUiVisibility();
        }

        private void CalculateDiveSteps(DiveResultsViewModel diveResults)
        {
            diveResults.DiveProfileResults.Add(_diveProfileController.RunDiveStep(DiveStep, GasMixture.SelectedGasMixture) );
        }

        private void UpdateUsedParameters(DiveParametersResultViewModel diveParameterResults)
        {
            var converter = new DiveParametersResultModelConverter();
            diveParameterResults = converter.ConvertToViewModel(_diveProfileController.UpdateParametersUsed(DiveStep, GasMixture.SelectedGasMixture));
        }

        private void UpdateUiVisibility()
        {
            DiveModelSelector.UiEnabled = false;
            GasManagement.UiEnabled = false;
        }
    }
}
