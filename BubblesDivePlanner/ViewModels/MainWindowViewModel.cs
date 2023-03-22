using System;
using System.Reactive;
using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.DiveInformation;
using BubblesDivePlanner.Header;
using BubblesDivePlanner.ViewModels.DiveInformation;
using BubblesDivePlanner.ViewModels.DivePlans;
using BubblesDivePlanner.ViewModels.Header;
using BubblesDivePlanner.ViewModels.Models;
using ReactiveUI;

namespace BubblesDivePlanner.ApplicationEntry
{
    public class MainWindowViewModel : ReactiveObject, IMainWindowModel
    {
        public MainWindowViewModel()
        {
            CalculateDiveStepCommand = ReactiveCommand.Create(CalculateDiveStep, CanCalculateDiveStep);
            CalculateDecompressionProfileCommand = ReactiveCommand.Create(CalculateDecompressionProfile);
            HeaderModel = new HeaderViewModel(this);
            SubscribeEvents();
        }

        public void SubscribeEvents()
        {
            DivePlanner.CylinderSelector.SelectedCylinderChanged += (sender, e) => RecalculateDecompressionSteps();
        }

        public IHeaderModel HeaderModel { get; }

        private DiveCalculationParametersViewModel _divePlanner = new();
        public DiveCalculationParametersViewModel DivePlanner
        {
            get => _divePlanner;
            set => this.RaiseAndSetIfChanged(ref _divePlanner, value);
        }

        private IDiveInformationModel _diveInformation = new DiveInformationViewModel();
        public IDiveInformationModel DiveInformation
        {
            get => _diveInformation;
            set => this.RaiseAndSetIfChanged(ref _diveInformation, value);
        }

        private IResultsOverviewModel _resultsOverviewModel = new ResultsOverviewViewModel();
        public IResultsOverviewModel ResultsOverview
        {
            get => _resultsOverviewModel;
            set => this.RaiseAndSetIfChanged(ref _resultsOverviewModel, value);
        }

        public ReactiveCommand<Unit, Unit> CalculateDiveStepCommand { get; }
        public IObservable<bool> CanCalculateDiveStep
        {
            get => this.WhenAnyValue(vm => vm.DivePlanner.DiveModelSelector.SelectedDiveModel,
                vm => vm.DivePlanner.CylinderSelector.SelectedCylinder,
                vm => vm.DivePlanner.DiveStep,
                (selectorDiveModel, selectorCylinder, diveStep) =>
                    DivePlanner.DiveModelSelector.ValidateSelectedDiveModel(selectorDiveModel)
                    && DivePlanner.CylinderSelector.ValidateSelectedCylinder(selectorCylinder)
                    && DivePlanner.DiveStep.ValidateDiveStep(diveStep));
        }

        public ReactiveCommand<Unit, Unit> CalculateDecompressionProfileCommand { get; }

        private void CalculateDiveStep()
        {
            DivePlannerService.CalculateDiveStep(this);
        }

        private void CalculateDecompressionProfile()
        {
            DivePlannerService.CalculateDecompressionProfile(this);
        }

        private void RecalculateDecompressionSteps()
        {
            DivePlannerService.RecalculateDecompressionSteps(this);
        }
    }
}