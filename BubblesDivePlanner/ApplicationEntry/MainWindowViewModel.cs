using ReactiveUI;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.DiveModels.Selector;
using BubblesDivePlanner.Cylinders.CylinderSelector;
using System;
using System.Reactive;

namespace BubblesDivePlanner.ApplicationEntry
{
    public class MainWindowViewModel : ReactiveObject
    {
        public MainWindowViewModel()
        {
           CalculateDiveStepCommand = ReactiveCommand.Create(CalculateDiveStep);
        }

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

        public ReactiveCommand<Unit, Unit> CalculateDiveStepCommand { get; }

        //TODO AH Put here a check for CanExecuteDiveStep isDiveModel == Null isSelectedCylinder == Null and DiveStep.Depth is between 0 and 100 with Time being between 0 and 60

        private void CalculateDiveStep()
        {
            //TODO AH Put in here the calculation new DiveStageCommandFactory (withing) → DiveStageRunner.RunDiveStages
            //Then return the result into a result view model (which will need better naming than the original)
        }
    }
}
