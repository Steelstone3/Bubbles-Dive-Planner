using BubblesDivePlanner.Models.DiveModels;
using Xunit;

namespace BubblesDivePlannerTests.Models.DiveModels
{
    public class DiveProfileShould
    {
        private const byte COMPARTMENT_COUNT = 10;
        private const double EXPECTED_PRESSURE_AT_DEPTH = 12.1;
        private const double EXPECTED_DEPTH_CEILING = 140;
        private readonly double[] expectedDefaultListValue = new double[COMPARTMENT_COUNT] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
        private readonly double[] expectedDefaultListTissuePressureValue = new double[COMPARTMENT_COUNT] { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 };
        private IDiveProfile diveProfile = new DiveProfile(COMPARTMENT_COUNT);

        [Fact]
        public void ContainsMaxSurfacePressures()
        {
            Assert.Equal(COMPARTMENT_COUNT, diveProfile.MaxSurfacePressures.Length);
            Assert.Equal(expectedDefaultListValue, diveProfile.MaxSurfacePressures);
        }

        [Fact]
        public void ContainsTissuePressuresNitrogen()
        {
            Assert.Equal(COMPARTMENT_COUNT, diveProfile.NitrogenTissuePressures.Length);
            Assert.Equal(expectedDefaultListTissuePressureValue, diveProfile.NitrogenTissuePressures);
        }

        [Fact]
        public void ContainsTissuePressuresHelium()
        {
            Assert.Equal(COMPARTMENT_COUNT, diveProfile.HeliumTissuePressures.Length);
            Assert.Equal(expectedDefaultListValue, diveProfile.HeliumTissuePressures);
        }

        [Fact]
        public void ContainsTissuePressuresTotal()
        {
            Assert.Equal(COMPARTMENT_COUNT, diveProfile.TotalTissuePressures.Length);
            Assert.Equal(expectedDefaultListTissuePressureValue, diveProfile.TotalTissuePressures);
        }

        [Fact]
        public void ContainsToleratedAmbientPressures()
        {
            Assert.Equal(COMPARTMENT_COUNT, diveProfile.ToleratedAmbientPressures.Length);
            Assert.Equal(expectedDefaultListValue, diveProfile.ToleratedAmbientPressures);
        }

        [Fact]
        public void ContainsAValues()
        {
            Assert.Equal(COMPARTMENT_COUNT, diveProfile.AValues.Length);
            Assert.Equal(expectedDefaultListValue, diveProfile.AValues);
        }

        [Fact]
        public void ContainsBValues()
        {
            Assert.Equal(COMPARTMENT_COUNT, diveProfile.BValues.Length);
            Assert.Equal(expectedDefaultListValue, diveProfile.BValues);
        }

        [Fact]
        public void ContainsCompartmentLoads()
        {
            Assert.Equal(COMPARTMENT_COUNT, diveProfile.CompartmentLoads.Length);
            Assert.Equal(expectedDefaultListValue, diveProfile.CompartmentLoads);
        }

        [Fact]
        public void ContainOxygenPressureAtDepth()
        {
            Assert.Equal(0, diveProfile.OxygenPressureAtDepth);
        }

        [Fact]
        public void ContainHeliumPressureAtDepth()
        {
            Assert.Equal(0, diveProfile.HeliumPressureAtDepth);
        }

        [Fact]
        public void ContainNitrogenPressureAtDepth()
        {
            Assert.Equal(0, diveProfile.NitrogenPressureAtDepth);
        }

        [Fact]
        public void ContainDepthCeiling()
        {
            Assert.Equal(0, diveProfile.DepthCeiling);
        }

        [Fact]
        public void ConstructDiveProfile()
        {
            double[] defaultList = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            diveProfile = new DiveProfile
            (
                defaultList,
                defaultList,
                defaultList,
                defaultList,
                defaultList,
                defaultList,
                defaultList,
                defaultList,
                EXPECTED_PRESSURE_AT_DEPTH,
                EXPECTED_PRESSURE_AT_DEPTH,
                EXPECTED_PRESSURE_AT_DEPTH,
                EXPECTED_PRESSURE_AT_DEPTH
            );

            Assert.Equal(defaultList, diveProfile.NitrogenTissuePressures);
            Assert.Equal(defaultList, diveProfile.HeliumTissuePressures);
            Assert.Equal(defaultList, diveProfile.TotalTissuePressures);
            Assert.Equal(defaultList, diveProfile.MaxSurfacePressures);
            Assert.Equal(defaultList, diveProfile.ToleratedAmbientPressures);
            Assert.Equal(defaultList, diveProfile.AValues);
            Assert.Equal(defaultList, diveProfile.BValues);
            Assert.Equal(defaultList, diveProfile.CompartmentLoads);
            Assert.Equal(EXPECTED_PRESSURE_AT_DEPTH, diveProfile.OxygenPressureAtDepth);
            Assert.Equal(EXPECTED_PRESSURE_AT_DEPTH, diveProfile.HeliumPressureAtDepth);
            Assert.Equal(EXPECTED_PRESSURE_AT_DEPTH, diveProfile.NitrogenPressureAtDepth);
            Assert.Equal(EXPECTED_PRESSURE_AT_DEPTH, diveProfile.DepthCeiling);
        }

        [Fact]
        public void UpdateGasMixtureUnderPressure()
        {
            diveProfile.UpdateGasMixtureUnderPressure(EXPECTED_PRESSURE_AT_DEPTH, EXPECTED_PRESSURE_AT_DEPTH, EXPECTED_PRESSURE_AT_DEPTH);

            Assert.Equal(EXPECTED_PRESSURE_AT_DEPTH, diveProfile.OxygenPressureAtDepth);
            Assert.Equal(EXPECTED_PRESSURE_AT_DEPTH, diveProfile.HeliumPressureAtDepth);
            Assert.Equal(EXPECTED_PRESSURE_AT_DEPTH, diveProfile.NitrogenPressureAtDepth);
        }

        [Fact]
        public void UpdateDepthCeiling()
        {
            diveProfile = new DiveProfile(null, null, null,null, new double[] {5.0, 10.0, 15.0},null,null,null, 0, 0, 0, 0 );
            
            diveProfile.UpdateDepthCeiling();

            Assert.Equal(EXPECTED_DEPTH_CEILING, diveProfile.DepthCeiling);
        }
    }
}