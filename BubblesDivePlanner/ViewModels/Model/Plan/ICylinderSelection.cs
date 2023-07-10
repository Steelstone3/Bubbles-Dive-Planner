using System.Collections.ObjectModel;
using BubblesDivePlanner.ViewModels.Models.Plan;

namespace BubblesDivePlanner.ViewModels.Model.Plan
{
    public interface ICylinderSelection
    {
        ObservableCollection<ICylinder> Cylinders { get; }
    }
}