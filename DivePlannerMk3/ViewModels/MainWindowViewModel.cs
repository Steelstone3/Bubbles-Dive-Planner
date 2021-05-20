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
        //TODO AH Test drive and Move NewCommand, SaveCommand, OpenCommand to there own classes under the header folder
        
        public MainWindowViewModel()
        {
            var diveProfileService = new DiveProfileService();
            _divePlan = new DivePlanViewModel(diveProfileService);
            _diveInfo = new DiveInfoViewModel(diveProfileService);
            _diveHeader.File = new FileViewModel(this);

            CalculateDiveStepCommand = ReactiveCommand.Create(RunDiveStep, CanExecuteDiveStep); // create command
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

        private DiveInfoViewModel _diveInfo;
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

        private void RunDiveStep()
        {
            DivePlan.CalculateDiveStep(DiveResults);
            DiveResults.DiveParametersResult = DivePlan.UpdateUsedParameters(DiveResults.DiveParametersResult);
            //TODO AH Likely this linq statement is passing in all steps refactor to take 
            DiveInfo.CalculateDiveStep();
        }
    }
}
