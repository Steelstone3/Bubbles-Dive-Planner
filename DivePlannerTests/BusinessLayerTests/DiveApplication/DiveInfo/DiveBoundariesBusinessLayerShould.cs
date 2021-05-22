using System.Collections.Generic;
using DivePlannerMk3.Controllers;
using DivePlannerMk3.ViewModels.DivePlan;
using Xunit;

namespace DivePlannerTests
{
    public class DiveBoundariesBusinessLayerShould
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
            gasMixtureViewModel.MaximumOperatingDepth = maxOperatingDepthController.CalculateMaximumOperatingDepth(oxygenPercentage);

            //Assert
            Assert.Equal(expectedDepth, gasMixtureViewModel.MaximumOperatingDepth);
        }

        [Fact]
        public void CalculateTheCeilingDepth1()
        {
            //A
            var diveCeilingController = new DiveCeilingController();
            
            var ambPressures = new List<double>()
            {
                1.1,1.2,1.3,1.4,1.5,1.99,2,
            };

            //A
            var result = diveCeilingController.CalculateDiveCeiling(ambPressures);

            //A
            Assert.Equal(10, result);
        }

         [Fact]
        public void CalculateTheCeilingDepth2()
        {
            //A
            var diveCeilingController = new DiveCeilingController();
            
            var ambPressures2 = new List<double>()
            {
                1.5,1.2,1.3,1.4,1.5,1.4,1.3,
            };

            //A
            var result2 = diveCeilingController.CalculateDiveCeiling(ambPressures2);

            //A
            Assert.Equal(5,result2);
        }
    }
}