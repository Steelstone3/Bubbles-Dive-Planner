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
            DivePlan.CylinderSelector.SelectedCylinderChanged += (sender, e) => RecalculateDecompressionSteps();
        }

        public IHeaderModel HeaderModel { get; }

        private DivePlanViewModel _divePlan = new();
        public DivePlanViewModel DivePlan
        {
            get => _divePlan;
            set => this.RaiseAndSetIfChanged(ref _divePlan, value);
        }

        private IDiveInformationModel _diveInformation = new DiveInformationViewModel();
        public IDiveInformationModel DiveInformation
        {
            get => _diveInformation;
            set => this.RaiseAndSetIfChanged(ref _diveInformation, value);
        }

        private IResultsModel _resultsModel = new ResultsViewModel();
        public IResultsModel Results
        {
            get => _resultsModel;
            set => this.RaiseAndSetIfChanged(ref _resultsModel, value);
        }

        public ReactiveCommand<Unit, Unit> CalculateDiveStepCommand { get; }
        public IObservable<bool> CanCalculateDiveStep
        {
            get => this.WhenAnyValue(vm => vm.DivePlan.DiveModelSelector.SelectedDiveModel,
                vm => vm.DivePlan.CylinderSelector.SelectedCylinder,
                vm => vm.DivePlan.DiveStep,
                (selectorDiveModel, selectorCylinder, diveStep) =>
                    DivePlan.DiveModelSelector.ValidateSelectedDiveModel(selectorDiveModel)
                    && DivePlan.CylinderSelector.ValidateSelectedCylinder(selectorCylinder)
                    && DivePlan.DiveStep.ValidateDiveStep(diveStep));
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