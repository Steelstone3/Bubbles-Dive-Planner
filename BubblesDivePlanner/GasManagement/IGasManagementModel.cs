using System.Collections.ObjectModel;
using BubblesDivePlanner.GasManagement.Cylinder;
using DynamicData.Binding;

namespace BubblesDivePlanner.GasManagement
{
    public interface IGasManagmentModel
    {
        ObservableCollection<ICylinderModel> Cylinders { get; }
    }
}