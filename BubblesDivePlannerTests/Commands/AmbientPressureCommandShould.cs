using BubblesDivePlanner.Commands;
using BubblesDivePlanner.Commands.Interfaces;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.Commands
{
    public class AmbientPressureShould
    {
        [Fact]
        public void RunAmbientPressurePreStage()
        {
            //Arrange
            var expectedDiveProfile = DivePlannerApplicationTestFixture.GetDiveProfileResultFromFirstRun;
            var diveProfile = DivePlannerApplicationTestFixture.GetDiveModel.DiveProfile;
            var gasMixtureModel = DivePlannerApplicationTestFixture.GetSelectedCylinder.GasMixture;
            var diveStepModel = DivePlannerApplicationTestFixture.GetDiveStep;

            IDiveStageCommand diveStage = new AmbientPressureCommand(diveProfile, gasMixtureModel, diveStepModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(expectedDiveProfile.PressureOxygen, diveProfile.PressureOxygen);
            Assert.Equal(expectedDiveProfile.PressureHelium, diveProfile.PressureHelium);
            Assert.Equal(expectedDiveProfile.PressureNitrogen, diveProfile.PressureNitrogen);
        }
    }
}