using BubblesDivePlanner.DiveStep;

namespace BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage
{
    public class GasUsageController : IGasUsageController
    {
        public int CalculateInitialPressurisedCylinderVolume(int cylinderVolume, int cylinderPressure)
        {
            return cylinderPressure * cylinderVolume;
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

        private int CalculateRemainingPressurisedCylinderVolume(int gasRemaining, int gasUsed)
        {
            return gasRemaining - gasUsed;
        }

        private int CalculateGasUsed(IDiveStepModel diveStepModel, int surfaceAirConsumptionRate)
        {
            return (diveStepModel.Depth / 10 + 1) * diveStepModel.Time * surfaceAirConsumptionRate;
        }
    }
}