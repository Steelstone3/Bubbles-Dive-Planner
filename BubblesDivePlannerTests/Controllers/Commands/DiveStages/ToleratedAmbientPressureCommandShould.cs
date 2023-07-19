using BubblesDivePlanner.Commands;
using BubblesDivePlanner.Commands.Interfaces;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlanner.ViewModels.Planner.DiveModels;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.Commands
{
    public class ToleratedAmbientPressureShould
    {
        [Fact]
        public void RunToleratedAmbientPressureStage()
        {
            //Arrange
            IDiveModel diveModel = PlannerTestFixture.GetDiveModel;
            diveModel.DiveProfile.TotalTissuePressures = PlannerTestFixture.GetDiveProfile.ToleratedAmbientPressures;
            diveModel.DiveProfile.AValues = PlannerTestFixture.GetDiveProfile.AValues;
            diveModel.DiveProfile.BValues = PlannerTestFixture.GetDiveProfile.BValues;
            float[] toleratedAmbientPressuresResult = PlannerTestFixture.GetDiveProfile.ToleratedAmbientPressures;
            IDiveStageCommand diveStage = new ToleratedAmbientPressureCommand(diveModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(toleratedAmbientPressuresResult, diveModel.DiveProfile.ToleratedAmbientPressures);
        }
    }
}