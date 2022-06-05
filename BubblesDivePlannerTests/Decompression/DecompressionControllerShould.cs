using System;
using BubblesDivePlanner.Decompression;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.Decompression
{
    public class DecompressionControllerShould
    {
        private DivePlannerApplicationTestFixture divePlannerApplicationTestFixture = new();
        private DecompressionController _decompressionController = new();

        [InlineData(14.7, 15)]
        [InlineData(11.4, 12)]
        [InlineData(12, 12)]
        [InlineData(-5, 0)]
        [Theory]
        public void FindNearestDepthToDiveCeiling(double diveCeiling, int expectedNearestDepth)
        {
            //Act
            int nearestDepth = _decompressionController.FindNearestDepthToDiveCeiling(diveCeiling);

            //Assert
            Assert.Equal(expectedNearestDepth, nearestDepth);
        }

        [Fact]
        public void ReturnNullDiveStepIfDiveCeilingIsZeroOrLess()
        {
            //Act
            IDiveStepModel diveStep = _decompressionController.CalculateDiveStepAtStepInterval(divePlannerApplicationTestFixture.GetDiveModel, divePlannerApplicationTestFixture.GetSelectedCylinder);
            
            //Assert
            Assert.Null(diveStep);
        }

        [Fact]
        public void CalculateDiveStepAtStepIntervalFromFirstRun()
        {
            //Act
            var diveModel = divePlannerApplicationTestFixture.GetDiveModel;
            diveModel.DiveProfile = divePlannerApplicationTestFixture.GetDiveProfileResultFromFirstRun;
            IDiveStepModel diveStep = _decompressionController.CalculateDiveStepAtStepInterval(diveModel, divePlannerApplicationTestFixture.GetSelectedCylinder);
            
            //Assert
            Assert.Equal(6, diveStep.Depth);
            Assert.Equal(1, diveStep.Time);
        }

        [Fact(Skip = "Need to implement second run in the test fixture")]
        public void CalculateDiveStepAtStepIntervalFromSecondRun()
        {
            //Act
            var diveModel = divePlannerApplicationTestFixture.GetDiveModel;
            diveModel.DiveProfile = divePlannerApplicationTestFixture.GetDiveProfileResultFromSecondRun;
            IDiveStepModel diveStep = _decompressionController.CalculateDiveStepAtStepInterval(diveModel, divePlannerApplicationTestFixture.GetSelectedCylinder);
            
            //Assert
            Assert.Equal(6, diveStep.Depth);
            Assert.Equal(1, diveStep.Time);
        }

        [Fact (Skip = "Need to implement test")]
        public void CollateDecompressionDiveSteps()
        {

        }
    }
}