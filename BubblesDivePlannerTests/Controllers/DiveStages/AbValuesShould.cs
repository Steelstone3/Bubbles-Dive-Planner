using BubblesDivePlanner.Controllers.DiveStages;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.DiveStages
{
    public class AbValuesShould
    {

        [Fact]
        public void RunAbValuesStage()
        {
            //Arrange
            var diveModel = TestFixture.FixtureDiveModel(null);
            IDiveStageCommand diveStage = new AbValues(diveModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(TestFixture.ExpectedAValues, diveModel.DiveProfile.AValues);
            Assert.Equal(TestFixture.ExpectedBValues, diveModel.DiveProfile.BValues);
        }
    }
}