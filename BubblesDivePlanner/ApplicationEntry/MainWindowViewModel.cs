using System;
using System.Reactive;
using BubblesDivePlanner.DiveInformation;
using BubblesDivePlanner.DivePlanner;
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
            DivePlanner.CylinderSelector.SelectedCylinderChanged += (sender, e) => RecalculateDecompressionSteps();
        }

        public IHeaderModel HeaderModel { get; }

        private DivePlannerViewModel _divePlanner = new DivePlannerViewModel();
        public DivePlannerViewModel DivePlanner
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