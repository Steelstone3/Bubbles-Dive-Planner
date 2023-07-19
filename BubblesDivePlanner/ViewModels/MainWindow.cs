using BubblesDivePlanner.ViewModels.Headers;
using BubblesDivePlanner.ViewModels.Model;
using BubblesDivePlanner.ViewModels.Model.Headers;
using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Plan;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels
{
    public class MainWindow : ViewModelBase, IMainWindow
    {
        private IHeader header = new Header();
        public IHeader Header
        {
            get => header;
            set => this.RaiseAndSetIfChanged(ref header, value);
        }

        private IPlanner planner = new Planner();
        public IPlanner Planner
        {
            get => planner;
            set => this.RaiseAndSetIfChanged(ref planner, value);
        }
    }
}