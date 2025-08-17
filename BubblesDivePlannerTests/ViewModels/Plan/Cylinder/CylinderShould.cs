using ReactiveUI;
using Xunit;

public class CylinderShould
{
    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        GasUsage gasUsage = new();
        GasMixture gasMixture = new();
        Cylinder cylinder = new();
        List<string> events = new();
        cylinder.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        cylinder.Name = "EAN32";
        cylinder.Volume = 12;
        cylinder.Pressure = 200;
        cylinder.InitialPressurisedVolume = 2400;
        cylinder.GasUsage = gasUsage;
        cylinder.GasMixture = gasMixture;
        cylinder.IsVisible = false;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(cylinder);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(cylinder.Name), events);
        Assert.Contains(nameof(cylinder.Volume), events);
        Assert.Contains(nameof(cylinder.Pressure), events);
        Assert.Contains(nameof(cylinder.GasUsage), events);
        Assert.Contains(nameof(cylinder.GasMixture), events);
        Assert.Contains(nameof(cylinder.IsVisible), events);
    }

    [Fact]
    public void DeepClone()
    {
        // Given
        Cylinder cylinder = new();

        // When
        Cylinder clonedCylinder = new(cylinder);

        // Then
        Assert.NotSame(cylinder, clonedCylinder);
    }
}