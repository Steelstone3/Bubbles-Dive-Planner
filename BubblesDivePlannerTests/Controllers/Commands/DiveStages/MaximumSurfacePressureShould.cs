using BubblesDivePlanner.Commands;
using BubblesDivePlanner.Commands.Interfaces;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.Commands.DiveStages
{
    public class MaximumSurfacePressureShould
    {
        [Fact]
        public void RunMaximumSurfacePressureStage()
        {
            //Arrange
            IDiveModel diveModel = PlannerTestFixture.GetDiveModel;
            diveModel.DiveProfile.AValues = diveModel.DiveProfile.AValues;
            diveModel.DiveProfile.BValues = diveModel.DiveProfile.BValues;
            float[] expectedMaxSurfacePresureResult = diveModel.DiveProfile.MaxSurfacePressures;
            IDiveStageCommand diveStage = new MaximumSurfacePressure(diveModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(expectedMaxSurfacePresureResult, diveModel.DiveProfile.MaxSurfacePressures);
        }
    }
}