using BubblesDivePlanner.Controllers.DiveStages;
using BubblesDivePlanner.Models.Cylinders;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.DiveStages
{
    public class GasManagementShould
    {
        [Fact]
        public void RunGasManagementStage()
        {
            // Given
            var selectedCylinder = new Mock<ICylinder>();
            var diveStep = TestFixture.FixtureDiveStep;
            selectedCylinder.Setup(sc => sc.UpdateCylinderGasConsumption(diveStep));
            var gasManagement = new GasManagement(selectedCylinder.Object, diveStep);

            // When
            gasManagement.RunDiveStage();

            // Then
            selectedCylinder.VerifyAll();
        }
    }
}