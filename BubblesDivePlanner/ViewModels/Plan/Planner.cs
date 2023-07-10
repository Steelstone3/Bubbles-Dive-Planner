using BubblesDivePlanner.ViewModels.Models.Plan;
using BubblesDivePlanner.ViewModels.Models.Plans;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Plan
{
    public class Planner : ReactiveObject, IPlanner
    {
        private IDiveStep diveStep = new DiveStep();
        public IDiveStep DiveStep
        {
            get => diveStep;
            set => this.RaiseAndSetIfChanged(ref diveStep, value);
        }

        private ICylinder cylinder = new Cylinder();
        public ICylinder Cylinder
        {
            get => cylinder;
            set => this.RaiseAndSetIfChanged(ref cylinder, value);
        }
    }
}