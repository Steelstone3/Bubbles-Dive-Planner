using BubblesDivePlanner.Commands;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.Commands
{
    public class MaximumSurfacePressureCommandShould
    {
        [Fact]
        public void RunMaximumSurfacePressureStage()
        {
            //Arrange
            var diveModel = DivePlannerApplicationTestFixture.GetDiveModel;
            diveModel.DiveProfile = DivePlannerApplicationTestFixture.GetDiveProfileResultFromFirstRun;
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