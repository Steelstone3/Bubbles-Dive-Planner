using System.Collections.ObjectModel;

namespace BubblesDivePlanner.ViewModels.Model.Planner.Cylinders
{
    public interface ICylinderSelection
    {
        ObservableCollection<ICylinder> Cylinders { get; }
        ICylinder Cylinder { get; set; }
    }
}