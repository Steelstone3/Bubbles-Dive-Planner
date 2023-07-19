using BubblesDivePlanner.Commands;
using BubblesDivePlanner.Commands.Interfaces;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlanner.ViewModels.Planner.DiveModels;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.Commands
{
    public class ABValuesCommandShould
    {
        [Fact]
        public void RunAbValuesStage()
        {
            //Arrange
            IDiveProfile expectedDiveProfile = PlannerTestFixture.GetDiveProfileResult;
            IDiveModel diveModel = PlannerTestFixture.GetDiveModel;
            diveModel.DiveProfile.NitrogenTissuePressures = PlannerTestFixture.GetDiveProfile.NitrogenTissuePressures;
            diveModel.DiveProfile.HeliumTissuePressures = PlannerTestFixture.GetDiveProfile.HeliumTissuePressures;
            diveModel.DiveProfile.TotalTissuePressures = PlannerTestFixture.GetDiveProfile.TotalTissuePressures;
            IDiveStageCommand diveStage = new ABValuesCommand(diveModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(expectedDiveProfile.AValues, diveModel.DiveProfile.AValues);
            Assert.Equal(expectedDiveProfile.BValues, diveModel.DiveProfile.BValues);
        }
    }
}