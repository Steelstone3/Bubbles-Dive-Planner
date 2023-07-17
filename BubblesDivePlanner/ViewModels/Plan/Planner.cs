using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Model.Plan.Cylinders;
using BubblesDivePlanner.ViewModels.Model.Plan.DiveModels;
using BubblesDivePlanner.ViewModels.Model.Plan.Information;
using BubblesDivePlanner.ViewModels.Plan.Cylinders;
using BubblesDivePlanner.ViewModels.Plan.DiveModels;
using BubblesDivePlanner.ViewModels.Plan.Information;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Plan
{
    public class Planner : ViewModelBase, IPlanner
    {
        public IDiveModelSelection diveModelSelection = new DiveModelSelection();
        public IDiveModelSelection DiveModelSelection
        {
            get => diveModelSelection;
            set => this.RaiseAndSetIfChanged(ref diveModelSelection, value);
        }

        private ICylinderSelection cylinderSelection = new CylinderSelection();
        public ICylinderSelection CylinderSelection
        {
            get => cylinderSelection;
            set => this.RaiseAndSetIfChanged(ref cylinderSelection, value);
        }

        private IDiveInformation information = new DiveInformation();
        public IDiveInformation Information
        {
            get => information;
            set => this.RaiseAndSetIfChanged(ref information, value);
        }

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

        private ICylinder cylinder = new Cylinder();
        public ICylinder Cylinder
        {
            get => cylinder;
            set => this.RaiseAndSetIfChanged(ref cylinder, value);
        }
    }
}