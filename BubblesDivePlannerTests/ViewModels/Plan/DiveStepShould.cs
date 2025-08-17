using ReactiveUI;
using Xunit;

public class DiveStepShould
{
    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        DiveStep diveStep = new();
        List<string> events = new();
        diveStep.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        diveStep.Depth = 50;
        diveStep.Time = 10;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(diveStep);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(diveStep.Depth), events);
        Assert.Contains(nameof(diveStep.Time), events);
    }
}
