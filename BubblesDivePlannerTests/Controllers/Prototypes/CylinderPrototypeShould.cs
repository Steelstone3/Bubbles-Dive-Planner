using BubblesDivePlanner.Controllers.Prototypes;
using BubblesDivePlanner.ViewModels.Model.Planner.Cylinders;
using BubblesDivePlanner.ViewModels.Planner.Cylinders;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.Prototypes
{
    public class CylinderPrototypeShould
    {
        [Fact]
        public void DeepClone()
        {
            // Given
            Cylinder cylinder = new()
            {
                Name = "EAN32",
                Volume = 12,
                Pressure = 200,
                GasMixture = new GasMixture()
                {
                    Oxygen = 21,
                    Helium = 10,
                },
                GasUsage = new GasUsage
                {
                    GasRemaining = 1200,
                    GasUsed = 720,
                    SurfaceAirConsumptionRate = 12
                }
            };

            // When
            ICylinder deepClone = CylinderPrototype.DeepClone(cylinder);

            // Then
            Assert.Equivalent(cylinder, deepClone);
            Assert.NotSame(cylinder, deepClone);
        }
    }
}