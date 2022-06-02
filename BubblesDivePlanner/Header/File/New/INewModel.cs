using System.Reactive;
using ReactiveUI;

namespace BubblesDivePlanner.Header.File.New
{
    public interface INewModel {
        ReactiveCommand<Unit, Unit> CreateNewDivePlannerInstanceCommand { get; }
    }
}