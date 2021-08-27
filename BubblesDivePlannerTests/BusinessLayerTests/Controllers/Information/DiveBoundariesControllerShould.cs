using System.Collections.Generic;
using BubblesDivePlanner.Controllers.Information;
using BubblesDivePlanner.ViewModels.DiveApplication.Plan;
using Xunit;

namespace BubblesDivePlannerTests.BusinessLayerTests.Controllers.Information
{
    public class DiveBoundariesControllerShould
    {
        [Theory]
        [InlineData(21, 56.67)]
        [InlineData(100, 4)]
        public void CalculateTheMaximumOperatingDepth(double oxygenPercentage, double expectedDepth)
        {
            //Arrange
            var gasMixtureViewModel = new GasMixtureSelectorViewModel();

            var maxOperatingDepthController = new MaxOperatingDepthController();

            //Act
            gasMixtureViewModel.MaximumOperatingDepth =
                maxOperatingDepthController.CalculateMaximumOperatingDepth(oxygenPercentage);

            //Assert
            Assert.Equal(expectedDepth, gasMixtureViewModel.MaximumOperatingDepth);
        }

        [Theory]
        [InlineData(4.1, 0.99, 1.41)]
        [InlineData(50, 6.0, 2.0)]
        public void PerformDiveCeilingCalculation(double ceilingExpected1,
            double toleratedAmbientPressure1,
            double toleratedAmbientPressure2)
        {
            //Arrange
            var diveCeilingController = new DiveCeilingController();
            var toleratedAmbientPressures1 = new List<double>()
            {
                toleratedAmbientPressure1, toleratedAmbientPressure2,
            };

            //Act
            var diveCeiling1 = diveCeilingController.CalculateDiveCeiling(toleratedAmbientPressures1);

            //Assert
            Assert.Equal(ceilingExpected1, diveCeiling1, 2);
        }
    }
}