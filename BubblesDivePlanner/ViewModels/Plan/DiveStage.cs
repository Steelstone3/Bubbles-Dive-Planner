using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Model.Plan.Cylinders;
using BubblesDivePlanner.ViewModels.Model.Plan.DiveModels;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Plan
{
    public class DiveStage : ViewModelBase, IDiveStage
    {
        private IDiveModel diveModel;
        public IDiveModel DiveModel
        {
            get => diveModel;
            set => this.RaiseAndSetIfChanged(ref diveModel, value);
        }

        private IDiveStep diveStep = new DiveStep();
        public IDiveStep DiveStep
        {
            get => diveStep;
            set => this.RaiseAndSetIfChanged(ref diveStep, value);
        }

        private ICylinder cylinder;
        public ICylinder Cylinder
        {
            get => cylinder;
            set => this.RaiseAndSetIfChanged(ref cylinder, value);
        }
    }
}