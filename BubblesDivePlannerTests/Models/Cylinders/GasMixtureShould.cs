using BubblesDivePlanner.Models.Cylinders;
using Xunit;

namespace BubblesDivePlannerTests.Models.Cylinders
{
    public class GasMixtureShould
    {
        [Theory]
        [InlineData(21, 0, 21, 0, 79, 56.67)]
        [InlineData(21, 10, 21, 10, 69, 56.67)]
        [InlineData(100, 0, 100, 0, 0, 4.0)]
        [InlineData(0, 0, 0, 0, 100, 0.0)]
        [InlineData(32, 0, 32, 0, 68, 33.75)]
        public void ConstructAGasMixture(byte oxygen, byte helium, byte expectedOxygen, byte expectedHelium, byte expectedNitrogen, double expectedMaximumOperatingDepth)
        {
            IGasMixture gasMixture = new GasMixture(oxygen, helium);

            Assert.Equal(expectedOxygen, gasMixture.Oxygen);
            Assert.Equal(expectedHelium, gasMixture.Helium);
            Assert.Equal(expectedNitrogen, gasMixture.Nitrogen);
            Assert.Equal(expectedMaximumOperatingDepth, gasMixture.MaximumOperatingDepth);
        }
    }
}