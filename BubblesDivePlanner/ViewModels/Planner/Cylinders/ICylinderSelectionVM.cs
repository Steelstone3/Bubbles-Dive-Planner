using System.Reactive;
using BubblesDivePlanner.ViewModels.Model.Planner.Cylinders;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Planner.Cylinders
{
    public interface ICylinderSelectionVM : ICylinderSelection
    {
        ReactiveCommand<Unit, Unit> AddCylinderCommand { get; }
    }
}