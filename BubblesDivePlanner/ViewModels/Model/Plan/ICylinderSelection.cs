using System.Collections.ObjectModel;

namespace BubblesDivePlanner.ViewModels.Model.Plan
{
    public interface ICylinderSelection
    {
        ObservableCollection<ICylinder> Cylinders { get; }
    }
}