using Xunit;
using BubblesDivePlanner.DiveStages.Runner;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage;
using System.Collections.Generic;
using System;
using BubblesDivePlanner.Results;
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
            var results = diveStageRunner.RunDiveStages(diveModel, diveStepModel, selectedCylinder);

            //Assert
            diveParameterAsserter.AssertDiveStepValuesEquality(diveStepModel, results.DiveStepModel);
            diveParameterAsserter.AssertSelectedCylinderValuesEquality(selectedCylinder, results.CylinderSetupModel);
            diveParameterAsserter.AssertDiveProfileValuesEquality(diveStagesTextFixture.GetDiveProfileResultFromFirstRun, results.DiveProfileModel);
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
            var results = diveStageRunner.RunDiveStages(diveModel, diveStepModel, selectedCylinder);
            
            //Assert
            diveParameterAsserter.AssertDiveStepValuesEquality(diveStepModel, results.DiveStepModel);
            diveParameterAsserter.AssertSelectedCylinderValuesEquality(selectedCylinder, results.CylinderSetupModel);
            diveParameterAsserter.AssertDiveProfileValuesEquality(diveStagesTextFixture.GetDiveProfileResultFromFirstRun, results.DiveProfileModel);

            //Act
            results = diveStageRunner.RunDiveStages(diveModel, diveStepModel, selectedCylinder);
            
            //Assert
            diveParameterAsserter.AssertDiveStepValuesEquality(diveStepModel, results.DiveStepModel);
            diveParameterAsserter.AssertSelectedCylinderValuesEquality(selectedCylinder, results.CylinderSetupModel);
            diveParameterAsserter.AssertDiveProfileValuesEquality(diveStagesTextFixture.GetDiveProfileResultFromSecondRun, results.DiveProfileModel);
        }

    }
}