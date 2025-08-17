using ReactiveUI;
using Xunit;

public class DiveModelShould
{
    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        DiveModelProfile diveModelProfile = new(16);
        DiveModel diveModel = new();
        List<string> events = new();
        diveModel.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        diveModel.DiveModelProfile = diveModelProfile;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(diveModel);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(diveModel.DiveModelProfile), events);
    }

    [Fact]
    public void DeepClone()
    {
        // Given
        DiveModelFactory diveModelFactory = new();
        DiveModel diveModel = diveModelFactory.CreateZhl16Buhlmann();

        // When
        DiveModel clonedDiveModel = new(diveModel);

        // Then
        Assert.NotSame(diveModel, clonedDiveModel);
        Assert.NotSame(diveModel.DiveModelProfile, clonedDiveModel.DiveModelProfile);
    }
}