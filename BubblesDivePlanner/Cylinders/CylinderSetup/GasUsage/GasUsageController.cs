using BubblesDivePlanner.DiveStep;

namespace BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage
{
    public class GasUsageController : IGasUsageController
    {
        public ushort CalculateInitialPressurisedCylinderVolume(byte cylinderVolume, ushort cylinderPressure)
        {
            return (ushort)(cylinderPressure * cylinderVolume);
        }

        public IGasUsageModel UpdateGasUsage(IDiveStepModel diveStepModel, IGasUsageModel gasUsage)
        {
            var gasUsed = CalculateGasUsed(diveStepModel, gasUsage.SurfaceAirConsumptionRate);
            var gasRemaining = CalculateRemainingPressurisedCylinderVolume(gasUsage.GasRemaining, gasUsed);

            return new GasUsageViewModel()
            {
                GasUsed = gasUsed,
                GasRemaining = gasRemaining,
                InitialPressurisedCylinderVolume = gasUsage.InitialPressurisedCylinderVolume,
                SurfaceAirConsumptionRate = gasUsage.SurfaceAirConsumptionRate,
            };
        }

        private ushort CalculateRemainingPressurisedCylinderVolume(ushort gasRemaining, ushort gasUsed)
        {
            return (ushort)(gasRemaining - gasUsed);
        }

        private ushort CalculateGasUsed(IDiveStepModel diveStepModel, byte surfaceAirConsumptionRate)
        {
            return (ushort)((diveStepModel.Depth / 10 + 1) * diveStepModel.Time * surfaceAirConsumptionRate);
        }
    }
}