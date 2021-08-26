using System;
using System.Reactive;
using BubblesDivePlanner.Contracts.Services;
using BubblesDivePlanner.Controllers.Converters;
using BubblesDivePlanner.ViewModels.DiveApplication.Information;
using BubblesDivePlanner.ViewModels.DiveApplication.Plan;
using BubblesDivePlanner.ViewModels.Result;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.DiveApplication
{
    public class DiveApplicationViewModel : ViewModelBase
    {
        private IDiveProfileService _diveProfileService;

        public DiveApplicationViewModel(IDiveProfileService diveProfileService)
        {
            _diveProfileService = diveProfileService;
            DivePlanSetup = new DivePlanSetupViewModel(diveProfileService);
            CalculateDiveStepCommand = ReactiveCommand.Create(RunDiveStep, CanExecuteDiveStep); // create command
        }

        private DivePlanSetupViewModel _divePlanSetup;
        public DivePlanSetupViewModel DivePlanSetup
        {
            get => _divePlanSetup;
            set => this.RaiseAndSetIfChanged(ref _divePlanSetup, value);
        }

        private DiveInformationViewModel _diveInformation = new DiveInformationViewModel();
        public DiveInformationViewModel DiveInformation
        {
            get => _diveInformation;
            set => this.RaiseAndSetIfChanged(ref _diveInformation, value);
        }

        private DiveResultsViewModel _diveResults = new DiveResultsViewModel();
        public DiveResultsViewModel DiveResults
        {
            get => _diveResults;
            set => this.RaiseAndSetIfChanged(ref _diveResults, value);
        }

        public ReactiveCommand<Unit, Unit> CalculateDiveStepCommand
        {
            get;
        }

        //Must make observables atomic e.g vm => vm.DivePlan.DiveModelSelector.SelectedDiveModel
        public IObservable<bool> CanExecuteDiveStep
        {
            get => this.WhenAnyValue(vm => vm.DivePlanSetup.DiveModelSelector.SelectedDiveModel,
            vm => vm.DivePlanSetup.GasMixture.SelectedGasMixture,
            vm => vm.DivePlanSetup.GasManagement.CylinderPressure,
            vm => vm.DivePlanSetup.GasManagement.CylinderVolume,
            vm => vm.DivePlanSetup.GasManagement.SacRate,
            vm => vm.DivePlanSetup.DiveStep.Depth,
            vm => vm.DivePlanSetup.DiveStep.Time,
            vm => vm.DivePlanSetup.GasMixture.MaximumOperatingDepth,
            (selectorDiveModel, selectedGasMixture, cylinderPressure, cylinderVolume, sacRate, depth, time, maximumOperatingDepth) =>
            DivePlanSetup.DiveModelSelector.ValidateSelectedDiveModel(selectorDiveModel)
            && DivePlanSetup.GasMixture.ValidateGasMixture(selectedGasMixture)
            && DivePlanSetup.GasManagement.ValidateGasManagement(cylinderVolume, cylinderPressure, sacRate)
            && DivePlanSetup.DiveStep.ValidateDiveStep(depth, time, maximumOperatingDepth));
        }

        private void RunDiveStep()
        {
            CalculateDiveStep();
            DiveResults.DiveParametersResult = UpdateUsedParameters();
            UpdateUiVisibility();
        }

        private void CalculateDiveStep()
        {
            DiveResults.DiveProfileResults.Add(_diveProfileService.RunDiveStep(DivePlanSetup.DiveStep, DivePlanSetup.GasMixture.SelectedGasMixture));
            //DiveCeilingViewModel.CalculateDiveCeiling(toleratedAmbientPressures);
        }

        private void UpdateUiVisibility()
        {
            DiveInformation.CnsToxicity.IsUiVisible = true;
            DivePlanSetup.GasManagement.IsUiVisible = false;
            DivePlanSetup.GasManagement.IsGasUsageVisible = true;
            DivePlanSetup.DiveModelSelector.IsUiVisible = false;
            DivePlanSetup.DiveModelSelector.IsUiEnabled = false;
            DivePlanSetup.DiveModelSelector.IsReadOnlyUiVisible = true;
        }

        private DiveParametersResultViewModel UpdateUsedParameters()
        {
            return new DiveParametersResultModelConverter().ConvertToViewModel(_diveProfileService.UpdateParametersUsed(DivePlanSetup.DiveStep, DivePlanSetup.GasMixture.SelectedGasMixture, DivePlanSetup.GasManagement));
        }
    }
}