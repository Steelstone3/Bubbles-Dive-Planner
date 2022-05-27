using BubblesDivePlanner.DiveStages;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.DiveStages
{
    public class ToleratedAmbientPressureShould
    {
        private DiveStagesTextFixture diveStagesTextFixture = new DiveStagesTextFixture();
      
        [Fact]
        public void RunToleratedAmbientPressureStage()
        {
            //Arrange
            var diveModel = diveStagesTextFixture.GetDiveModel;
            diveModel.DiveProfile = diveStagesTextFixture.GetDiveProfileResultFromFirstRun;
            var toleratedAmbientPressuresResult = diveStagesTextFixture.GetDiveProfileResultFromFirstRun.ToleratedAmbientPressures;
            
            var diveStage = new ToleratedAmbientPressureCommand(diveModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(toleratedAmbientPressuresResult, diveModel.DiveProfile.ToleratedAmbientPressures);
        }
    }
}