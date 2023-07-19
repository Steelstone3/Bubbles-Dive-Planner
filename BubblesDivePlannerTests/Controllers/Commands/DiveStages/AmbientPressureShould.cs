using BubblesDivePlanner.Commands;
using BubblesDivePlanner.Commands.Interfaces;
using BubblesDivePlanner.ViewModels.Model.Planner.Cylinders;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan.Stage;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.Commands.DiveStages
{
    public class AmbientPressureShould
    {
        [Fact]
        public void RunAmbientPressureStage()
        {
            //Arrange
            IDiveProfile expectedDiveProfile = PlannerTestFixture.GetDiveProfileResult;
            IDiveProfile diveProfile = PlannerTestFixture.GetDiveModel.DiveProfile;
            IGasMixture gasMixtureModel = PlannerTestFixture.GetCylinder.GasMixture;
            IDiveStep diveStepModel = PlannerTestFixture.GetDiveStep;
            IDiveStageCommand diveStage = new AmbientPressure(diveProfile, gasMixtureModel, diveStepModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(expectedDiveProfile.OxygenAtPressure, diveProfile.OxygenAtPressure);
            Assert.Equal(expectedDiveProfile.HeliumAtPressure, diveProfile.HeliumAtPressure);
            Assert.Equal(expectedDiveProfile.NitrogenAtPressure, diveProfile.NitrogenAtPressure);
        }
    }
}