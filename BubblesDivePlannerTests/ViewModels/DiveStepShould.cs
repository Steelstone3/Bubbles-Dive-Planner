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
        Assert.NotEmpty(events);
        Assert.Contains(nameof(diveStep.Depth), events);
        Assert.Contains(nameof(diveStep.Time), events);
    }
}

internal class DiveStep : ReactiveObject, IDiveStep
{
    private byte _depth;
    public byte Depth
    {
        get => _depth;
        set => this.RaiseAndSetIfChanged(ref _depth, value);
    }

    private byte _time;
    public byte Time
    {
        get => _time;
        set => this.RaiseAndSetIfChanged(ref _time, value);
    }
}

internal interface IDiveStep
{
    byte Depth { get; set; }
    byte Time { get; set; }
}