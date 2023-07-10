using BubblesDivePlanner.ViewModels.Models;
using BubblesDivePlanner.ViewModels.Models.Headers;
using BubblesDivePlanner.ViewModels.Models.Informations;
using BubblesDivePlanner.ViewModels.Models.Plan;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels
{
    public class MainWindow : ViewModelBase, IMainWindow
    {
        public MainWindow(IHeader header, IPlanner divePlan, IDiveInformation diveInformation)
        {
            Header = header;
            Planner = divePlan;
            Information = diveInformation;
        }

        private IHeader header;
        public IHeader Header
        {
            get => header;
            set => this.RaiseAndSetIfChanged(ref header, value);
        }

        private IPlanner planner;
        public IPlanner Planner
        {
            get => planner;
            set => this.RaiseAndSetIfChanged(ref planner, value);
        }

        private IDiveInformation information;
        public IDiveInformation Information
        {
            get => information;
            set => this.RaiseAndSetIfChanged(ref information, value);
        }
    }
}