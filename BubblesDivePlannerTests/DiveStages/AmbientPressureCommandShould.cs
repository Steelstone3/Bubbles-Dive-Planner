using BubblesDivePlanner.DiveStages;
using BubblesDivePlannerTests.Asserters;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.DiveStages
{
    public class AmbientPressureShould
    {
        private DiveStagesTextFixture diveStagesTextFixture = new DiveStagesTextFixture();
        private DiveParameterAsserter diveParameterAsserter = new DiveParameterAsserter();

        [Fact]
        public void RunAmbientPressurePreStage()
        {
            //Arrange
            var expectedDiveProfile = diveStagesTextFixture.GetDiveProfileResultFromFirstRun;
            var diveProfile = diveStagesTextFixture.GetDiveModel.DiveProfile;
            var gasMixtureModel = diveStagesTextFixture.GetSelectedCylinder.GasMixture;
            var diveStepModel = diveStagesTextFixture.GetDiveStep;
            
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