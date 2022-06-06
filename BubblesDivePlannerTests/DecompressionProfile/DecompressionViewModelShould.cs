using BubblesDivePlanner.DecompressionProfile;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.DecompressionProfile
{
    public class DecompressionViewModelShould
    {
        private DivePlannerApplicationTestFixture _divePlannerApplicationTestFixture = new();

        [Fact]
        public void SimulateDecompressionProfile()
        {
            var diveProfile = _divePlannerApplicationTestFixture.GetDiveProfileResultFromFirstRun;
            var diveModel = _divePlannerApplicationTestFixture.GetDiveModel;
            diveModel.DiveProfile = diveProfile;
            DecompressionProfileViewModel decompressionProfileViewModel = new(diveModel, _divePlannerApplicationTestFixture.GetSelectedCylinder);

            //Act
            var diveStepQueue = decompressionProfileViewModel.DecompressionDiveSteps;

            //Arrange
            Assert.NotNull(diveStepQueue);
            Assert.NotEmpty(diveStepQueue);
            var diveSteps = diveStepQueue.ToArray();
            Assert.Equal(6, diveSteps[0].Depth);
            Assert.Equal(1, diveSteps[0].Time);
            Assert.Equal(3, diveSteps[1].Depth);
            Assert.Equal(3, diveSteps[1].Time);
        }
    }
}