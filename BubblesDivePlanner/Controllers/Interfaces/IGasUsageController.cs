using BubblesDivePlanner.DiveStep;

namespace BubblesDivePlanner.Controllers.Interfaces
{
    public interface IGasUsageController
    {
        ushort CalculateInitialPressurisedCylinderVolume(byte cylinderVolume, ushort cylinderPressure);
        ushort CalculateRemainingPressurisedCylinderVolume(ushort gasRemaining, ushort gasUsed);
        ushort CalculateGasUsed(IDiveStepModel diveStepModel, byte surfaceAirConsumptionRate);
    }
}