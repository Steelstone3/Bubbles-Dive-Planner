using BubblesDivePlanner.DiveStep;

namespace BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage
{
    public interface IGasUsageController
    {
        int CalculateInitialPressurisedCylinderVolume(int cylinderVolume, int cylinderPressure);
        IGasUsageModel UpdateGasUsage(IDiveStepModel diveStepModel, IGasUsageModel gasUsage);
    }
}