
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlanner.ViewModels.Model.Planner.Setup;
using BubblesDivePlanner.ViewModels.Planner.Cylinders;
using BubblesDivePlanner.ViewModels.Planner.DiveModels;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Planner.Plan.Setup
{
    public class DiveSetup : ViewModelBase, IDiveSetup
    {
        public IDiveModelSelection diveModelSelection = new DiveModelSelection();
        public IDiveModelSelection DiveModelSelection
        {
            get => diveModelSelection;
            set => this.RaiseAndSetIfChanged(ref diveModelSelection, value);
        }

        private ICylinderSelectionVM cylinderSelection = new CylinderSelection();
        public ICylinderSelectionVM CylinderSelection
        {
            get => cylinderSelection;
            set => this.RaiseAndSetIfChanged(ref cylinderSelection, value);
        }
    }
}