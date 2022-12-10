using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Models.DiveModels.Types;
using Xunit;

namespace BubblesDivePlannerTests.Models.DiveModels.Types
{
    public class Zhl16BuhlmannShould
    {
        private const byte COMPARTMENT_COUNT = 16;
        private readonly IDiveModel diveModel = new Zhl16Buhlmann(null);

        [Fact]
        public void ContainsDiveModelName()
        {
            Assert.Equal("ZHL16_B", diveModel.Name);
        }

        [Fact]
        public void ContainsCompartmentCount()
        {
            Assert.Equal(COMPARTMENT_COUNT, diveModel.CompartmentCount);
        }

        [Fact]
        public void ContainsNitrogenHalfTime()
        {
            double[] expectedValue = new double[COMPARTMENT_COUNT] { 4.0, 8.0, 12.5, 18.5, 27.0, 38.3, 54.3, 77.0, 109.0, 146.0, 187.0, 239.0, 305.0, 390.0, 498.0, 635.0 };

            Assert.Equal(expectedValue, diveModel.NitrogenHalfTimes);
        }

        [Fact]
        public void ContainsHeliumHalfTime()
        {
            double[] expectedValue = new double[COMPARTMENT_COUNT] { 1.51, 3.02, 4.72, 6.99, 10.21, 14.48, 20.53, 29.11, 41.20, 55.19, 70.69, 90.34, 115.29, 147.42, 188.24, 240.03 };

            Assert.Equal(expectedValue, diveModel.HeliumHalfTimes);
        }

        [Fact]
        public void ContainsAValuesNitrogen()
        {
            double[] expectedValue = new double[COMPARTMENT_COUNT] { 1.2559, 1.0000, 0.8618, 0.7562, 0.6667, 0.5600, 0.4947, 0.4500, 0.4187, 0.3798, 0.3497, 0.3223, 0.2850, 0.2737, 0.2523, 0.2327 };

            Assert.Equal(expectedValue, diveModel.AValuesNitrogen);
        }

        [Fact]
        public void ContainsBValuesNitrogen()
        {
            double[] expectedValue = new double[COMPARTMENT_COUNT] { 0.5050, 0.6514, 0.7222, 0.7825, 0.8126, 0.8434, 0.8693, 0.8910, 0.9092, 0.9222, 0.9319, 0.9403, 0.9477, 0.9544, 0.9602, 0.9653 };

            Assert.Equal(expectedValue, diveModel.BValuesNitrogen);
        }

        [Fact]
        public void ContainsAValuesHelium()
        {
            double[] expectedValue = new double[COMPARTMENT_COUNT] { 1.7424, 1.3830, 1.1919, 1.0458, 0.9220, 0.8205, 0.7305, 0.6502, 0.5950, 0.5545, 0.5333, 0.5189, 0.5181, 0.5176, 0.5172, 0.5119 };

            Assert.Equal(expectedValue, diveModel.AValuesHelium);
        }

        [Fact]
        public void ContainsBValuesHelium()
        {
            double[] expectedValue = new double[COMPARTMENT_COUNT] { 0.4245, 0.5747, 0.6527, 0.7223, 0.7582, 0.7957, 0.8279, 0.8553, 0.8757, 0.8903, 0.8997, 0.9073, 0.9122, 0.9171, 0.9217, 0.9267 };

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
            var diveModel = new Zhl16Buhlmann(new DiveProfile(16));

            // Then
            Assert.NotNull(diveModel.DiveProfile);
        }

        [Fact]
        public void ContainsDiveProfile()
        {
            // Given
            var diveModel = new Zhl16Buhlmann(TestFixture.FixtureDiveModel(null).DiveProfile);

            // Then
            Assert.NotNull(diveModel.DiveProfile);
        }
    }
}