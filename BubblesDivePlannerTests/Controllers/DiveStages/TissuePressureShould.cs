using BubblesDivePlanner.DiveStages;
using BubblesDivePlanner.Models.DiveModels;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.DiveStages
{
    public class TissuePressureShould
    {
        [Fact]
        public void RunTissuePressureStages()
        {
            //Arrange
            var diveProfile = new DiveProfile
            (
                TestFixture.DefaultTissuesList,
                TestFixture.DefaultList,
                TestFixture.DefaultTissuesList,
                null,
                null,
                null,
                null,
                null,
                TestFixture.ExpectedOxygenPressureAtDepth,
                TestFixture.ExpectedHeliumPressureAtDepth,
                TestFixture.ExpectedNitrogenPressureAtDepth,
                0
            );
            var diveModel = TestFixture.FixtureDiveModel(diveProfile);
            var diveStep = TestFixture.FixtureDiveStep;
            var diveStage = new TissuePressure(diveModel, diveStep);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(TestFixture.ExpectedNitrogenTissuePressures, diveModel.DiveProfile.NitrogenTissuePressures);
            Assert.Equal(TestFixture.ExpectedHeliumTissuePressures, diveModel.DiveProfile.HeliumTissuePressures);
            Assert.Equal(TestFixture.ExpectedTotalTissuePressures, diveModel.DiveProfile.TotalTissuePressures);
        }
    }
}