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

        public MainWindowViewModel()
        {
            _diveProfileController = new DiveProfileService();
            _divePlan = new DivePlanViewModel(_diveProfileController);

            CalculateDiveStepCommand = ReactiveCommand.Create(RunDiveStep, CanExecuteDiveStep); // create command
        }

        private DiveResultsViewModel _diveStepProfile = new DiveResultsViewModel();
        public DiveResultsViewModel DiveStepProfiles
        {
            get => _diveStepProfile;
            set => this.RaiseAndSetIfChanged(ref _diveStepProfile, value);
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
            DisableUiElements();
            
            var result = _diveProfileController.RunDiveStep(DivePlan.DiveStep, DivePlan.GasMixture);

            DiveStepProfiles.DiveParametersUsed = (result.DiveParametersOutput);
            DiveStepProfiles.DiveProfileResults.Add(result);
        }

        private void DisableUiElements()
        {
            DivePlan.DiveModelSelector.UiEnabled = false;
            DivePlan.GasManagement.UiEnabled = false;
        }
    }
}
