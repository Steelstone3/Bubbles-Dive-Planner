using System.Collections.ObjectModel;
using BubblesDivePlanner.GasManagement.Cylinder;
using DynamicData.Binding;

namespace BubblesDivePlanner.GasManagement
{
    public class GasManagementViewModel : IGasManagmentModel
    {
        public ObservableCollection<ICylinderModel> Cylinders
        {
            get;
        } = new ObservableCollection<ICylinderModel>();
    }
}