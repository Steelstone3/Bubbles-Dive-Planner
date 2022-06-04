using BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage;

namespace BubblesDivePlanner.Cylinders.CylinderSetup
{
    public class CylinderPrototype : ICylinderPrototype
    {
        public ICylinderSetupModel Clone(ICylinderSetupModel cylinderSetupModel)
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

        private IGasMixtureModel CloneGasMixture(ICylinderSetupModel cylinderSetupModel)
        {
            return new GasMixtureViewModel()
            {
                Oxygen = cylinderSetupModel.GasMixture.Oxygen,
                Helium = cylinderSetupModel.GasMixture.Helium,
            };
        }

        private IGasUsageModel CloneGasUsage(ICylinderSetupModel cylinderSetupModel)
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