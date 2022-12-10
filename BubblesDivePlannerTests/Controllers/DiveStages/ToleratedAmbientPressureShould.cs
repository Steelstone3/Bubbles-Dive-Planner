using BubblesDivePlanner.DiveStages;
using BubblesDivePlanner.Models.DiveModels;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.DiveStages
{
    public class ToleratedAmbientPressureShould
    {
        [Fact]
        public void RunToleratedAmbientPressureStage()
        {
            //Arrange
            var diveProfile = new DiveProfile
            (
                null,
                null,
                TestFixture.ExpectedTotalTissuePressures,
                null,
                TestFixture.DefaultList,
                TestFixture.ExpectedAValues,
                TestFixture.ExpectedBValues,
                null,
                0,
                0,
                0,
                0
            );
            var diveModel = TestFixture.FixtureDiveModel(diveProfile);
            var diveStage = new ToleratedAmbientPressure(diveModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(TestFixture.ExpectedToleratedAmbientPressures, diveModel.DiveProfile.ToleratedAmbientPressures);
        }
    }
}