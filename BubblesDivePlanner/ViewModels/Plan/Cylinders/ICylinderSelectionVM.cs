using System.Reactive;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Plan.Cylinders
{
    public interface ICylinderSelectionVM : Model.Plan.Cylinders.ICylinderSelection
    {
        ReactiveCommand<Unit, Unit> AddCylinderCommand { get; }
    }
}