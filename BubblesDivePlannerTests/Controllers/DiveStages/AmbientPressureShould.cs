using BubblesDivePlanner.Controllers.DiveStages;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.DiveStages
{
    public class AmbientPressureShould
    {
        [Fact]
        public void RunAmbientPressureStage()
        {
            //Arrange
            var diveProfile = TestFixture.FixtureDiveModel(null).DiveProfile;
            var gasMixtureModel = TestFixture.FixtureSelectedCylinder.GasMixture;
            var diveStepModel = TestFixture.FixtureDiveStep;
            IDiveStageCommand diveStage = new AmbientPressure(diveProfile, gasMixtureModel, diveStepModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(TestFixture.ExpectedOxygenPressureAtDepth, diveProfile.OxygenPressureAtDepth);
            Assert.Equal(TestFixture.ExpectedHeliumPressureAtDepth, diveProfile.HeliumPressureAtDepth);
            Assert.Equal(TestFixture.ExpectedNitrogenPressureAtDepth, diveProfile.NitrogenPressureAtDepth);
        }
    }
}