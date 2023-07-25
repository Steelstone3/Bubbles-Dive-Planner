using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Model.Planner.Cylinders;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan.Stage;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Planner.Plan.Stage
{
    public class DiveStage : ViewModelBase, IDiveStage
    {
        private IDiveModel diveModel;
        public IDiveModel DiveModel
        {
            get => diveModel;
            set
            {
                diveModel = value;
                this.RaisePropertyChanged(nameof(DiveModel));
            }
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