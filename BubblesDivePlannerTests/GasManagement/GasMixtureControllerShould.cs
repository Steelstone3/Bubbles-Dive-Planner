using BubblesDivePlanner.GasManagement;
using Xunit;

namespace BubblesDivePlannerTests.ApplicationLayerTests.ViewModels.Plan
{
    public class GasMixtureControllerShould
    {
        private IGasMixtureController _gasMixtureController = new GasMixtureController();
        
        [Fact]
        public void AllowModelToBeSet()
        {
            //Arrange
            int oxygen = 21;
            int helium = 10;
            int expectedNitrogen = 69;

            //Act
            var actualNitrogen = _gasMixtureController.CalculateNitrogenMixture(oxygen, helium);

            //Assert
            Assert.Equal(expectedNitrogen, actualNitrogen);
        }
    }
}