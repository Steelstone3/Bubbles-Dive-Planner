using System;
using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Models
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