using Moq;
using ReactiveUI;
using Xunit;

public class GasUsageShould
{
    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        GasUsage gasUsage = new();
        List<string> events = new();
        gasUsage.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        gasUsage.Remaining = 1680;
        gasUsage.Used = 720;
        gasUsage.SurfaceAirConsumptionRate = 12;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(gasUsage);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(gasUsage.Remaining), events);
        Assert.Contains(nameof(gasUsage.Used), events);
        Assert.Contains(nameof(gasUsage.SurfaceAirConsumptionRate), events);
    }

    [Fact]
    public void DeepClone()
    {
        // Given
        GasUsage gasUsage = new();

        // When
        GasUsage clonedGasUsage = new(gasUsage);

        // Then
        Assert.NotSame(gasUsage, clonedGasUsage);
    }
}