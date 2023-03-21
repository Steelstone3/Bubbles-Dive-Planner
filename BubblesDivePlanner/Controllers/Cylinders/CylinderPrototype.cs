using BubblesDivePlanner.Controllers.Interfaces;
using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage;
using BubblesDivePlanner.ViewModels.Models;

namespace BubblesDivePlanner.Controllers.Cylinders
{
    public class CylinderPrototype : ICylinderPrototype
    {
        public ICylinderSetupModel DeepClone(ICylinderSetupModel cylinderSetupModel)
        {
            return new CylinderSetupViewModel()
            {
                CylinderName = cylinderSetupModel.CylinderName.Clone() as string,
                CylinderVolume = cylinderSetupModel.CylinderVolume,
                CylinderPressure = cylinderSetupModel.CylinderPressure,
                InitialPressurisedCylinderVolume = cylinderSetupModel.InitialPressurisedCylinderVolume,
                GasMixture = CloneGasMixture(cylinderSetupModel),
                GasUsage = CloneGasUsage(cylinderSetupModel),
            };
        }

        private static IGasMixtureModel CloneGasMixture(ICylinderSetupModel cylinderSetupModel)
        {
            return new GasMixtureViewModel()
            {
                Oxygen = cylinderSetupModel.GasMixture.Oxygen,
                Helium = cylinderSetupModel.GasMixture.Helium,
            };
        }

        private static IGasUsageModel CloneGasUsage(ICylinderSetupModel cylinderSetupModel)
        {
            return new GasUsageViewModel()
            {
                GasRemaining = cylinderSetupModel.GasUsage.GasRemaining,
                GasUsed = cylinderSetupModel.GasUsage.GasUsed,
                SurfaceAirConsumptionRate = cylinderSetupModel.GasUsage.SurfaceAirConsumptionRate,
            };
        }
    }
}