using BubblesDivePlanner.DiveStages;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.DiveStages
{
    public class TissuePressureShould
    {
        private DiveStagesTextFixture diveStagesTextFixture = new DiveStagesTextFixture();

        [Fact]
        public void RunTissuePressureStage()
        {
            //Arrange
            var diveModel = diveStagesTextFixture.GetDiveModel;
            diveModel.DiveProfile = diveStagesTextFixture.GetDiveProfileResultFromFirstRun;
            var diveStep = diveStagesTextFixture.GetDiveStep;
            var expectedTissuePressureNitrogen = diveModel.DiveProfile.TissuePressuresNitrogen;
            var expectedTissuePressureHelium = diveModel.DiveProfile.TissuePressuresHelium;
            var expectedTissuePressureTotal = diveModel.DiveProfile.TissuePressuresTotal;

            var diveStage = new TissuePressureCommand(diveModel, diveStep);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(expectedTissuePressureNitrogen, diveModel.DiveProfile.TissuePressuresNitrogen);
            Assert.Equal(expectedTissuePressureHelium, diveModel.DiveProfile.TissuePressuresHelium);
            Assert.Equal(expectedTissuePressureTotal, diveModel.DiveProfile.TissuePressuresTotal);
        }
    }
}