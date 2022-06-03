using BubblesDivePlanner.DiveStep;

namespace BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage
{
    public interface IGasUsageController
    {
        ushort CalculateInitialPressurisedCylinderVolume(byte cylinderVolume, ushort cylinderPressure);
        IGasUsageModel UpdateGasUsage(IDiveStepModel diveStepModel, IGasUsageModel gasUsage);
    }
}