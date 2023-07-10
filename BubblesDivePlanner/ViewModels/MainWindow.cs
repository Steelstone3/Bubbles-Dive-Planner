using BubblesDivePlanner.ViewModels.Models;
using BubblesDivePlanner.ViewModels.Models.DiveInformation;
using BubblesDivePlanner.ViewModels.Models.DivePlan;
using BubblesDivePlanner.ViewModels.Models.Header;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels
{
    public class MainWindow : ViewModelBase, IMainWindow
    {
        public MainWindow(IHeader header, IPlanner divePlan, IInformation diveInformation)
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

        private IInformation information;
        public IInformation Information
        {
            get => information;
            set => this.RaiseAndSetIfChanged(ref information, value);
        }
    }
}