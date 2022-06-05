using System;
using BubblesDivePlanner.Decompression;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.Decompression
{
    public class DecompressionControllerShould
    {
        private DivePlannerApplicationTestFixture diveStagesTestFixture = new();
        private DecompressionController _decompressionController = new();

        [InlineData(14.7, 15)]
        [InlineData(11.4, 12)]
        [InlineData(12, 12)]
        [Theory]
        public void FindNearestDepthToDiveCeiling(double diveCeiling, int expectedNearestDepth)
        {
            //Act
            int nearestDepth = _decompressionController.FindNearestDepthToDiveCeiling(diveCeiling);

            //Assert
            Assert.Equal(expectedNearestDepth, nearestDepth);
        }

        [Fact(Skip = "Need to implement step")]
        public void CalculateDiveStepAtStepInterval()
        {
            //Act
            
            IDiveStepModel diveStep = _decompressionController.CalculateDiveStepAtStepInterval();
            
            //Assert
            Assert.Equal(18, diveStep.Depth);
            Assert.Equal(3, diveStep.Time);
        }

        [Fact (Skip = "Need to implement test")]
        public void CollateDecompressionDiveSteps()
        {

        }
    }
}