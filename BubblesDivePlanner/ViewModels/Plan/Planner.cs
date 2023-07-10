using BubblesDivePlanner.ViewModels.Models.DivePlan;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Plan
{
    public class Planner : ReactiveObject, IPlanner
    {
        // private IDiveModel selectedDiveModel;
        // public IDiveModel SelectedDiveModel
        // {
        //     get => selectedDiveModel;
        //     set => this.RaiseAndSetIfChanged(ref selectedDiveModel, value);
        // }

        // private ICylinder selectedCylinder;
        // public ICylinder SelectedCylinder
        // {
        //     get => selectedCylinder;
        //     set => this.RaiseAndSetIfChanged(ref selectedCylinder, value);
        // }

        private IDiveStep diveStep = new DiveStep();
        public IDiveStep DiveStep
        {
            get => diveStep;
            set => this.RaiseAndSetIfChanged(ref diveStep, value);
        }
    }
}