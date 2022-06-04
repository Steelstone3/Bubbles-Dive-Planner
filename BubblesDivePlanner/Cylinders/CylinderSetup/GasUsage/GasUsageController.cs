using BubblesDivePlanner.DiveStep;

namespace BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage
{
    public class GasUsageController : IGasUsageController
    {
        public ushort CalculateInitialPressurisedCylinderVolume(byte cylinderVolume, ushort cylinderPressure)
        {
            return (ushort)(cylinderPressure * cylinderVolume);
        }

        public ushort CalculateRemainingPressurisedCylinderVolume(ushort gasRemaining, ushort gasUsed)
        {
            return (ushort)(gasRemaining - gasUsed);
        }

        public ushort CalculateGasUsed(IDiveStepModel diveStepModel, byte surfaceAirConsumptionRate)
        {
            return (ushort)((diveStepModel.Depth / 10 + 1) * diveStepModel.Time * surfaceAirConsumptionRate);
        }
    }
}