using System;
using System.Reactive;
using BubblesDivePlanner.CentralNervousSystemToxicity;
using BubblesDivePlanner.Cylinders.CylinderSelector;
using BubblesDivePlanner.DecompressionProfile;
using BubblesDivePlanner.DiveInformation;
using BubblesDivePlanner.DiveModels.Selector;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Header;
using BubblesDivePlanner.Results;
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
            CylinderSelector.SelectedCylinderChanged += (sender, e) => RecalculateDecompressionSteps();
        }

        public IHeaderModel HeaderModel { get; }

        private IDiveModelSelectorModel _diveModelSelector = new DiveModelSelectorViewModel();
        public IDiveModelSelectorModel DiveModelSelector
        {
            get => _diveModelSelector;
            set => this.RaiseAndSetIfChanged(ref _diveModelSelector, value);
        }

        private IDiveStepModel _diveStep = new DiveStepViewModel();
        public IDiveStepModel DiveStep
        {
            get => _diveStep;
            set => this.RaiseAndSetIfChanged(ref _diveStep, value);
        }

        private ICylinderSelectorModel _cylinderSelector = new CylinderSelectorViewModel();
        public ICylinderSelectorModel CylinderSelector
        {
            get => _cylinderSelector;
            set => this.RaiseAndSetIfChanged(ref _cylinderSelector, value);
        }

        private IDiveInformationModel _diveInformation = new DiveInformationViewModel();
        public IDiveInformationModel DiveInformation
        {
            get => _diveInformation;
            set => this.RaiseAndSetIfChanged(ref _diveInformation, value);
        }

        private IResultsOverviewModel _resultsOverviewModel = new ResultsOverviewViewModel();
        public IResultsOverviewModel ResultsOverviewModel
        {
            get => _resultsOverviewModel;
            set => this.RaiseAndSetIfChanged(ref _resultsOverviewModel, value);
        }

        public ReactiveCommand<Unit, Unit> CalculateDiveStepCommand { get; }
        public IObservable<bool> CanCalculateDiveStep
        {
            get => this.WhenAnyValue(vm => vm.DiveModelSelector.SelectedDiveModel,
                vm => vm.CylinderSelector.SelectedCylinder,
                vm => vm.DiveStep,
                (selectorDiveModel, selectorCylinder, diveStep) =>
                    DiveModelSelector.ValidateSelectedDiveModel(selectorDiveModel)
                    && CylinderSelector.ValidateSelectedCylinder(selectorCylinder)
                    && DiveStep.ValidateDiveStep(diveStep));
        }

        public ReactiveCommand<Unit, Unit> CalculateDecompressionProfileCommand { get; }

        private void CalculateDiveStep()
        {
            new DivePlannerService().CalculateDiveStep(this);
        }

        private void RecalculateDecompressionSteps()
        {
            new DivePlannerService().RecalculateDecompressionSteps(this);
        }

        private void CalculateDecompressionProfile()
        {
            new DivePlannerService().CalculateDecompressionProfile(this);
        }
    }
}