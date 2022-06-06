namespace BubblesDivePlanner.Cylinders.CylinderSetup
{
    public interface ICylinderPrototype
    {
        ICylinderSetupModel DeepClone(ICylinderSetupModel cylinderSetupModel);
    }
}