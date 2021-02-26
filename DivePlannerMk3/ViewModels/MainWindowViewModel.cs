using System;
using System.Linq;
using System.Reactive;
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
        }

        private DiveResultsViewModel _diveResults = new DiveResultsViewModel();
        public DiveResultsViewModel DiveResults
        {
            get => _diveResults;
            set => this.RaiseAndSetIfChanged(ref _diveResults, value);
        }

        private DiveParametersResultViewModel _diveParametersResult = new DiveParametersResultViewModel();
        public DiveParametersResultViewModel DiveParametersResult
        {
            get => _diveParametersResult;
            set => this.RaiseAndSetIfChanged(ref _diveParametersResult, value);
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

        public IObservable<bool> CanExecuteDiveStep
        {
            get => this.WhenAnyValue(vm => vm.DivePlan.GasMixture.SelectedGasMixture, vm => vm.DivePlan.DiveModelSelector.SelectedDiveModel, (selectedGasMixture, selectedDiveModel) => selectedGasMixture != null && selectedDiveModel != null);
        }

        private void RunDiveStep()
        {
            DivePlan.CalculateDiveStep(DiveResults, DiveParametersResult);            
            DiveInfo.CalculateDiveStep(DiveResults.DiveProfileResults.SelectMany(diveModel => diveModel.DiveProfileStepOutput.Select(x => x.ToleratedAmbientPressureResult)));
        }
    }
}
