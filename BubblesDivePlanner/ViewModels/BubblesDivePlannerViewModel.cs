using BubblesDivePlanner.Models;
using BubblesDivePlanner.ViewModels.DivePlan;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels
{
    public class BubblesDivePlannerViewModel : ReactiveObject, IBubblesDivePlanner
    {
         private DivePlanViewModel divePlan = new();
        public DivePlanViewModel DivePlan
        {
            get => divePlan;
            set => this.RaiseAndSetIfChanged(ref divePlan, value);
        }
    }
}