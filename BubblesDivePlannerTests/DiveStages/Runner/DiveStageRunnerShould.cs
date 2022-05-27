using Xunit;
using BubblesDivePlanner.DiveStages.Runner;
using System.Collections.Generic;
using BubblesDivePlannerTests.TestFixtures;
using BubblesDivePlannerTests.Asserters;

namespace BubblesDivePlannerTests.DiveStages.Runner
{
    public class DiveStageRunnerShould
    {
        private DiveStagesTextFixture diveStagesTextFixture = new DiveStagesTextFixture();
        private DiveParameterAsserter diveParameterAsserter = new DiveParameterAsserter();
        private List<double> compartmentLoad = new List<double> { 124.9, 121.46, 110.01, 99.51, 88.78, 82.71, 76.8, 71.87, 68.05, 66.46, 65.63, 65.15, 65.57, 65.05, 65.29, 65.65 };

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