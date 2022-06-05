using BubblesDivePlanner.DiveStages;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.DiveStages
{
    public class MaximumSurfacePressureCommandShould
    {
        private DivePlannerApplicationTestFixture diveStagesTextFixture = new DivePlannerApplicationTestFixture();
        
        [Fact]
        public void RunMaximumSurfacePressureStage()
        {
            //Arrange
            var diveModel = diveStagesTextFixture.GetDiveModel;
            diveModel.DiveProfile = diveStagesTextFixture.GetDiveProfileResultFromFirstRun;
            var aValues = diveModel.DiveProfile.AValues;
            var bValues = diveModel.DiveProfile.BValues;
            diveModel.DiveProfile.AValues = aValues;
            diveModel.DiveProfile.BValues = bValues;
            var expectedMaxSurfacePresureResult = diveModel.DiveProfile.MaxSurfacePressures;

            var diveStage = new MaximumSurfacePressureCommand(diveModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(expectedMaxSurfacePresureResult, diveModel.DiveProfile.MaxSurfacePressures);
        }
    }
}