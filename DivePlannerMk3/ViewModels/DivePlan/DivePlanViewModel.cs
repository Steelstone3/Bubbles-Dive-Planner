using DivePlannerMk3.Contracts;
using DivePlannerMk3.ViewModels.DiveResult;
using ReactiveUI;
using DivePlannerMk3.Controllers.ModelConverters;

namespace DivePlannerMk3.ViewModels.DivePlan
{
    public class DivePlanViewModel : ViewModelBase
    {
        private GasMixtureSelectorViewModel _gasMixture = new GasMixtureSelectorViewModel();
        public GasMixtureSelectorViewModel GasMixture
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

        private IDiveProfileService _diveProfileService;
        public DivePlanViewModel(IDiveProfileService diveProfileService)
        {
            _diveProfileService = diveProfileService;
            _diveModelSelector = new PlanDiveModelSelectorViewModel(_diveProfileService);
        }

        public void CalculateDiveStep(DiveResultsViewModel diveResults)
        {
            CalculateDiveSteps(diveResults);
            UpdateUiVisibility();
        }

        public DiveParametersResultViewModel UpdateUsedParameters(DiveParametersResultViewModel diveParameterResults)
        {
            //TODO AH put the converter in the update parameters used method on the dive service...
            //TODO AH or just output the viewmodel/ interface
            var converter = new DiveParametersResultModelConverter();
            return converter.ConvertToViewModel(_diveProfileService.UpdateParametersUsed(DiveStep, GasMixture.SelectedGasMixture, GasManagement));
        }

        private void CalculateDiveSteps(DiveResultsViewModel diveResults)
        {
            diveResults.DiveProfileResults.Add(_diveProfileService.RunDiveStep(DiveStep, GasMixture.SelectedGasMixture));
        }

        private void UpdateUiVisibility()
        {
            GasManagement.IsGasUsageVisible = true;

            DiveModelSelector.IsUiVisible = false;
            DiveModelSelector.IsUiEnabled = false;

            GasManagement.IsUiVisible = false;
            GasManagement.IsUiEnabled = false;
        }

        //Move this off the view model to a controller with them all in under a strategy pattern prehaps
        /*public DivePlanEntityModel ModelToEntity()
        {
            return new DivePlanEntityModelConverter().ModelToEntity(this);
        }

        public void EntityToModel()
        {
            throw new System.NotImplementedException();
        }*/
    }
}
