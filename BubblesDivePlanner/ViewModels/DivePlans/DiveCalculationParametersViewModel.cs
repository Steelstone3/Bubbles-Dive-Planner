using BubblesDivePlanner.Controllers.Cylinders;
using BubblesDivePlanner.ViewModels.DiveStages;
using BubblesDivePlanner.ViewModels.Models;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.DivePlans
{
    public class DiveCalculationParametersViewModel : DivePlannerSelectorViewModel, IDiveCalculationParametersModel
    {
        private IDiveStepModel _diveStep = new DiveStepViewModel();
        public IDiveStepModel DiveStep
        {
            get => _diveStep;
            set => this.RaiseAndSetIfChanged(ref _diveStep, value);
        }

        public IDiveModel DiveModel => DiveModelSelector.SelectedDiveModel.DeepClone();

        public ICylinderSetupModel SelectedCylinder => new CylinderPrototype().DeepClone(CylinderSelector.SelectedCylinder);
    }
}