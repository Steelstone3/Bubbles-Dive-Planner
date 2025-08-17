using Moq;
using ReactiveUI;
using Xunit;

public class DiveModelProfileShould
{
    private const int COMPARTMENTS = 16;

    [Fact]
    public void Construct()
    {
        // Given
        DiveModelProfile diveModelProfile = new(COMPARTMENTS);

        // Then
        Assert.Equal(0.0F, diveModelProfile.OxygenAtPressure);
        Assert.Equal(0.0F, diveModelProfile.HeliumAtPressure);
        Assert.Equal(0.0F, diveModelProfile.NitrogenAtPressure);

        for (int i = 0; i < COMPARTMENTS; i++)
        {
            Assert.Equal(0.79F, diveModelProfile.NitrogenTissuePressures[i]);
            Assert.Equal(0.0F, diveModelProfile.HeliumTissuePressures[i]);
            Assert.Equal(0.79F, diveModelProfile.TotalTissuePressures[i]);
            Assert.Equal(0.0F, diveModelProfile.AValues[i]);
            Assert.Equal(0.0F, diveModelProfile.BValues[i]);
            Assert.Equal(0.0F, diveModelProfile.ToleratedAmbientPressures[i]);
            Assert.Equal(0.0F, diveModelProfile.MaxSurfacePressures[i]);
            Assert.Equal(0.0F, diveModelProfile.CompartmentLoads[i]);
        }
    }

    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        float[] defaultValue = [5.0F, 10.0F];
        DiveModelProfile diveModelProfile = new(COMPARTMENTS);
        List<string> events = new();
        diveModelProfile.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        diveModelProfile.OxygenAtPressure = 5.0F;
        diveModelProfile.HeliumAtPressure = 5.0F;
        diveModelProfile.NitrogenAtPressure = 5.0F;
        diveModelProfile.NitrogenTissuePressures = defaultValue;
        diveModelProfile.HeliumTissuePressures = defaultValue;
        diveModelProfile.AValues = defaultValue;
        diveModelProfile.BValues = defaultValue;
        diveModelProfile.ToleratedAmbientPressures = defaultValue;
        diveModelProfile.MaxSurfacePressures = defaultValue;
        diveModelProfile.CompartmentLoads = defaultValue;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(diveModelProfile);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(diveModelProfile.OxygenAtPressure), events);
        Assert.Contains(nameof(diveModelProfile.HeliumAtPressure), events);
        Assert.Contains(nameof(diveModelProfile.NitrogenAtPressure), events);
        Assert.Contains(nameof(diveModelProfile.NitrogenTissuePressures), events);
        Assert.Contains(nameof(diveModelProfile.HeliumTissuePressures), events);
        Assert.Contains(nameof(diveModelProfile.AValues), events);
        Assert.Contains(nameof(diveModelProfile.BValues), events);
        Assert.Contains(nameof(diveModelProfile.ToleratedAmbientPressures), events);
        Assert.Contains(nameof(diveModelProfile.MaxSurfacePressures), events);
        Assert.Contains(nameof(diveModelProfile.CompartmentLoads), events);
    }

    // TODO AH Remove controller polution
    [Fact]
    public void CalculateDiveCeiling()
    {
        // Given
        float[] toleratedAmbientPressures = [0.9F, 0.2F, 0.3F, 1.0F, 1.1F, 1.2F, 0.1F, 0.11F, 0.12F, 0.14F, 0.15F, 0.16F, 0.17F, 0.18F, 0.19F, 0.21F];
        DiveModelProfile diveModelProfile = new(COMPARTMENTS)
        {
            ToleratedAmbientPressures = toleratedAmbientPressures
        };

        // When
        float diveCeiling = diveModelProfile.DiveCeiling;

        // Then
        Assert.Equal(2.0F, diveCeiling);
    }

    [Fact]
    public void DeepClone()
    {
        // Given
        DiveModelProfile diveModelProfile = new DiveModelProfile(16);

        // When
        DiveModelProfile clonedDiveModelProfile = new(diveModelProfile);

        // Then
        Assert.NotSame(diveModelProfile, clonedDiveModelProfile);
    }
}