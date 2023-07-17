using BubblesDivePlanner.ViewModels.Model.Plan.DiveModels;
using BubblesDivePlanner.ViewModels.Plan.DiveModels;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Plan.DiveModels
{
    public class DiveProfileShould
    {
        [Fact]
        public void Construct()
        {
            // Given
            const double defaultValue = 0.0;
            const double defaultPressurisedValue = 0.79;
            IDiveProfile diveProfile = new DiveProfile(1);

            // Then
            Assert.Equal(defaultValue, diveProfile.MaxSurfacePressures[0]);
            Assert.Equal(defaultPressurisedValue, diveProfile.NitrogenTissuePressures[0]);
            Assert.Equal(defaultValue, diveProfile.HeliumTissuePressures[0]);
            Assert.Equal(defaultPressurisedValue, diveProfile.TotalTissuePressures[0]);
            Assert.Equal(defaultValue, diveProfile.ToleratedAmbientPressures[0]);
            Assert.Equal(defaultValue, diveProfile.AValues[0]);
            Assert.Equal(defaultValue, diveProfile.BValues[0]);
            Assert.Equal(defaultValue, diveProfile.CompartmentLoads[0]);
            Assert.Equal(defaultValue, diveProfile.OxygenAtPressure);
            Assert.Equal(defaultValue, diveProfile.NitrogenAtPressure);
            Assert.Equal(defaultValue, diveProfile.HeliumAtPressure);
        }

        [Theory]
        [InlineData(16)]
        [InlineData(12)]
        [InlineData(8)]
        public void ConstructToCorrectSize(int compartmentSize)
        {
            // Given
            IDiveProfile diveProfile = new DiveProfile(compartmentSize);

            // Then
            Assert.Equal(compartmentSize, diveProfile.MaxSurfacePressures.Length);
            Assert.Equal(compartmentSize, diveProfile.NitrogenTissuePressures.Length);
            Assert.Equal(compartmentSize, diveProfile.HeliumTissuePressures.Length);
            Assert.Equal(compartmentSize, diveProfile.TotalTissuePressures.Length);
            Assert.Equal(compartmentSize, diveProfile.ToleratedAmbientPressures.Length);
            Assert.Equal(compartmentSize, diveProfile.AValues.Length);
            Assert.Equal(compartmentSize, diveProfile.BValues.Length);
            Assert.Equal(compartmentSize, diveProfile.CompartmentLoads.Length);
            Assert.Equal(0, diveProfile.OxygenAtPressure);
            Assert.Equal(0, diveProfile.NitrogenAtPressure);
            Assert.Equal(0, diveProfile.HeliumAtPressure);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            double[] defaultArrayValue = new double[] {3.0, 6.0};
            const double defaultValue = 10.0;
            DiveProfile diveProfile = new(16);
            var viewModelEvents = new List<string>();
            diveProfile.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            diveProfile.MaxSurfacePressures = defaultArrayValue;
            diveProfile.NitrogenTissuePressures = defaultArrayValue;
            diveProfile.HeliumTissuePressures = defaultArrayValue;
            diveProfile.TotalTissuePressures = defaultArrayValue;
            diveProfile.ToleratedAmbientPressures = defaultArrayValue;
            diveProfile.AValues = defaultArrayValue;
            diveProfile.BValues = defaultArrayValue;
            diveProfile.CompartmentLoads = defaultArrayValue;
            diveProfile.OxygenAtPressure = defaultValue;
            diveProfile.HeliumAtPressure = defaultValue;
            diveProfile.NitrogenAtPressure = defaultValue;

            //Assert
            Assert.NotEmpty(viewModelEvents);
            Assert.Contains(nameof(diveProfile.MaxSurfacePressures), viewModelEvents);
            Assert.Contains(nameof(diveProfile.NitrogenTissuePressures), viewModelEvents);
            Assert.Contains(nameof(diveProfile.HeliumTissuePressures), viewModelEvents);
            Assert.Contains(nameof(diveProfile.TotalTissuePressures), viewModelEvents);
            Assert.Contains(nameof(diveProfile.ToleratedAmbientPressures), viewModelEvents);
            Assert.Contains(nameof(diveProfile.AValues), viewModelEvents);
            Assert.Contains(nameof(diveProfile.BValues), viewModelEvents);
            Assert.Contains(nameof(diveProfile.CompartmentLoads), viewModelEvents);
            Assert.Contains(nameof(diveProfile.OxygenAtPressure), viewModelEvents);
            Assert.Contains(nameof(diveProfile.HeliumAtPressure), viewModelEvents);
            Assert.Contains(nameof(diveProfile.NitrogenAtPressure), viewModelEvents);
        }
    }
}