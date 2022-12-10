using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.DivePlan
{
    public class DivePlanViewModel : ReactiveObject
    {
        private DiveModelViewModel diveModel = new();
        public DiveModelViewModel DiveModel
        {
            get => diveModel;
            set => this.RaiseAndSetIfChanged(ref diveModel, value);
        }

        private DiveStepViewModel diveStep = new();
        public DiveStepViewModel DiveStep
        {
            get => diveStep;
            set => this.RaiseAndSetIfChanged(ref diveStep, value);
        }

        private CylinderViewModel cylinder = new();
        public CylinderViewModel Cylinders
        {
            get => cylinder;
            set => this.RaiseAndSetIfChanged(ref cylinder, value);
        }
    }
}