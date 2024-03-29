using BubblesDivePlanner.Controllers.DiveStages;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.DiveStages
{
    public class DecompressionControllerShould
    {
        [Fact]
        public void FindNearestDepthToDiveCeilingFromFirstRun()
        {
            //Act
            var diveModel = DivePlannerApplicationTestFixture.GetDiveModel;
            diveModel.DiveProfile = DivePlannerApplicationTestFixture.GetDiveProfileResultFromFirstRun;
            var diveStepQueue = DecompressionProfileController.CollateDecompressionDiveSteps(diveModel, DivePlannerApplicationTestFixture.GetSelectedCylinder);

            //Assert
            var diveSteps = diveStepQueue.ToArray();
            Assert.Equal(0, diveSteps[0].Depth % 3);
            Assert.Equal(0, diveSteps[1].Depth % 3);
        }

        [Fact]
        public void CollateDecompressionDiveStepsFromFirstRun()
        {
            //Act
            var diveModel = DivePlannerApplicationTestFixture.GetDiveModel;
            diveModel.DiveProfile = DivePlannerApplicationTestFixture.GetDiveProfileResultFromFirstRun;
            var diveStepQueue = DecompressionProfileController.CollateDecompressionDiveSteps(diveModel, DivePlannerApplicationTestFixture.GetSelectedCylinder);

            //Assert
            var diveSteps = diveStepQueue.ToArray();
            Assert.Equal(2, diveSteps.Length);
            Assert.Equal(6, diveSteps[0].Depth);
            Assert.Equal(1, diveSteps[0].Time);
            Assert.Equal(3, diveSteps[1].Depth);
            Assert.Equal(3, diveSteps[1].Time);
        }

        [Fact]
        public void CollateDecompressionDiveStepsFromSecondRun()
        {
            //Act
            var diveModel = DivePlannerApplicationTestFixture.GetDiveModel;
            diveModel.DiveProfile = DivePlannerApplicationTestFixture.GetDiveProfileResultFromSecondRun;
            var diveStepQueue = DecompressionProfileController.CollateDecompressionDiveSteps(diveModel, DivePlannerApplicationTestFixture.GetSelectedCylinder);

            //Assert
            var diveSteps = diveStepQueue.ToArray();
            Assert.Equal(4, diveSteps.Length);
            Assert.Equal(12, diveSteps[0].Depth);
            Assert.Equal(1, diveSteps[0].Time);
            Assert.Equal(9, diveSteps[1].Depth);
            Assert.Equal(3, diveSteps[1].Time);
        }

        [Fact]
        public void ReturnEmptyQueueWhenDiveCeilingIsZero()
        {
            //Act
            var diveStepQueue = DecompressionProfileController.CollateDecompressionDiveSteps(DivePlannerApplicationTestFixture.GetDiveModel, DivePlannerApplicationTestFixture.GetSelectedCylinder);

            //Assert
            Assert.Empty(diveStepQueue);
        }
    }
}