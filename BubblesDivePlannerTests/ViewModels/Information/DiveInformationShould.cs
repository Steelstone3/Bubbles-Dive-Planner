using ReactiveUI;
using Xunit;

public class DiveInformationShould
{
[Fact]
    public void Construct()
    {
        // Given
        DiveInformation diveInformation = new();

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(diveInformation);
        Assert.IsAssignableFrom<IDiveInformation>(diveInformation);
        Assert.Equal(0.0F, diveInformation.DiveCeiling);
    }

    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        DiveInformation diveInformation = new();
        List<string> events = new();
        diveInformation.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        diveInformation.DiveCeiling = 2.0F;

        // Then
        Assert.NotEmpty(events);
        Assert.Contains(nameof(diveInformation.DiveCeiling), events);
    }
}