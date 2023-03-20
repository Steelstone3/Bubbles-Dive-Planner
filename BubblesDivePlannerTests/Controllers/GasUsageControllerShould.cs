using BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlannerTests.TestFixtures;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Cylinders.CylinderSetup.GasUsage
{
    public class GasUsageControllerShould
    {
        private readonly IGasUsageController _gasUsageController = new GasUsageController();

        [Theory]
        [InlineData(12, 200, 2400)]
        [InlineData(10, 200, 2000)]
        [InlineData(12, 100, 1200)]
        public void CalculateInitialPressurisedCylinderVolume(byte cylinderVolume, ushort clyinderPressure, ushort expectedInitialPressurisedCylinderVolume)
        {
            //Act
            int actualInitialPressurisedCylinderVolume = _gasUsageController.CalculateInitialPressurisedCylinderVolume(cylinderVolume, clyinderPressure);

            //Assert
            Assert.Equal(expectedInitialPressurisedCylinderVolume, actualInitialPressurisedCylinderVolume);
        }

        [Fact]
        public void CalculateGasUsed()
        {
            //Act
            var gasUsed = _gasUsageController.CalculateGasUsed(DivePlannerApplicationTestFixture.GetDiveStep, DivePlannerApplicationTestFixture.GetSelectedCylinder.GasUsage.SurfaceAirConsumptionRate);

            //Assert
            Assert.Equal(720, gasUsed);
        }

        [Fact]
        public void CalculateGasRemaining()
        {
            //Act
            var gasRemaining = _gasUsageController.CalculateRemainingPressurisedCylinderVolume(DivePlannerApplicationTestFixture.GetSelectedCylinder.GasUsage.GasRemaining, DivePlannerApplicationTestFixture.GetSelectedCylinder.GasUsage.GasUsed);

            //Assert
            Assert.Equal(1680, gasRemaining);
        }

        [Fact]
        public void OverflowGasRemainingToZero()
        {
            //Act
            var gasRemaining = _gasUsageController.CalculateRemainingPressurisedCylinderVolume(DivePlannerApplicationTestFixture.GetSelectedCylinder.GasUsage.GasRemaining, 3000);

            //Assert
            Assert.Equal(0, gasRemaining);
        }
    }
}