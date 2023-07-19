using BubblesDivePlanner.ViewModels.Model.Planner.Plan.Stage;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Planner.Plan.Stage
{
    public class DiveStep : ViewModelBase, IDiveStep
    {
        private byte depth;
        public byte Depth
        {
            get => depth;
            set => this.RaiseAndSetIfChanged(ref depth, value);
        }

        private byte time;
        public byte Time
        {
            get => time;
            set => this.RaiseAndSetIfChanged(ref time, value);
        }
    }
}