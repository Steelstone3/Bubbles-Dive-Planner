using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan.Information;
using BubblesDivePlanner.ViewModels.Model.Planner.Setup;
using BubblesDivePlanner.ViewModels.Planner.Cylinders;
using BubblesDivePlanner.ViewModels.Planner.DiveModels;
using BubblesDivePlanner.ViewModels.Planner.Plan.Information;
using BubblesDivePlanner.ViewModels.Planner.Plan.Setup;
using BubblesDivePlanner.ViewModels.Planner.Plan.Stage;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Planner.Plan
{
    public class DivePlanner : ViewModelBase, IDivePlanner
    {
        // TODO Combine into a dive setup?
        public IDiveSetup diveModelSelection = new DiveSetup();
        public IDiveSetup DiveSetup
        {
            get => diveModelSelection;
            set => this.RaiseAndSetIfChanged(ref diveModelSelection, value);
        }

        // TODO combine into a dive application?
        private IDiveInformation information = new DiveInformation();
        public IDiveInformation Information
        {
            get => information;
            set => this.RaiseAndSetIfChanged(ref information, value);
        }

        private IDiveStage diveStage = new DiveStage();
        public IDiveStage DiveStage
        {
            get => diveStage;
            set => this.RaiseAndSetIfChanged(ref diveStage, value);
        }
    }
}