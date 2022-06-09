using System;
using System.Collections.ObjectModel;
using System.Reactive;
using BubblesDivePlanner.Cylinders.CylinderSetup;
using ReactiveUI;

namespace BubblesDivePlanner.Cylinders.CylinderSelector
{
    public interface ICylinderSelectorModel
    {
        event EventHandler SelectedCylinderChanged;
        ObservableCollection<ICylinderSetupModel> Cylinders { get; }
        ICylinderSetupModel SelectedCylinder { get; set; }
        ReactiveCommand<Unit, Unit> AddCylinderCommand { get; }
        bool ValidateSelectedCylinder(ICylinderSetupModel selectorCylinder);
    }
}