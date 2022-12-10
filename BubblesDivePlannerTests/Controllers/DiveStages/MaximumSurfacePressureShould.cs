using BubblesDivePlanner.Controllers.DiveStages;
using BubblesDivePlanner.Models.DiveModels;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.DiveStages
{
    public class MaximumSurfacePressureShould
    {
        [Fact]
        public void RunMaximumSurfacePressureStage()
        {
            //Arrange
            var diveProfile = new DiveProfile
            (
                null,
                null,
                null,
                TestFixture.DefaultList,
                null,
                TestFixture.ExpectedAValues,
                TestFixture.ExpectedBValues,
                null,
                0,
                0,
                0,
                0
            );
            var diveModel = TestFixture.FixtureDiveModel(diveProfile);
            var diveStage = new MaximumSurfacePressure(diveModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(TestFixture.ExpectedMaxSurfacePressures, diveModel.DiveProfile.MaxSurfacePressures);
        }
    }
}