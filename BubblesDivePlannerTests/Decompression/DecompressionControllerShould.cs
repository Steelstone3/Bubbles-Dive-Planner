using BubblesDivePlanner.Decompression;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.Decompression
{
    public class DecompressionControllerShould
    {
        private DivePlannerApplicationTestFixture divePlannerApplicationTestFixture = new();
        private DecompressionController _decompressionController = new();

        [Fact]
        public void FindNearestDepthToDiveCeilingFromFirstRun()
        {
            //Act
            var diveModel = divePlannerApplicationTestFixture.GetDiveModel;
            diveModel.DiveProfile = divePlannerApplicationTestFixture.GetDiveProfileResultFromFirstRun;
            var diveStepQueue = _decompressionController.CollateDecompressionDiveSteps(diveModel, divePlannerApplicationTestFixture.GetSelectedCylinder);

            //Assert
            var diveSteps = diveStepQueue.ToArray();
            Assert.Equal(0, diveSteps[0].Depth % 3);
            Assert.Equal(0, diveSteps[1].Depth % 3);
        }

        [Fact(Skip = "Second run needs implementing in the test fixture")]
        public void FindNearestDepthToDiveCeilingFromSecondRun()
        {
            //Act
            var diveModel = divePlannerApplicationTestFixture.GetDiveModel;
            diveModel.DiveProfile = divePlannerApplicationTestFixture.GetDiveProfileResultFromSecondRun;
            var diveStepQueue = _decompressionController.CollateDecompressionDiveSteps(diveModel, divePlannerApplicationTestFixture.GetSelectedCylinder);

            //Assert
            var diveSteps = diveStepQueue.ToArray();
            Assert.Equal(0, diveSteps[0].Depth % 3);
            Assert.Equal(0, diveSteps[1].Depth % 3);
        }

        [Fact]
        public void CollateDecompressionDiveStepsFromFirstRun()
        {
            //Act
            var diveModel = divePlannerApplicationTestFixture.GetDiveModel;
            diveModel.DiveProfile = divePlannerApplicationTestFixture.GetDiveProfileResultFromFirstRun;
            var diveStepQueue = _decompressionController.CollateDecompressionDiveSteps(diveModel, divePlannerApplicationTestFixture.GetSelectedCylinder);

            //Assert
            var diveSteps = diveStepQueue.ToArray();
            Assert.Equal(2, diveSteps.Length);
            Assert.Equal(6, diveSteps[0].Depth);
            Assert.Equal(1, diveSteps[0].Time);
            Assert.Equal(3, diveSteps[1].Depth);
            Assert.Equal(3, diveSteps[1].Time);
        }

        [Fact(Skip = "Second run needs implementing in the test fixture")]
        public void CollateDecompressionDiveStepsFromSecondRun()
        {
            //Act
            var diveModel = divePlannerApplicationTestFixture.GetDiveModel;
            diveModel.DiveProfile = divePlannerApplicationTestFixture.GetDiveProfileResultFromSecondRun;
            var diveStepQueue = _decompressionController.CollateDecompressionDiveSteps(diveModel, divePlannerApplicationTestFixture.GetSelectedCylinder);

            //Assert
            var diveSteps = diveStepQueue.ToArray();
            Assert.Equal(2, diveSteps.Length);
            Assert.Equal(6, diveSteps[0].Depth);
            Assert.Equal(2, diveSteps[0].Time);
        }

        [Fact]
        public void ReturnEmptyQueueWhenDiveCeilingIsZero()
        {
            //Act
            var diveStepQueue = _decompressionController.CollateDecompressionDiveSteps(divePlannerApplicationTestFixture.GetDiveModel, divePlannerApplicationTestFixture.GetSelectedCylinder);

            //Assert
            Assert.Empty(diveStepQueue);
        }
    }
}