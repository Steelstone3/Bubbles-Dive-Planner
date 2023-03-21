using BubblesDivePlanner.ViewModels.Models;

namespace BubblesDivePlanner.Controllers.Interfaces
{
    public interface ICylinderPrototype
    {
        ICylinderSetupModel DeepClone(ICylinderSetupModel cylinderSetupModel);
    }
}