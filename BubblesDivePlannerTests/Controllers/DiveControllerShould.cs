using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Planner.Plan.Stage;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.Controllers
{
    public class DiveControllerShould
    {
        [Fact]
        public void RunDiveStages()
        {
            // Given
            IDiveStage expectedDiveStage = new DiveStage
            {
                DiveModel = PlannerTestFixture.GetDiveModel,
                DiveStep = PlannerTestFixture.GetDiveStep,
                Cylinder = PlannerTestFixture.GetCylinder
            };
            expectedDiveStage.DiveModel.DiveProfile.NitrogenTissuePressures = PlannerTestFixture.GetDiveProfileResult.NitrogenTissuePressures;
            expectedDiveStage.DiveModel.DiveProfile.HeliumTissuePressures = PlannerTestFixture.GetDiveProfileResult.HeliumTissuePressures;
            expectedDiveStage.DiveModel.DiveProfile.TotalTissuePressures = PlannerTestFixture.GetDiveProfileResult.TotalTissuePressures;
            expectedDiveStage.DiveModel.DiveProfile.AValues = PlannerTestFixture.GetDiveProfileResult.AValues;
            expectedDiveStage.DiveModel.DiveProfile.BValues = PlannerTestFixture.GetDiveProfileResult.BValues;
            expectedDiveStage.DiveModel.DiveProfile.MaxSurfacePressures = PlannerTestFixture.GetDiveProfileResult.MaxSurfacePressures;
            expectedDiveStage.DiveModel.DiveProfile.ToleratedAmbientPressures = PlannerTestFixture.GetDiveProfileResult.ToleratedAmbientPressures;
            expectedDiveStage.DiveModel.DiveProfile.CompartmentLoads = PlannerTestFixture.GetDiveProfileResult.CompartmentLoads;
            expectedDiveStage.DiveModel.DiveProfile.OxygenAtPressure = PlannerTestFixture.GetDiveProfileResult.OxygenAtPressure;
            expectedDiveStage.DiveModel.DiveProfile.HeliumAtPressure = PlannerTestFixture.GetDiveProfileResult.HeliumAtPressure;
            expectedDiveStage.DiveModel.DiveProfile.NitrogenAtPressure = PlannerTestFixture.GetDiveProfileResult.NitrogenAtPressure;

            IDiveStage diveStage = new DiveStage
            {
                DiveModel = PlannerTestFixture.GetDiveModel,
                DiveStep = PlannerTestFixture.GetDiveStep,
                Cylinder = PlannerTestFixture.GetCylinder
            };

            // When
            DiveController.Run(diveStage);

            // Then
            Assert.Equivalent(expectedDiveStage, diveStage);
        }
    }
}