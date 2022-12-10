using BubblesDivePlanner.Models.DiveModels;
using Xunit;

namespace BubblesDivePlannerTests.Models.DiveModels
{
    public class DiveModelShould
    {
        private readonly IDiveModel diveModel = new DiveModel(null);

        [Fact]
        public void ContainsName()
        {
            Assert.Null(diveModel.Name);
        }

        [Fact]
        public void ContainsCompartmentName()
        {
            // Then
            Assert.Equal(0, diveModel.CompartmentCount);
        }

        [Fact]
        public void ContainsNitrogenHalfTimes()
        {
            // Then
            Assert.Null(diveModel.NitrogenHalfTimes);
        }

        [Fact]
        public void ContainsHeliumHalfTimes()
        {
            // Then
            Assert.Null(diveModel.HeliumHalfTimes);
        }

        [Fact]
        public void ContainsAValuesNitrogen()
        {
            // Then
            Assert.Null(diveModel.AValuesNitrogen);
        }

        [Fact]
        public void ContainsBValuesNitrogen()
        {
            // Then
            Assert.Null(diveModel.BValuesNitrogen);
        }

        [Fact]
        public void ContainsAValuesHelium()
        {
            // Then
            Assert.Null(diveModel.AValuesHelium);
        }

        [Fact]
        public void ContainsBValuesHelium()
        {
            // Then
            Assert.Null(diveModel.BValuesHelium);
        }

        [Fact]
        public void ContainsDiveProfile()
        {
            // Then
            Assert.Null(diveModel.DiveProfile);
        }
    }
}