using BubblesDivePlanner.Commands;
using BubblesDivePlanner.Commands.Interfaces;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlanner.ViewModels.Planner.DiveModels;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.Commands
{
    public class MaximumSurfacePressureCommandShould
    {
        [Fact]
        public void RunMaximumSurfacePressureStage()
        {
            //Arrange
            IDiveModel diveModel = PlannerTestFixture.GetDiveModel;
            diveModel.DiveProfile.AValues = diveModel.DiveProfile.AValues;
            diveModel.DiveProfile.BValues = diveModel.DiveProfile.BValues;
            float[] expectedMaxSurfacePresureResult = diveModel.DiveProfile.MaxSurfacePressures;
            IDiveStageCommand diveStage = new MaximumSurfacePressureCommand(diveModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(expectedMaxSurfacePresureResult, diveModel.DiveProfile.MaxSurfacePressures);
        }
    }
}