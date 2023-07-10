using System.Collections.ObjectModel;
using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Models.Plan;

namespace BubblesDivePlanner.ViewModels.Plan
{
    public class CylinderSelection : ICylinderSelection
    {
        public ObservableCollection<ICylinder> Cylinders
        {
            get;
        } = new ObservableCollection<ICylinder>();
    }
}