using ReactiveUI;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.DiveModels.Selector;
using BubblesDivePlanner.Cylinders.CylinderSelector;
using BubblesDivePlanner.Cylinders.CylinderSetup;
using System;
using System.Reactive;
using BubblesDivePlanner.Visibility;
using BubblesDivePlanner.Results;
using BubblesDivePlanner.DiveStages.Runner;
using BubblesDivePlanner.Header;
using BubblesDivePlanner.CentralNervousSystemToxicity;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage;
using BubblesDivePlanner.DecompressionProfile;

namespace BubblesDivePlanner.ApplicationEntry
{
    public class MainWindowViewModel : ReactiveObject, IMainWindowModel
    {
        public MainWindowViewModel()
        {
            CalculateDiveStepCommand = ReactiveCommand.Create(CalculateDiveStep, CanCalculateDiveStep);
            HeaderModel = new HeaderViewModel(this);
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

        public ICentralNervousSystemToxicityModel CentralNervousSystemToxicity
        {
            get;
        } = new CentralNervousSystemToxicityViewModel();

        public IDecompressionProfileModel DecompressionProfile => new DecompressionProfileViewModel(DiveModelSelector.SelectedDiveModel, CylinderSelector.SelectedCylinder);

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

        private void CalculateDiveStep()
        {
            new VisibilityController().UpdateVisibilty(this);
            new DiveStageRunner().RunDiveStages(DiveModelSelector.SelectedDiveModel, DiveStep, CylinderSelector.SelectedCylinder);
            ResultsOverviewModel.LatestResult.DiveProfileModel = DiveModelSelector.SelectedDiveModel.DiveProfile.DeepClone();
            ResultsOverviewModel.LatestResult.DiveStepModel = DiveStep.DeepClone();
            CylinderSelector.SelectedCylinder.GasUsage.GasUsed = new GasUsageController().CalculateGasUsed(DiveStep, CylinderSelector.SelectedCylinder.GasUsage.SurfaceAirConsumptionRate);
            CylinderSelector.SelectedCylinder.GasUsage.UpdateGasRemaining();
            ResultsOverviewModel.LatestResult.CylinderSetupModel = new CylinderPrototype().DeepClone(CylinderSelector.SelectedCylinder);
        }
    }
}