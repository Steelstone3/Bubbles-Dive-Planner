using BubblesDivePlanner.Cylinders.CylinderSelector;
using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.DiveCalculationParameters;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveModels.Selector;
using BubblesDivePlanner.DiveStep;
using ReactiveUI;

namespace BubblesDivePlanner.DivePlanner
{
    public class DivePlannerViewModel : ReactiveObject, IDiveParameterSelectorModel, IDiveCalculationParametersModel
    {
        private IDiveModelSelectorModel _diveModelSelector = new DiveModelSelectorViewModel();
        public IDiveModelSelectorModel DiveModelSelector 
        { 
            get => _diveModelSelector; 
            set => this.RaiseAndSetIfChanged(ref _diveModelSelector, value); 
        }

        private ICylinderSelectorModel _cylinderSelector = new CylinderSelectorViewModel();
        public ICylinderSelectorModel CylinderSelector 
        { 
            get => _cylinderSelector; 
            set => this.RaiseAndSetIfChanged(ref _cylinderSelector, value); 
        }

        private IDiveStepModel _diveStep = new DiveStepViewModel();
        public IDiveStepModel DiveStep
        {
            get => _diveStep;
            set => this.RaiseAndSetIfChanged(ref _diveStep, value);
        }

        public IDiveModel DiveModel => DiveModelSelector.SelectedDiveModel;

        public ICylinderSetupModel SelectedCylinder => CylinderSelector.SelectedCylinder;

        public IDiveCalculationParametersModel DeepClone()
        {
            throw new System.NotImplementedException();
        }
    }
}