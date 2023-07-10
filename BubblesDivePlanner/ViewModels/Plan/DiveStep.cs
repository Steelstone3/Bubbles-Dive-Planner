using BubblesDivePlanner.ViewModels.Models.Plans;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Plan
{
    public class DiveStep : ReactiveObject, IDiveStep
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