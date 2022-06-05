using Xunit;
using BubblesDivePlanner.DiveStages.Runner;
using System.Collections.Generic;
using BubblesDivePlannerTests.TestFixtures;
using BubblesDivePlannerTests.Asserters;

namespace BubblesDivePlannerTests.DiveStages.Runner
{
    public class DiveStageRunnerShould
    {
        private DivePlannerApplicationTestFixture diveStagesTextFixture = new DivePlannerApplicationTestFixture();
        private DiveParameterAsserter diveParameterAsserter = new DiveParameterAsserter();

        [Fact]
        public void RunDiveStages()
        {
            //Arrange
            var diveModel = diveStagesTextFixture.GetDiveModel;
            var diveStepModel = diveStagesTextFixture.GetDiveStep;
            var selectedCylinder = diveStagesTextFixture.GetSelectedCylinder;

            IDiveStageRunner diveStageRunner = new DiveStageRunner();

            //Act
            diveStageRunner.RunDiveStages(diveModel, diveStepModel, selectedCylinder);

            //Assert
            diveParameterAsserter.AssertDiveStepValuesEquality(diveStepModel, diveStepModel);
            diveParameterAsserter.AssertSelectedCylinderValuesEquality(selectedCylinder, selectedCylinder);
            diveParameterAsserter.AssertDiveProfileValuesEquality(diveStagesTextFixture.GetDiveProfileResultFromFirstRun, diveModel.DiveProfile);
        }

        [Fact(Skip = "Need to implement second result in test fixture")]
        public void RunDiveStagesMoreThanOnce()
        {
            //Arrange
            var diveModel = diveStagesTextFixture.GetDiveModel;
            var diveStepModel = diveStagesTextFixture.GetDiveStep;
            var selectedCylinder = diveStagesTextFixture.GetSelectedCylinder;
            IDiveStageRunner diveStageRunner = new DiveStageRunner();

            //Act
            diveStageRunner.RunDiveStages(diveModel, diveStepModel, selectedCylinder);
            
            //Assert
            diveParameterAsserter.AssertDiveStepValuesEquality(diveStepModel, diveStepModel);
            diveParameterAsserter.AssertSelectedCylinderValuesEquality(selectedCylinder, selectedCylinder);
            diveParameterAsserter.AssertDiveProfileValuesEquality(diveStagesTextFixture.GetDiveProfileResultFromFirstRun, diveModel.DiveProfile);

            //Act
            diveStageRunner.RunDiveStages(diveModel, diveStepModel, selectedCylinder);
            
            //Assert
            diveParameterAsserter.AssertDiveStepValuesEquality(diveStepModel, diveStepModel);
            diveParameterAsserter.AssertSelectedCylinderValuesEquality(selectedCylinder, selectedCylinder);
            diveParameterAsserter.AssertDiveProfileValuesEquality(diveStagesTextFixture.GetDiveProfileResultFromSecondRun, diveModel.DiveProfile);
        }

    }
}