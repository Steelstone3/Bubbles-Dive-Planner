using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
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
        public MainWindowViewModel()
        {
            _divePlan = new DivePlanViewModel(new DiveProfileService());

            CalculateDiveStepCommand = ReactiveCommand.Create(RunDiveStep, CanExecuteDiveStep); // create command
            NewCommand = ReactiveCommand.Create(CreateNewDiveSession);
            SaveCommand = ReactiveCommand.Create(SaveDivePlannerState);
            OpenCommand = ReactiveCommand.Create(LoadDivePlannerState);
        }

        private DiveResultsViewModel _diveResults = new DiveResultsViewModel();
        public DiveResultsViewModel DiveResults
        {
            get => _diveResults;
            set => this.RaiseAndSetIfChanged(ref _diveResults, value);
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

        //Must make observables atomic e.g vm => vm.DivePlan.DiveModelSelector.SelectedDiveModel
        public IObservable<bool> CanExecuteDiveStep
        {
            get => this.WhenAnyValue(vm => vm.DivePlan.DiveModelSelector.SelectedDiveModel,
            vm => vm.DivePlan.GasMixture.SelectedGasMixture,
            vm => vm.DivePlan.GasManagement.CylinderPressure,
            vm => vm.DivePlan.GasManagement.CylinderVolume,
            vm => vm.DivePlan.GasManagement.SacRate,
            vm => vm.DivePlan.DiveStep.Depth,
            vm => vm.DivePlan.DiveStep.Time,
            vm => vm.DivePlan.GasMixture.MaximumOperatingDepth,
            (selectorDiveModel, selectedGasMixture, cylinderPressure, cylinderVolume, sacRate, depth, time, maximumOperatingDepth) =>
            DivePlan.DiveModelSelector.ValidateSelectedDiveModel(selectorDiveModel)
            && DivePlan.GasMixture.ValidateGasMixture(selectedGasMixture)
            && DivePlan.GasManagement.ValidateGasManagement(cylinderVolume, cylinderPressure, sacRate)
            && DivePlan.DiveStep.ValidateDiveStep(depth, time, maximumOperatingDepth));
        }

        public ReactiveCommand<Unit, Unit> NewCommand
        {
            get;
        }

        public ReactiveCommand<Unit, Unit> SaveCommand
        {
            get;
        }

        public ReactiveCommand<Unit, Unit> OpenCommand
        {
            get;
        }

        private void RunDiveStep()
        {
            DivePlan.CalculateDiveStep(DiveResults);
            DiveResults.DiveParametersResult = DivePlan.UpdateUsedParameters(DiveResults.DiveParametersResult);
            DiveInfo.CalculateDiveStep(DiveResults.DiveProfileResults.SelectMany(diveModel => diveModel.DiveProfileStepOutput.Select(x => x.ToleratedAmbientPressureResult)));
        }

        private void CreateNewDiveSession()
        {
            DiveResults = new DiveResultsViewModel();
            DivePlan = new DivePlanViewModel(new DiveProfileService());
            DiveInfo = new DiveInfoViewModel();
            DiveHeader = new DiveHeaderViewModel();
        }

        //TODO AH this area needs a changable save name investigate Directory property of the SaveDialog
        private void SaveDivePlannerState()
        {
            var saveApplicationState = new SaveApplicationStateController();
            saveApplicationState.SaveApplication(this);
        }

        //TODO AH this area needs work
        private void LoadDivePlannerState()
        {
            var loadApplicationState = new LoadApplicationStateController();
            loadApplicationState.LoadApplication(this);
        }
    }
}
