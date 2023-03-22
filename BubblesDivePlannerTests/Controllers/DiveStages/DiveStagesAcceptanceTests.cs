using BubblesDivePlanner.Controllers.DiveStages;
using BubblesDivePlanner.Controllers.Interfaces;
using BubblesDivePlannerTests.Asserters;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.DiveStages
{
    public class DiveStagesAcceptanceTests
    {
        [Fact]
        public void RunDiveStagesAcceptanceTest()
        {
            //Arrange
            var diveModel = DivePlannerApplicationTestFixture.GetDiveModel;
            var diveStepModel = DivePlannerApplicationTestFixture.GetDiveStep;
            var selectedCylinder = DivePlannerApplicationTestFixture.GetSelectedCylinder;
            IDiveStageRunner diveStageRunner = new DiveStageRunner();

            #region Run Dive Stages Twice

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

            #endregion

            #region Run Collation Of Decompression Steps

            //Act
            var diveStepQueue = DecompressionProfileController.CollateDecompressionDiveSteps(diveModel, DivePlannerApplicationTestFixture.GetSelectedCylinder);

            //Assert
            var diveSteps = diveStepQueue.ToArray();
            Assert.Equal(4, diveSteps.Length);
            Assert.Equal(12, diveSteps[0].Depth);
            Assert.Equal(1, diveSteps[0].Time);
            Assert.Equal(9, diveSteps[1].Depth);
            Assert.Equal(3, diveSteps[1].Time);

            #endregion

            // TODO AH Calculate decompression
        }
    }
}