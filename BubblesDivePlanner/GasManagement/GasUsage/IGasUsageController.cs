using BubblesDivePlanner.DiveStep;

namespace BubblesDivePlanner.GasManagement.GasUsage
{
    public interface IGasUsageController 
    {
        int CalculateInitialPressurisedCylinderVolume(int cylinderVolume, int cylinderPressure);
        IGasUsageModel UpdateGasUsage(IDiveStepModel diveStepModel, IGasUsageModel gasUsage);
    }
}