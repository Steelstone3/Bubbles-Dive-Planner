using BubblesDivePlanner.DiveStages;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.DiveStages
{
    public class TissuePressureShould
    {
        [Fact]
        public void RunTissuePressureStage()
        {
            //Arrange
            var diveModel = DivePlannerApplicationTestFixture.GetDiveModel;
            diveModel.DiveProfile = DivePlannerApplicationTestFixture.GetDiveProfileResultFromFirstRun;
            var diveStep = DivePlannerApplicationTestFixture.GetDiveStep;
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