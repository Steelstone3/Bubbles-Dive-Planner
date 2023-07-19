using BubblesDivePlanner.Commands;
using BubblesDivePlanner.Commands.Interfaces;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.Commands.DiveStages
{
    public class CompartmentLoadShould
    {
        [Fact]
        public void RunCompartmentLoadStage()
        {
            //Arrange
            IDiveProfile expectedDiveProfile = PlannerTestFixture.GetDiveProfileResult;
            IDiveModel diveModel = PlannerTestFixture.GetDiveModel;
            diveModel.DiveProfile.TotalTissuePressures = expectedDiveProfile.TotalTissuePressures;
            diveModel.DiveProfile.MaxSurfacePressures = expectedDiveProfile.MaxSurfacePressures;
            IDiveStageCommand diveStage = new CompartmentLoad(diveModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(expectedDiveProfile.CompartmentLoads, diveModel.DiveProfile.CompartmentLoads);
        }
    }
}