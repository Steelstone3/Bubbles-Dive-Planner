using BubblesDivePlanner.Cylinders.CylinderSetup;

namespace BubblesDivePlannerTests.Cylinders.CylinderSetup
{
    public interface ICylinderPrototype
    {
        ICylinderSetupModel Clone(ICylinderSetupModel cylinderSetupModel);
    }
}