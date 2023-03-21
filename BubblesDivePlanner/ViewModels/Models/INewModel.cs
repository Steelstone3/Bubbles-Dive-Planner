using System.Reactive;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Models
{
    public interface INewModel
    {
        ReactiveCommand<Unit, Unit> CreateNewDivePlannerInstanceCommand { get; }
    }
}