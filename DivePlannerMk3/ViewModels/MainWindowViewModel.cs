using System;
using System.Reactive;
using DivePlannerMk3.Contracts;
using DivePlannerMk3.Controllers;
using DivePlannerMk3.ViewModels.DiveHeader;
using DivePlannerMk3.ViewModels.DiveInfo;
using DivePlannerMk3.ViewModels.DivePlan;
using DivePlannerMk3.ViewModels.DiveResult;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IDiveProfileService _diveProfileController;
        //private IGasManagementController _gasManagementController;

        public MainWindowViewModel()
        {
            _diveProfileController = new DiveProfileService();
            _divePlan = new DivePlanViewModel(_diveProfileController);

            //_gasManagementController = new GasManagementController();

            CalculateDiveStepCommand = ReactiveCommand.Create(RunDiveStep, CanExecuteDiveStep); // create command
        }

        private DiveResultsViewModel _diveResults = new DiveResultsViewModel();
        public DiveResultsViewModel DiveResults
        {
            get => _diveResults;
            set => this.RaiseAndSetIfChanged(ref _diveResults, value);
        }

        private DiveParametersResultsViewModel _diveParametersResults = new DiveParametersResultsViewModel();
        public DiveParametersResultsViewModel DiveParametersResults
        {
            get => _diveParametersResults;
            set => this.RaiseAndSetIfChanged(ref _diveParametersResults, value);
        }

        private DivePlanViewModel _divePlan;
        public DivePlanViewModel DivePlan
        {
            get => _divePlan;
            set => this.RaiseAndSetIfChanged(ref _divePlan, value);
        }

        private DiveInfoViewModel _diveInfo = new DiveInfoViewModel();
        public DiveInfoViewModel DiveInfo
        {
            get => _diveInfo;
            set => this.RaiseAndSetIfChanged(ref _diveInfo, value);
        }

        //TODO AH need to rename this for all the file, edit functionality
        private DiveHeaderViewModel _diveHeader = new DiveHeaderViewModel();
        public DiveHeaderViewModel DiveHeader
        {
            get => _diveHeader;
            set => this.RaiseAndSetIfChanged(ref _diveHeader, value);
        }

        public ReactiveCommand<Unit, Unit> CalculateDiveStepCommand
        {
            get;
        }

        public IObservable<bool> CanExecuteDiveStep
        {
            get => this.WhenAnyValue(vm => vm.DivePlan.GasMixture.SelectedGasMixture, vm => vm.DivePlan.DiveModelSelector.SelectedDiveModel, (selectedGasMixture, selectedDiveModel) => selectedGasMixture != null && selectedDiveModel != null);
        }

        private void RunDiveStep()
        {
            UpdateUiElementsVisibility();
            
            //TODO This could potentially be moved down another layer to DivePlan itself an event would need to be set up for all UI visibiltiy
            //As in pass in dive step and gas mixture which returns a result

            //CalculateGasUsage();

            var result = _diveProfileController.RunDiveStep(DivePlan.DiveStep.DiveStepModel, DivePlan.GasMixture.SelectedGasMixture);
            var parametersUsed = _diveProfileController.UpdateParametersUsed(DivePlan.DiveStep.DiveStepModel, DivePlan.GasMixture.SelectedGasMixture);
           
            //TODO AH fix this line
            DiveParametersResults.DiveParametersUsed = parametersUsed;
            DiveResults.DiveProfileResults.Add(result);

        }

        private void UpdateUiElementsVisibility()
        {
            DivePlan.DiveModelSelector.UiEnabled = false;
            DivePlan.GasManagement.UiEnabled = false;

            DiveInfo.InfoGasManagementReadOnly.UiEnabled = true;
            DiveInfo.InfoDiveModelSelectedReadOnly.UiEnabled = true;
            DiveInfo.DiveBoundaries.UiEnabled = true;

            //TODO AH complexity to be added later true when user needs to decompress
            DiveInfo.DecompressionProfile.UiEnabled = true;
        }

        /*private void CalculateGasUsage()
        {
            DiveInfo.InfoGasManagementReadOnly.GasUsedForStep = _gasManagementController.CalculateGasUsed(DivePlan.DiveStep.Depth, DivePlan.DiveStep.Time, DivePlan.GasManagement.GasManagementModel.SacRate);
        }*/
    }
}
