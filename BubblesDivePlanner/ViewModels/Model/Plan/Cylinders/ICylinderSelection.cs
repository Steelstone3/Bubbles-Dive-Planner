using System.Collections.ObjectModel;

namespace BubblesDivePlanner.ViewModels.Model.Plan.Cylinders
{
    public interface ICylinderSelection
    {
        ObservableCollection<ICylinder> Cylinders { get; }
        ICylinder SelectedCylinder{get;set;}
    }
}