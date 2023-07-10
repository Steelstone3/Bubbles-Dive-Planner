using BubblesDivePlanner.ViewModels.Model.Plan;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Plan
{
    public class Planner : ReactiveObject, IPlanner
    {
        private ICylinderSelection cylinderSelection = new CylinderSelection();
        public ICylinderSelection CylinderSelection
        {
            get => cylinderSelection;
            set => this.RaiseAndSetIfChanged(ref cylinderSelection, value);
        }

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