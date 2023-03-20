using BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture;
using Xunit;

namespace BubblesDivePlannerTests.Cylinders.CylinderSetup.GasMixture
{
    public class GasMixtureControllerShould
    {
        private readonly IGasMixtureController _gasMixtureController = new GasMixtureController();

        [Fact]
        public void CalculateNitrogenMixture()
        {
            //Arrange
            const int oxygen = 21;
            const int helium = 10;
            const int expectedNitrogen = 69;

            //Act
            var actualNitrogen = _gasMixtureController.CalculateNitrogenMixture(oxygen, helium);

            //Assert
            Assert.Equal(expectedNitrogen, actualNitrogen);
        }
    }
}