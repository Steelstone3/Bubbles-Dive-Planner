using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Models;
using Xunit;

namespace BubblesDivePlannerTests.Controllers
{
    public class DiveStagesControllerShould
    {
        private readonly IDivePlan divePlan;
        private IDiveStagesController diveController;

        public DiveStagesControllerShould()
        {
            divePlan = new DivePlan(TestFixture.FixtureDiveModel(null), TestFixture.FixtureCylinders(), TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);
        }

        [Fact]
        public void RunADiveStage()
        {
            // Given
            diveController = new DiveStagesController();

            // When
            diveController.Run(divePlan);

            // Then
            Assert.Equivalent(TestFixture.ExpectedDiveModel, divePlan.DiveModel);
            Assert.Equivalent(TestFixture.FixtureDiveStep, divePlan.DiveStep);
            Assert.Equivalent(TestFixture.ExpectedCylinders(), divePlan.Cylinders);
            Assert.Equivalent(TestFixture.ExpectedSelectedCylinder, divePlan.SelectedCylinder);
        }
    }
}