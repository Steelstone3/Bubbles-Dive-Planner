using Xunit;
using BubblesDivePlanner.DiveStages.Runner;
using BubblesDivePlannerTests.TestFixtures;
using BubblesDivePlannerTests.Asserters;
using BubblesDivePlanner.Controllers.Interfaces;

namespace BubblesDivePlannerTests.Controllers
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

        [Fact(Skip = "Need to implement second result in test fixture")]
        public void RunDiveStagesMoreThanOnce()
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

            //Act
            diveStageRunner.RunDiveStages(diveModel, diveStepModel, selectedCylinder);

            //Assert
            DiveParameterAsserter.AssertDiveStepValuesEquality(diveStepModel, diveStepModel);
            DiveParameterAsserter.AssertSelectedCylinderValuesEquality(selectedCylinder, selectedCylinder);
            DiveParameterAsserter.AssertDiveProfileValuesEquality(DivePlannerApplicationTestFixture.GetDiveProfileResultFromSecondRun, diveModel.DiveProfile);
        }
    }
}