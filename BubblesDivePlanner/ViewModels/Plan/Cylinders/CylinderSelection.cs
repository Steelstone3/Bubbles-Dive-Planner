using System.Collections.ObjectModel;
using BubblesDivePlanner.ViewModels.Model.Plan.Cylinders;

namespace BubblesDivePlanner.ViewModels.Plan.Cylinders
{
    public class CylinderSelection : ICylinderSelection
    {
        public ObservableCollection<ICylinder> Cylinders
        {
            get;
        } = new ObservableCollection<ICylinder>();
    }
}