using BubblesDivePlanner.DiveBoundaries;
using Xunit;

namespace BubblesDivePlannerTests.DiveBoundaries
{
    public class MaximumOperatingDepthControllerShould
    {
        private MaximumOperatingDepthController _maximumOperatingDepthController = new();

        [InlineData(21, 56.67)]
        [InlineData(32, 33.75)]
        [InlineData(50, 18)]
        [InlineData(100, 4)]
        [Theory]
        public void CalculateMaximumOperatingDepth(int oxygenPercentage, double expectedMaximumOperatingDepth)
        {
            //Act
            var maximumOperatingDepth = _maximumOperatingDepthController.CalculateMaximumOperatingDepth(oxygenPercentage);

            //Assert
            Assert.Equal(expectedMaximumOperatingDepth, maximumOperatingDepth);
        }

    }
}