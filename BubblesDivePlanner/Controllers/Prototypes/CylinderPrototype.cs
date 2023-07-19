using BubblesDivePlanner.ViewModels.Model.Planner.Cylinders;
using BubblesDivePlanner.ViewModels.Planner.Cylinders;

namespace BubblesDivePlanner.Controllers.Prototypes
{
    public static class CylinderPrototype
    {
        public static ICylinder DeepClone(ICylinder cylinder)
        {
            return new Cylinder()
            {
                Name = cylinder.Name,
                Volume = cylinder.Volume,
                Pressure = cylinder.Pressure,
                GasMixture = new GasMixture()
                {
                    Oxygen = cylinder.GasMixture.Oxygen,
                    Helium = cylinder.GasMixture.Helium,
                },
                GasUsage = new GasUsage
                {
                    GasRemaining = cylinder.GasUsage.GasRemaining,
                    GasUsed = cylinder.GasUsage.GasUsed,
                    SurfaceAirConsumptionRate = cylinder.GasUsage.SurfaceAirConsumptionRate
                }
            };
        }
    }
}