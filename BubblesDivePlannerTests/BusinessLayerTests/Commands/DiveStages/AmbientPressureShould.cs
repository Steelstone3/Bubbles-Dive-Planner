using BubblesDivePlanner.Commands.DiveStages;
using BubblesDivePlanner.Models.DiveModels;
using Xunit;

namespace BubblesDivePlannerTests.BusinessLayerTests.Commands.DiveStages
{
    public class AmbientPressureShould
    {
        //Using the buhlmann model for tests
        private DiveProfile _diveProfile = new DiveProfile();

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 1)]
        [InlineData(21, 0, 0, 0.21, 0, 0.79)]
        [InlineData(32, 50, 0, 0.32, 0.5, 0.18)]
        [InlineData(32, 50, 10, 0.64, 1, 0.36)]
        [InlineData(32, 50, 30, 1.28, 2, 0.72)]
        public void RunAmbientPressurePreStage(double oxygenPercentage, double heliumPercentage, int depth,
            double resultOxygen, double resultHelium, double resultNitrogen)
        {
            //Arrange
            var diveStage = new PreDiveStageAmbientPressure(_diveProfile, oxygenPercentage, heliumPercentage, depth);

            //Act
            diveStage.RunStage();

            //Assert
            Assert.Equal(resultOxygen, _diveProfile.PressureOxygen, 2);
            Assert.Equal(resultHelium, _diveProfile.PressureHelium, 2);
            Assert.Equal(resultNitrogen, _diveProfile.PressureNitrogen, 2);
        }
    }
}