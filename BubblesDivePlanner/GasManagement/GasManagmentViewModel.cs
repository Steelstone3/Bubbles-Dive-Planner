using System.Collections.ObjectModel;
using BubblesDivePlanner.GasManagement.Cylinder;

namespace BubblesDivePlanner.GasManagement
{
    public class GasManagementViewModel : IGasManagementModel
    {
        public ObservableCollection<ICylinderModel> Cylinders
        {
            get;
        } = new ObservableCollection<ICylinderModel>();
    }
}