using BubblesDivePlanner.Models;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.DivePlan
{
    public class DiveStepViewModel : ReactiveObject, IDiveStep
    {
        private byte depth = 0;
        public byte Depth
        {
            get => depth;
            set => this.RaiseAndSetIfChanged(ref depth, value);
        }

        private byte time = 0;
        public byte Time
        {
            get => depth;
            set => this.RaiseAndSetIfChanged(ref time, value);
        }
    }
}