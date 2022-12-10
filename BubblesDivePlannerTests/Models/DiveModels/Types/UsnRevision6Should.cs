using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Models.DiveModels.Types;
using Xunit;

namespace BubblesDivePlannerTests.Models.DiveModels.Types
{
    public class UsnRevision6Should
    {
        private const byte COMPARTMENT_COUNT = 9;
        private readonly IDiveModel diveModel = new UsnRevision6(null);

        [Fact]
        public void ContainsDiveModelName()
        {
            Assert.Equal("USN_REVISION_6", diveModel.Name);
        }

        [Fact]
        public void ContainsCompartmentCount()
        {
            Assert.Equal(COMPARTMENT_COUNT, diveModel.CompartmentCount);
        }

        [Fact]
        public void ContainsNitrogenHalfTime()
        {
            double[] expectedValue = new double[COMPARTMENT_COUNT] { 5.0, 10.0, 20.0, 40.0, 80.0, 120.0, 160.0, 200.0, 240.0 };

            Assert.Equal(expectedValue, diveModel.NitrogenHalfTimes);
        }

        [Fact]
        public void ContainsHeliumHalfTime()
        {
            double[] expectedValue = new double[COMPARTMENT_COUNT] { 5.0, 10.0, 20.0, 40.0, 80.0, 120.0, 160.0, 200.0, 240.0 };

            Assert.Equal(expectedValue, diveModel.HeliumHalfTimes);
        }

        [Fact]
        public void ContainsAValuesNitrogen()
        {
            double[] expectedValue = new double[COMPARTMENT_COUNT] { 1.37, 1.08, 0.69, 0.3, 0.34, 0.38, 0.4, 0.45, 0.42 };

            Assert.Equal(expectedValue, diveModel.AValuesNitrogen);
        }

        [Fact]
        public void ContainsBValuesNitrogen()
        {
            double[] expectedValue = new double[COMPARTMENT_COUNT] { 0.555, 0.625, 0.666, 0.714, 0.769, 0.833, 0.870, 0.909, 0.909 };

            Assert.Equal(expectedValue, diveModel.BValuesNitrogen);
        }

        [Fact]
        public void ContainsAValuesHelium()
        {
            double[] expectedValue = new double[COMPARTMENT_COUNT] { 1.12, 0.85, 0.71, 0.63, 0.5, 0.44, 0.54, 0.61, 0.61 };

            Assert.Equal(expectedValue, diveModel.AValuesHelium);
        }

        [Fact]
        public void ContainsBValuesHelium()
        {
            double[] expectedValue = new double[COMPARTMENT_COUNT] { 0.67, 0.714, 0.769, 0.83, 0.83, 0.91, 1.0, 1.0, 1.0 };

            Assert.Equal(expectedValue, diveModel.BValuesHelium);
        }

        [Fact]
        public void ContainsDiveProfileDefault()
        {
            Assert.NotNull(diveModel.DiveProfile);
        }

        [Fact]
        public void ContainsDiveProfileDefaultDiveProfile()
        {
            // Given
            var diveModel = new UsnRevision6(new DiveProfile(9));

            // Then
            Assert.NotNull(diveModel.DiveProfile);
        }

        [Fact]
        public void ContainsDiveProfile()
        {
            // Given
            var diveModel = new UsnRevision6(TestFixture.FixtureDiveModel(null).DiveProfile);

            // Then
            Assert.NotNull(diveModel.DiveProfile);
        }
    }
}