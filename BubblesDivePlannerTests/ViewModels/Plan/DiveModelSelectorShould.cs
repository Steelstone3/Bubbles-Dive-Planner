using Moq;
using ReactiveUI;
using Xunit;

public class DiveModelSelectorShould
{
    [Fact]
    public void Constructs()
    {
        // Given
        DiveModelSelector diveModelSelector = new();

        // Then
        Assert.IsAssignableFrom<IVisibility>(diveModelSelector);
        Assert.True(diveModelSelector.IsVisible);
        Assert.NotEmpty(diveModelSelector.DiveModels);
    }

    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        Mock<IDiveModel> diveModel = new();
        DiveModelSelector diveModelSelector = new();
        List<string> events = new();
        diveModelSelector.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        diveModelSelector.DiveModelSelected = diveModel.Object;
        diveModelSelector.IsVisible = false;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(diveModelSelector);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(diveModelSelector.DiveModelSelected), events);
        Assert.Contains(nameof(diveModelSelector.IsVisible), events);
    }
}
