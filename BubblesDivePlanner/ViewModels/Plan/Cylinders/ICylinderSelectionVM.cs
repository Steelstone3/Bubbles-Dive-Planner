using System.Reactive;
using BubblesDivePlanner.ViewModels.Model.Plan.Cylinders;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Plan.Cylinders
{
    public interface ICylinderSelectionVM : ICylinderSelection
    {
        ReactiveCommand<Unit, Unit> AddCylinderCommand { get; }
    }
}