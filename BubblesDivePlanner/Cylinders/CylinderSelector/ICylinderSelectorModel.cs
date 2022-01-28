using System.Collections.ObjectModel;
using BubblesDivePlanner.Cylinders.CylinderSetup;

namespace BubblesDivePlanner.Cylinders.CylinderSelector
{
    public interface ICylinderSelectorModel
    {
        ObservableCollection<ICylinderSetupModel> Cylinders { get; }
    }
}