using BubblesDivePlanner.Models;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.DivePlan
{
    public class DiveStepViewModel : ReactiveObject
    {
        private IDiveStep diveStep = new DiveStep(0, 0);
        public IDiveStep DiveStep
        {
            get => diveStep;
            set => this.RaiseAndSetIfChanged(ref diveStep, value);
        }
    }
}