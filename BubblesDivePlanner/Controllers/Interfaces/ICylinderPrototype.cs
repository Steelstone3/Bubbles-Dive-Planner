using BubblesDivePlanner.Cylinders.CylinderSetup;

namespace BubblesDivePlanner.Controllers.Interfaces
{
    public interface ICylinderPrototype
    {
        ICylinderSetupModel DeepClone(ICylinderSetupModel cylinderSetupModel);
    }
}