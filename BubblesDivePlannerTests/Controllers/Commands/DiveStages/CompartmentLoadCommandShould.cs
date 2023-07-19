using BubblesDivePlanner.Commands;
using BubblesDivePlanner.Commands.Interfaces;
using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.Commands
{
    public class CompartmentLoadCommandShould
    {
        [Fact]
        public void RunCompartmentLoadStage()
        {
            //Arrange
            IDiveProfile expectedDiveProfile = PlannerTestFixture.GetDiveProfileResult;
            IDiveModel diveModel = PlannerTestFixture.GetDiveModel;
            diveModel.DiveProfile.TotalTissuePressures = expectedDiveProfile.TotalTissuePressures;
            diveModel.DiveProfile.MaxSurfacePressures = expectedDiveProfile.MaxSurfacePressures;
            IDiveStageCommand diveStage = new CompartmentLoadCommand(diveModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(expectedDiveProfile.CompartmentLoads, diveModel.DiveProfile.CompartmentLoads);
        }
    }
}