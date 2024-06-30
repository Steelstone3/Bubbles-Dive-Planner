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
        Assert.IsAssignableFrom<IDiveModelProfile>(diveModelProfile);
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

    [Fact(Skip="Needs interaction test")]
    public void CalculateDiveCeiling()
    {
        // Given
    
        // When
    
        // Then
    }
}