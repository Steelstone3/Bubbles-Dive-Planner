using BubblesDivePlanner.Models;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.DivePlan
{
    public class DivePlanViewModel : ReactiveObject
    {
        private IDivePlan divePlan;
        public IDivePlan DivePlan
        {
            get => divePlan;
            set => this.RaiseAndSetIfChanged(ref divePlan, value);
        }
    }
}