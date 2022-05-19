namespace BubblesDivePlanner.Cylinders.CylinderSetup
{
    public interface ICylinderPrototype
    {
        ICylinderSetupModel Clone(ICylinderSetupModel cylinderSetupModel);
    }
}