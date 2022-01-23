using System.Collections.ObjectModel;
using BubblesDivePlanner.GasManagement.Cylinder;

namespace BubblesDivePlanner.GasManagement
{
    public interface IGasManagementModel
    {
        ObservableCollection<ICylinderModel> Cylinders { get; }
    }
}