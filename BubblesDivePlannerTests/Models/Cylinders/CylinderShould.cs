using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Models.Cylinders
{
    public class CylinderShould
    {
        private readonly string name = "Air";
        private readonly byte cylinderVolume = 12;
        private readonly ushort cylinderPressure = 200;
        private readonly byte surfaceAirConsumptionRate = 12;
        private readonly ushort initialPressurisedCylinderVolume = 2400;
        private readonly ushort remainingGas = 1680;
        private readonly ushort usedGas = 720;
        private readonly Mock<IGasMixture> dummyGasMixture = new();
        private ICylinder cylinder;

        public CylinderShould()
        {
            cylinder = new Cylinder(name, cylinderVolume, cylinderPressure, surfaceAirConsumptionRate, dummyGasMixture.Object, remainingGas, usedGas);
        }

        [Fact]
        public void TestCylinderName()
        {
            Assert.Equal(name, cylinder.Name);
        }

        [Fact]
        public void ContainsCylinderVolume()
        {
            Assert.Equal(cylinderVolume, cylinder.CylinderVolume);
        }

        [Fact]
        public void ContainsCylinderPressure()
        {
            Assert.Equal(cylinderPressure, cylinder.CylinderPressure);
        }

        [Fact]
        public void ContainsGasMixture()
        {
            Assert.NotNull(cylinder.GasMixture);
        }

        [Fact]
        public void ContainsInitialisedPressurisedCylinderVolume()
        {
            Assert.Equal(initialPressurisedCylinderVolume, cylinder.InitialPressurisedVolume);
        }

        [Fact]
        public void ContainsSurfaceAirConsumptionRate()
        {
            Assert.Equal(surfaceAirConsumptionRate, cylinder.SurfaceAirConsumptionRate);
        }

        [Fact]
        public void ContainsRemainingGas()
        {
            Assert.Equal(remainingGas, cylinder.RemainingGas);
        }

        [Fact]
        public void ContainsUsedGas()
        {
            Assert.Equal(usedGas, cylinder.UsedGas);
        }

        [Theory]
        [InlineData(12, 200, 2400)]
        [InlineData(24, 200, 4800)]
        [InlineData(12, 100, 1200)]
        [InlineData(12, 50, 600)]
        [InlineData(30, 300, 9000)]
        [InlineData(12, 0, 0)]
        [InlineData(31, 0, 0)]
        [InlineData(0, 200, 0)]
        [InlineData(0, 301, 0)]
        [InlineData(0, 0, 0)]
        public void CalculateInitialPressurisedVolume(byte cylinderVolume, ushort cylinderPressure, ushort expectedInitialPressurisedVolume)
        {
            cylinder = new Cylinder(name, cylinderVolume, cylinderPressure, surfaceAirConsumptionRate, dummyGasMixture.Object, 0, 0);

            Assert.Equal(expectedInitialPressurisedVolume, cylinder.InitialPressurisedVolume);
        }

        [Theory]
        [InlineData(12, 50, 10, 720, 1680)]
        [InlineData(24, 50, 10, 1440, 960)]
        [InlineData(12, 100, 10, 1320, 1080)]
        [InlineData(12, 50, 20, 1440, 960)]
        [InlineData(12, 0, 0, 0, 2400)]
        [InlineData(12, 50, 0, 0, 2400)]
        [InlineData(12, 0, 10, 0, 2400)]
        [InlineData(0, 50, 10, 0, 2400)]
        [InlineData(0, 0, 0, 0, 2400)]
        public void CalculateGasUsage(byte surfaceAirConsumptionRate, byte depth, byte time, ushort expectedUsedGas, ushort expectedRemainingGas)
        {
            cylinder = new Cylinder(name, cylinderVolume, cylinderPressure, surfaceAirConsumptionRate, dummyGasMixture.Object, 0, 0);
            Mock<IDiveStep> diveStep = new();
            diveStep.Setup(ds => ds.Depth).Returns(depth);
            diveStep.Setup(ds => ds.Time).Returns(time);

            cylinder.UpdateCylinderGasConsumption(diveStep.Object);

            Assert.Equal(expectedUsedGas, cylinder.UsedGas);
            Assert.Equal(expectedRemainingGas, cylinder.RemainingGas);
        }
    }
}