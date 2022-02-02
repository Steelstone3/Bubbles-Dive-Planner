using System.Collections.ObjectModel;
using System.Reactive;
using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.Visibility;
using ReactiveUI;

namespace BubblesDivePlanner.Cylinders.CylinderSelector
{
    public interface ICylinderSelectorModel
    {
        ObservableCollection<ICylinderSetupModel> Cylinders { get; }
        ICylinderSetupModel SelectedCylinder { get; set; }
        ReactiveCommand<Unit, Unit> AddCylinderCommand { get; }
    }
}