using BubblesDivePlanner.Controllers.DiveStages;
using BubblesDivePlanner.Models.DiveModels;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.DiveStages
{
    public class CompartmentLoadShould
    {
        [Fact]
        public void RunCompartmentLoadStage()
        {
            //Arrange
            var diveProfile = new DiveProfile
            (
                null,
                null,
                TestFixture.ExpectedTotalTissuePressures,
                TestFixture.ExpectedMaxSurfacePressures,
                null,
                null,
                null,
                TestFixture.DefaultList,
                0,
                0,
                0,
                0
            );
            var diveModel = TestFixture.FixtureDiveModel(diveProfile);
            var diveStage = new CompartmentLoad(diveModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(TestFixture.ExpectedCompartmentLoads, diveModel.DiveProfile.CompartmentLoads);
        }
    }
}