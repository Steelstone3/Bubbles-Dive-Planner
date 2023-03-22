using Xunit;
using BubblesDivePlannerTests.TestFixtures;
using BubblesDivePlannerTests.Asserters;
using BubblesDivePlanner.Controllers.Interfaces;
using BubblesDivePlanner.Controllers.DiveStages;

namespace BubblesDivePlannerTests.Controllers.DiveStages
{
    public class DiveStageRunnerShould
    {
        [Fact]
        public void RunDiveStages()
        {
            //Arrange
            var diveModel = DivePlannerApplicationTestFixture.GetDiveModel;
            var diveStepModel = DivePlannerApplicationTestFixture.GetDiveStep;
            var selectedCylinder = DivePlannerApplicationTestFixture.GetSelectedCylinder;

            IDiveStageRunner diveStageRunner = new DiveStageRunner();

            //Act
            diveStageRunner.RunDiveStages(diveModel, diveStepModel, selectedCylinder);

            //Assert
            DiveParameterAsserter.AssertDiveStepValuesEquality(diveStepModel, diveStepModel);
            DiveParameterAsserter.AssertSelectedCylinderValuesEquality(selectedCylinder, selectedCylinder);
            DiveParameterAsserter.AssertDiveProfileValuesEquality(DivePlannerApplicationTestFixture.GetDiveProfileResultFromFirstRun, diveModel.DiveProfile);
        }
    }
}