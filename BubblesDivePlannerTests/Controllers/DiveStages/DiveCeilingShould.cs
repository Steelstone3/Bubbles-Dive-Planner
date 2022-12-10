using BubblesDivePlanner.Controllers.DiveStages;
using BubblesDivePlanner.Models.DiveModels;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.DiveStages
{
    public class DiveCeilingShould
    {
        [Fact]
        public void RunDiveCeilingStage()
        {
            //Arrange
            var diveProfile = new DiveProfile
            (
                null,
                null,
                null,
                null,
                TestFixture.ExpectedToleratedAmbientPressures,
                null,
                null,
                null,
                0,
                0,
                0,
                0
            );
            var diveModel = TestFixture.FixtureDiveModel(diveProfile);
            var diveStage = new DiveCeiling(diveModel.DiveProfile);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(4.07, diveModel.DiveProfile.DepthCeiling);
        }
    }
}