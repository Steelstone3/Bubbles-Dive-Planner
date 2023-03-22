using BubblesDivePlanner.Controllers.Cylinders;
using BubblesDivePlanner.ViewModels.DiveStages;
using BubblesDivePlanner.ViewModels.Models;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.DivePlans
{
    //TODO AH Rename to DivePlanViewModel and reuse for dive results (deep clone)
    public class DivePlanViewModel : DivePlanSelectorViewModel, IDivePlanModel
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