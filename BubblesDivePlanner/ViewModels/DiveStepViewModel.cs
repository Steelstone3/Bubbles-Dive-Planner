using BubblesDivePlanner.Models;
using ReactiveUI;

namespace BubblesDivePlannerTests.ViewModels
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