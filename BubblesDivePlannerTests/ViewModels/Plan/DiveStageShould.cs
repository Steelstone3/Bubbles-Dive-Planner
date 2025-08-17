using ReactiveUI;
using Xunit;

public class DiveStageShould
{
    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        DiveModel diveModel = new();
        DiveStep diveStep = new();
        Cylinder cylinder = new();
        DiveStage diveStage = new();
        List<string> events = new();
        diveStage.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        diveStage.DiveModel = diveModel;
        diveStage.DiveStep = diveStep;
        diveStage.Cylinder = cylinder;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(diveStage);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(diveStage.DiveModel), events);
        Assert.Contains(nameof(diveStage.DiveStep), events);
        Assert.Contains(nameof(diveStage.Cylinder), events);
    }
}