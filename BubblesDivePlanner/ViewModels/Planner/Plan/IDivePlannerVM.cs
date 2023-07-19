using System.Reactive;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Planner.Plan
{
    public interface IDivePlannerVM : IDivePlanner
    {
        ReactiveCommand<Unit, Unit> CalculateDiveProfileCommand { get; }
    }
}