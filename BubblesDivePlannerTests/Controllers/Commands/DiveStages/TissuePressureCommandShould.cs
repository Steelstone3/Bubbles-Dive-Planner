using BubblesDivePlanner.Commands;
using BubblesDivePlanner.Commands.Interfaces;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan.Stage;
using BubblesDivePlanner.ViewModels.Planner.DiveModels;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.Commands
{
    public class TissuePressureShould
    {
        [Fact]
        public void RunTissuePressureStage()
        {
            //Arrange
            Zhl16bBuhlmann diveModel = PlannerTestFixture.GetDiveModel;
            IDiveStep diveStep = PlannerTestFixture.GetDiveStep;
            float expectedTissuePressureNitrogen = diveModel.DiveProfile.NitrogenAtPressure;
            float expectedTissuePressureHelium = diveModel.DiveProfile.HeliumAtPressure;
            float[] expectedTissuePressureTotal = diveModel.DiveProfile.TotalTissuePressures;
            IDiveStageCommand diveStage = new TissuePressureCommand(diveModel, diveStep);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(expectedTissuePressureNitrogen, diveModel.DiveProfile.NitrogenAtPressure);
            Assert.Equal(expectedTissuePressureHelium, diveModel.DiveProfile.HeliumAtPressure);
            Assert.Equal(expectedTissuePressureTotal, diveModel.DiveProfile.TotalTissuePressures);
        }
    }
}