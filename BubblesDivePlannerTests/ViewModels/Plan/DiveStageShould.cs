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

    [Fact]
    public void DeepClone()
    {
        // Given
        DiveModelFactory diveModelFactory = new();
        DiveStage diveStage = new();
        diveStage.DiveModel = diveModelFactory.CreateZhl16Buhlmann();

        // When
        DiveStage clonedDiveStage = new DiveStage(diveStage);

        // Then
        Assert.NotSame(diveStage, clonedDiveStage);
        Assert.NotSame(diveStage.DiveModel, clonedDiveStage.DiveModel);
        Assert.NotSame(diveStage.DiveStep, clonedDiveStage.DiveStep);
        Assert.NotSame(diveStage.Cylinder, clonedDiveStage.Cylinder);
    }
}